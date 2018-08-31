using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ResultApi<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
