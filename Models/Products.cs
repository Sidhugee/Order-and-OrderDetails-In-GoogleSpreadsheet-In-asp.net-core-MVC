using System.Drawing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect_SQL_Derver_with_Spreadsheet.Models
{


    public class Products
    {
        public string ItemName { get; set; }
        public string Avaliable { get; set; }

        [ForeignKey("LineItemsId")]
        public string LineItemsId { get; set; }
        public LineItems LineItems { get; set; }

    }
}
