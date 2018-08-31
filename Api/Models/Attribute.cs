using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Attribute
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int? Length { get; set; }
        public bool AllowNull { get; set; }
    }
}
