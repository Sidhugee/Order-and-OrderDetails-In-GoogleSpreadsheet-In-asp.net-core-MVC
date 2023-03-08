using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore.Mvc;

namespace Connect_SQL_Derver_with_Spreadsheet.Controllers
{
    public class CatLittersController : Controller
    {
        const string SPREADSHEET_ID = "1fYO7fIkd3b_rDQkoMUoIXdloPHLqOTU4uyhDt-ktzOc";
        const string SHEET_NAME = "Orders";

        SpreadsheetsResource.ValuesResource _googleSheetValues;
        public CatLittersController(GoogleSheetsHelper googleSheetsHelper)
        {
            _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;
        }

        public IActionResult CatLitters()
        {
            var range = $"{SHEET_NAME}!A2:B";
            var request = _googleSheetValues.Get(SPREADSHEET_ID, range);
            var response = request.Execute();
            var values = response.Values;
            var items = ItemsMapper.MapFromCatLittersRangeData(values);

            return View(items);
        }

    }
}
