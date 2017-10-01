namespace CoreQuizz.WebService.Identity.JWT
{
    public class TokenResponce
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}