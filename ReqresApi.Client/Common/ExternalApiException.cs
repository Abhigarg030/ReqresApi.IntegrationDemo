using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqresApi.Client.Common
{
    public class ExternalApiException : Exception
    {
        public ExternalApiException(string message, Exception? inner = null)
            : base(message, inner)
        {
        }
    }
}
