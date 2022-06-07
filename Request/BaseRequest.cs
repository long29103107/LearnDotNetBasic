using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Request
{
    public class BaseRequest
    {
        public int Size { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string OrderBy { get; set; }
     }
}
