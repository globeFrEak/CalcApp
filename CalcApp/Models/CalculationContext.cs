using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CalcApp.Models
{
    public class CalculationContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
    }
}