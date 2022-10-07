using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurmasModels
{
    public class TokenUserObject
    {
        public int id { get; set; } = default(int);
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string extension { get; set; } = string.Empty;
        public int active { get; set; } = default(int);

        public TokenUserObject()
        {

        }
    }
}
