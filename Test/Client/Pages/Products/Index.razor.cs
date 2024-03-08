using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using Test.Shared;
using Test.Shared.Entities.DTO;

namespace Test.Client.Pages.Products
{
    public partial class Index
    {
        private List<ProductResponse> productList { get; set; } = new List<ProductResponse>();

        [Inject]
        private IDialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetProductList();

        }

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

        [Inject]
        private IJSRuntime Js {  get; set; }

        private async Task GenerateExcel(List<ProductResponse> productResponses)
        {
            string filename = "export.xlsx";
            var XLSStream = ExportToExcel(productResponses).Result;

            await Js.InvokeVoidAsync("BlazorDownloadFile", filename, XLSStream);
        }

        private Task<byte[]> ExportToExcel(List<ProductResponse> productResponses)
        {
            var wb = new XLWorkbook();

            var ws = wb.Worksheets.Add("Weather Forecast");

            ws.Cell(1, 1).Value = "Name";
            ws.Cell(1, 2).Value = "Model";
            ws.Cell(1, 3).Value = "Description";
            ws.Cell(1, 4).Value = "Price";

            for (int row = 0; row < productResponses.Count; row++)
            {
                ws.Cell(row + 1, 1).Value = productResponses[row].Name;
                ws.Cell(row + 1, 2).Value = productResponses[row].Model;
                ws.Cell(row + 1, 3).Value = productResponses[row].Description;
                ws.Cell(row + 1, 3).Value = productResponses[row].Price;
            }

            MemoryStream XLSStream = new();
            wb.SaveAs(XLSStream);

            return Task.FromResult(XLSStream.ToArray());
        }

        void OpenProductModal(string? id = null) => NavigationManager.NavigateTo($"product/NewOrEdit/{id}");

        async Task OpenProductDetailModel(ProductResponse product)
        {
            var options = new DialogOptions { ClassBackground = "my-custom-class", CloseOnEscapeKey = true, FullWidth = true, Position = DialogPosition.Center };
            var parameters = new DialogParameters<ProductDetailsDialog> { { x => x.Product, product } };

            var dialog = await DialogService.ShowAsync<ProductDetailsDialog>("Product Detail", parameters, options);
            var result = await dialog.Result;
        }
    }
}
