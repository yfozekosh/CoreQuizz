using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using CoreQuizz.WebService.Communication.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace CoreQuizz.WebService.Communication.ViewModel
{
    [ModelBinder(BinderType = typeof(SurveyViewModelBinder))] 
    public class SaveSurveyViewModel
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Description { get; set; }
        public int Access { get; set; }
        public IList<Question> QuestionDefinitions { get; set; }
    }
}