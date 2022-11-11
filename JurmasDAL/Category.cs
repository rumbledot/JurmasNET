using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurmasDAL
{
    public class RecipeeStatus
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = String.Empty;
    }

    public class Category
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = String.Empty;

        [Column(TypeName = "varchar(200)")]
        public string Description { get; set; } = String.Empty;

        public virtual List<Recipee> Recipees { get; set; }
    }
}
