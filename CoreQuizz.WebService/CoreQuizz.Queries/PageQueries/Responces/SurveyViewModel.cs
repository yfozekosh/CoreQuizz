using System;
using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;

namespace CoreQuizz.Queries.PageQueries.Responces
{
    public class SurveyViewModel
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Stars { get; set; }
        public string CreatedBy { get; set; } 
    }
}