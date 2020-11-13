using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCrud.Data
{
    public class Response
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public List<Person> Data { get; set; }
        public Response()
        {
            this.Success = 0;
        }
    }
}
