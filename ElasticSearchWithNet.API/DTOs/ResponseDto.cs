using System.Net;

namespace ElasticSearchWithNet.API.DTOs
{
    public record ResponseDto<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public HttpStatusCode Status { get; set; }


        // Used Static Factory Method
        public static ResponseDto<T> Success(T data,HttpStatusCode code)
        {
            return new ResponseDto<T> { Data = data,Status = code};
        }


        public static ResponseDto<T> Fail(List<string> errors,HttpStatusCode code) 
        {
            return new ResponseDto<T> { Errors=errors, Status = code };
        }

        public static ResponseDto<T> Fail(string error, HttpStatusCode code)
        {
            return new ResponseDto<T> { Errors = new List<string> { error}, Status = code };
        }

    }
}
