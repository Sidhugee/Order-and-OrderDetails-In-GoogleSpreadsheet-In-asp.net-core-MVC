using Connect_SQL_Derver_with_Spreadsheet.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Connect_SQL_Derver_with_Spreadsheet
{
    public static class ItemsMapper
    {
        public static List<Item> MapFromRangeData(IList<IList<object>> values)
        {
            var items = new List<Item>();
            foreach (var value in values)
            {

                Item item = new()
                {

                    Id = value[0].ToString(),
                    Name = value[1].ToString()

                };
                items.Add(item);
            }
            return items;
        }
        public static List<LineItems> MapFromLineItemsRangeData(IList<IList<object>> values)
        {
            var items = new List<LineItems>();
            foreach (var value in values)
            {

                LineItems item = new()
                {

                    LineItemsId = value[0].ToString(),
                    ProductName = value[1].ToString(),
                    Quantity = Convert.ToInt32(value[2]),
                    OrderId = value[3].ToString()



                };
                items.Add(item);
            }
            return items;
        }
        public static List<Products> MapFromCatLittersRangeData(IList<IList<object>> values)
        {
            var catlitters = new List<Products>();
            foreach (var value in values)
            {
                Products item = new()
                {
                    ItemName = value[0].ToString(),
                    Avaliable = value[1].ToString()

                };
                catlitters.Add(item);
            }
            return catlitters;
        }
        public static IList<IList<object>> MapToRangeData(Item item)
        {
            var objectList = new List<object>() { Guid.NewGuid(), item.Name };
            var rangeData = new List<IList<object>> { objectList };
            return rangeData;
        }
        public static IList<IList<object>> MapToLineItemsRangeData(LineItems item, Item data)
        {
            var objectList = new List<object>() { Guid.NewGuid(), item.ProductName,item.Quantity, data.Id  };
            var rangeData = new List<IList<object>> { objectList };
            return rangeData;
        }
    }
}