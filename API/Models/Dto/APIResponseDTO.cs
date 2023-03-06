
using System.Net;

namespace API.Models.Dto
{
    public class APIResponseDTO
    {

        public APIResponseDTO()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

    }
}

