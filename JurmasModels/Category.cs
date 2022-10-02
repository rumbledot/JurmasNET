using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurmasModels
{
    public class JurmasStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }
}
