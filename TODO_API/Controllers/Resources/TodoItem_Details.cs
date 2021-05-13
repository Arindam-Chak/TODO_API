using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TODO_API.Controllers.Resources
{
    public class TodoItem_Details
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Item_id { get; set; }
        public string Task { get; set; }
        public bool Is_Complete { get; set; }
    }

}
