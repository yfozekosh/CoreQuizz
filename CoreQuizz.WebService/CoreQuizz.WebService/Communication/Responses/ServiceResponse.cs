namespace CoreQuizz.WebService.Communication.Responses
{
    public abstract class ServiceResponse
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public bool IsCritical { get; set; } = false;
    }

    public abstract class ServiceResponse<TResponceValue> : ServiceResponse
    {
        public TResponceValue Value { get; set; }
    }
}