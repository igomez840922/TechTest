using Microsoft.AspNetCore.Components;
using MudBlazor;
using Test.Shared.Entities.DTO;

namespace Test.Client.Pages.Products
{
    public partial class ProductDetailsDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public ProductResponse Product { get; set; } = new ProductResponse();

        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
