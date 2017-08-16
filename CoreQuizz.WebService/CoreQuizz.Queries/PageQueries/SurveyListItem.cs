using System;

namespace CoreQuizz.Queries.PageQueries
{
    public class SurveyListItem
    {
        public string SurveyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int QuestionsCount { get; set; }
        public int Stars { get; set; }
    }
}
