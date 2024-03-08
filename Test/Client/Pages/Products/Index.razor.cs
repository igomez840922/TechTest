using Test.Shared.Entities.DataBase;
using Test.Shared.Entities;

namespace Test.Client.Pages.Products
{
    public partial class Index
    {
        private List<Product> productList { get; set; } = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            await GetProductList();

        }

        async Task OnProductChangedCallback(object? product) { }

        private async Task GetProductList()
        {
            productList.Clear();
            var productItems = await productServices.GetAllProduct();
            foreach (var item in productItems)
            {
                productList.Add(item);
            }
        }

        private async Task DeleteProduct(string id)
        {
            var result = await productServices.DeleteProduct(id);

            try
            {
                if (result != null)
                {
                    Console.WriteLine("Product deleted");
                    await GetProductList();
                    //
                    StateHasChanged();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product is not deleted" + ex);
            }
        }

        async Task OpenProductModal(string id) => NavigationManager.NavigateTo($"product/NewOrEdit/{id}");
    }
}
