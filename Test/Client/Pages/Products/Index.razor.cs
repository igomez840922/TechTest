using Test.Shared.Entities.DTO;

namespace Test.Client.Pages.Products
{
    public partial class Index
    {
        private List<ProductResponse> productList { get; set; } = new List<ProductResponse>();

        protected override async Task OnInitializedAsync()
        {
            await GetProductList();

        }

        async Task OnProductChangedCallback(object? product) { }

        private async Task GetProductList()
        {
            productList.Clear();
            productList = await productServices.GetAllProduct();
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
