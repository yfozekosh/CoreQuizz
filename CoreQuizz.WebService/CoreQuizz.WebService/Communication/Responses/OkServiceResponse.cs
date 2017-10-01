namespace CoreQuizz.WebService.Communication.Responses
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