using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class RequestResult
    {
        public HttpStatusCode StatusCode { get; set; } 
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
