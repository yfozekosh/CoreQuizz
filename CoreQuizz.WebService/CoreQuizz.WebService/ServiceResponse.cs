using System;

namespace CoreQuizz.WebService
{
    public abstract class ServiceResponse<TResponceValue>
    {
        public TResponceValue Value { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public bool IsCritical { get; set; } = false;
    }

    public class OkServiceResponse<TResponceValue> : ServiceResponse<TResponceValue>
    {
        public OkServiceResponse(TResponceValue value)
        {
            IsSuccess = true;
            Value = value;
        }
    }

    public class ErrorServiceRespose: ServiceResponse<string>
    {
        public ErrorServiceRespose(string error)
        {
            this.IsSuccess = true;
            this.Error = error;
        }
    }

    public class CriticalServiceResponse : ServiceResponse<Exception>
    {
        public CriticalServiceResponse(Exception e)
        {
            this.IsSuccess = false;
            this.IsCritical = true;
            this.Value = e;
            this.Error = e.Message;
        }
    }
}