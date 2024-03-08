using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Test.Shared.Entities.DataBase;
using Test.Shared.Entities;

namespace Test.Client.Pages.Products
{
    public partial class NewOrEditProduct
    {
        private bool IsNew;
        private List<string> ImgList { get; set; } = new List<string>();
        private const int maxImageSize = 2 * 1024 * 1024;
        private Product product = new();

        [Parameter]
        public string? id { get; set; }

        [Parameter]
        public EventCallback<object?> ProductCallback { get; set; }

        public bool loading;

        protected override async Task OnInitializedAsync()
        {
            IsNew = true;
            if (!string.IsNullOrEmpty(id))
            {
                IsNew = false;
                // Si se proporciona un ID válido, obtener los datos del producto
                product = await productServices.GetProductById(id);
            }
        }

        private async Task OnSubmit()
        {
            AppResult result = new AppResult();

            if (IsNew)
            {
                result = await productServices.AddProduct(product);
            }
            else
            {
                result = await productServices.UpdateProduct(product);
            }

            NavigationManager.NavigateTo("/product");
            await ProductCallback.InvokeAsync(product);
        }

        async Task OnChangeImage(InputFileChangeEventArgs e)
        {
            foreach (var archivo in e.GetMultipleFiles())
            {
                using var stream = archivo.OpenReadStream(maxImageSize);
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                ImgList.Add($"data:{archivo.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}");
            }
        }

        private async Task LoadFile(IBrowserFile file)
        {
            loading = true;
            try
            {
                using var stream = file.OpenReadStream();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                product.Photo = ($"data:{file.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}");
            }
            catch (Exception ex)
            {
            }
            loading = false;
            //TODO upload the files to the server
        }
    }
}
