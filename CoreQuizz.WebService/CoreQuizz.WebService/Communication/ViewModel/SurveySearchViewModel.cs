namespace CoreQuizz.WebService.Communication.ViewModel
{
    public class SurveySearchViewModel
    {
        public string SearchText { get; set; }

        public int ItemsOnPage { get; set; }

        public int PageNumber { get; set; }

        public bool IsIncludeCount { get; set; } = false;
    }
}
