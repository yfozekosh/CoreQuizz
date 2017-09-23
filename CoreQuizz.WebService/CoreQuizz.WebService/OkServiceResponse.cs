namespace CoreQuizz.WebService
{
    public class OkServiceResponse<TResponceValue> : ServiceResponse<TResponceValue>
    {
        public OkServiceResponse(TResponceValue value)
        {
            IsSuccess = true;
            Value = value;
        }
    }
}