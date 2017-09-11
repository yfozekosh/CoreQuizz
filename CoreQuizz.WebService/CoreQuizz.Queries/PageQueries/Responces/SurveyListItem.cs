using System;

namespace CoreQuizz.Queries.PageQueries.Responces
{
    public class SurveyListItem
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int QuestionsCount { get; set; }
        public int Stars { get; set; }

        public string CreatedBy { get; set; }
    }
}
