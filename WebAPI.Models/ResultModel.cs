using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ResultModel
    {
        public ResultModel()
        {
            Status = ResultStatus.Undefined;
        }

        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
