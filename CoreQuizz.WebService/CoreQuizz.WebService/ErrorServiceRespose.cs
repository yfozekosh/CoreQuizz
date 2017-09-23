namespace CoreQuizz.WebService
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