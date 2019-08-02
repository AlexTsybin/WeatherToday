using System;

namespace WeatherToday.API
{
    public class ErrorModel
    {
        public Exception ServiceException { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsError { get; set; }

        public int ErrorCode { get; set; }
    }

    public class ErrorModel<TResult> : ErrorModel
    {
        public TResult Instance { get; set; }
    }

    public class StringErrorModel : ErrorModel<string>
    {
    }
}
