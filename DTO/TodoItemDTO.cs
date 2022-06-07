using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.DTO
{
    public class TodoItemDTO
    {
        public string Name { get; set; }
        public bool? IsComplete { get; set; }
        public string Secret { get; set; }
    }
}
