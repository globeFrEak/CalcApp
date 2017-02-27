using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalcApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Input { get; set; }
        public string Result { get; set; }
        public bool Degree { get; set; } = false;
    }
}