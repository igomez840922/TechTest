using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Test.Shared.Entities.DTO;
using Mapster;

namespace Test.Client.Pages.Products
{
    public partial class NewOrEditProduct
    {
        private bool IsNew;
        private List<string> ImgList { get; set; } = new List<string>();
        private const int maxImageSize = 2 * 1024 * 1024;
        private ProductResponse? product = new();

        [Parameter]
        public string? Id { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<object?> ProductCallback { get; set; }

        public bool loading;

        protected override async Task OnInitializedAsync()
        {
            IsNew = true;
            if (!string.IsNullOrEmpty(Id))
            {
                IsNew = false;
                // Si se proporciona un ID válido, obtener los datos del producto
                product = await productServices.GetProductById(Id);
            }
        }

        private async Task OnSubmit()
        {
            var productRequest = product.Adapt<ProductRequest>();

            if (IsNew)
            {
                await productServices.AddProduct(productRequest);
            }
            else
            {
                await productServices.UpdateProduct(Id, productRequest);
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
                if (product is not null)
                {
                    using var stream = file.OpenReadStream();
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    product.Photo = ($"data:{file.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}");
                }
            }
            catch (Exception)
            {
            }
            loading = false;
        }
    }
}
