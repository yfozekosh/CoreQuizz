namespace CoreQuizz.WebService.Communication.Responses
{
    public class ErrorServiceRespose: ServiceResponse<string>
    {
        public ErrorServiceRespose(string error)
        {
            this.IsSuccess = false;
            this.Error = error;
        }
    }
}