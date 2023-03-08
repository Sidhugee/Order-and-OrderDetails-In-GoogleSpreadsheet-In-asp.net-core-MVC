using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect_SQL_Derver_with_Spreadsheet.Models
{



    public class Item
    {
        [Key]

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        //Relatiomshipt represents that One order can have multiple line items
        public ICollection<LineItems> LineItems { get; set; }

    }
    public class LineItems
    {
        [Key]
        public string LineItemsId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }

        [ForeignKey("Id")]
        public string  OrderId { get; set; }
        public Products Product { get; set; }
    }
}
