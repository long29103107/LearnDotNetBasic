using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Request
{
    public class ProductRequest : BaseRequest
    {
        public string Name { get; set; } = "";
        public string Sku { get; set; } = "";
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = 0;
        public long CategoryId { get; set; } = 0;
        public DateTime CreatedAtFrom { get; set; } = DateTime.MinValue;
        public DateTime CreatedAtTo { get; set; } = DateTime.MaxValue;
        public DateTime UpdatedAtFrom { get; set; } = DateTime.MinValue;
        public DateTime UpdatedAtTo { get; set; } = DateTime.MaxValue;
    }
}
