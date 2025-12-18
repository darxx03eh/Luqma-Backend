using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Bases
{
    public class FileApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Messgae { get; set; }
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
