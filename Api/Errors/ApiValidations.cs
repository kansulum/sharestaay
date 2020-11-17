using System.Collections.Generic;

namespace Api.Errors
{
    public class ApiValidations : ApiResponse
    {
         public ApiValidations() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}