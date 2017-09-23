namespace CoreQuizz.WebService.Identity
{
    public class TokenResponce
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}