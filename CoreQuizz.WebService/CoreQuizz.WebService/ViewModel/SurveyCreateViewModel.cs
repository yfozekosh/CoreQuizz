namespace CoreQuizz.WebService.ViewModel
{
    public enum SurveyAccess
    {
        Public = 1,
        ByUrl = 2,
        ByGrant = 3
    }

    public class SurveyCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public SurveyAccess Access { get; set; }
    }
}