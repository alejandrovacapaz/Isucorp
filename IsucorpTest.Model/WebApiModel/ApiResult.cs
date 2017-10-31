using System;
using System.Collections.Generic;

namespace IsucorpTest.Model.WebApiModel
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public object Result { get; set; }

        public ApiResult(bool success, IEnumerable<string> errors)
        {
            Success = success;
            Errors = errors;
        }

        public ApiResult(bool success, object result)
        {
            Success = success;
            Result = result;
        }

        public ApiResult(bool success, params string[] errors)
        {
            Success = success;
            Errors = errors;
        }

        public ApiResult(bool success, Exception e)
        {
            Success = success;
            Errors = new string[] { e.Message };
        }

    }
}
