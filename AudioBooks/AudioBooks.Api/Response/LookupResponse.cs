using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AudioBooks.Response
{
    public class LookupResponse<T>
    {
        public static LookupResponse<T> BuildErrorResponse(string key, string message)
        {
            var response = new LookupResponse<T>();
            response.Message = message;
            response.Errors.AddModelError(key, message);
            return response;
        }

        public LookupResponse()
        {
            Errors = new ModelStateDictionary();
        }

        public string Message { get; private set; }
        public T Result { get; private set; }
        public ModelStateDictionary Errors { get; set; }
    }
}
