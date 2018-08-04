using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Aplicacao.Api
{
    public class ResultApi<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
