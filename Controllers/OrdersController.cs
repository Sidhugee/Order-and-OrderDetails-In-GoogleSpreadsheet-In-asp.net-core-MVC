using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;
using Connect_SQL_Derver_with_Spreadsheet;
using Connect_SQL_Derver_with_Spreadsheet.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

public class ItemsController : Controller
{
    const string SPREADSHEET_ID = "1fYO7fIkd3b_rDQkoMUoIXdloPHLqOTU4uyhDt-ktzOc";
    const string SHEET_NAME = "Customers";

    SpreadsheetsResource.ValuesResource _googleSheetValues;
    public ItemsController(GoogleSheetsHelper googleSheetsHelper)
    {
        _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
    }

    public IActionResult Index()
    {
        var range = $"{SHEET_NAME}!A2:C";
        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;
        var items = ItemsMapper.MapFromRangeData(values);
        return View(items);
    }

    public IActionResult Get(int rowId)
    {
        var range = $"{SHEET_NAME}!A{rowId}:C{rowId}";
        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;
        var item = ItemsMapper.MapFromRangeData(values).FirstOrDefault();

        return View(item);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    // Creating Order in Customer Sheet

    [HttpPost]
    public IActionResult Create(Item item, LineItems lineItems)
    {
        var range = $"{SHEET_NAME}!A:C";
        var valueRange = new ValueRange
        {
            Values = ItemsMapper.MapToRangeData(item)
        };
     var appendRequest = _googleSheetValues.Append(valueRange, SPREADSHEET_ID, range);
        appendRequest.ValueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
        appendRequest.Execute();


        // Add line items to "LineItems" sheet
        const string LineItemsSheet = "LineItems";
        var lineItemsRange = $"{LineItemsSheet}!A:F";
        var lineItemsValueRange = new ValueRange();

        lineItemsValueRange.Values = new List<IList<object>>();
        foreach (var lineItem in item.LineItems)
        {
            lineItemsValueRange.Values.Add(new List<object>
        {
            Guid.NewGuid(),
            lineItem.ProductName,
            lineItem.Quantity,
            valueRange.Values[0][0]

        });
        }
        var appendLineItemsRequest = _googleSheetValues.Append(lineItemsValueRange, SPREADSHEET_ID, lineItemsRange);
        appendLineItemsRequest.ValueInputOption = AppendRequest.ValueInputOptionEnum.USERENTERED;
        appendLineItemsRequest.Execute();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int rowId)
    {
        var range = $"{SHEET_NAME}!A{rowId}:C{rowId}";
        var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
        var response = request.Execute();
        var values = response.Values;
        var item = ItemsMapper.MapFromRangeData(values).FirstOrDefault();

        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(int rowId, Item item)
    {
        var range = $"{SHEET_NAME}!A{rowId}:C{rowId}";
        var valueRange = new ValueRange
        {
            Values = ItemsMapper.MapToRangeData(item)
        };
        var updateRequest = _googleSheetValues.Update(valueRange, SPREADSHEET_ID, range);
        updateRequest.ValueInputOption = UpdateRequest.ValueInputOptionEnum.USERENTERED;
        updateRequest.Execute();
        return RedirectToAction(nameof(Index));
    }


}
