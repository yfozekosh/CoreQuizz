using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using CoreQuizz.WebService.Communication.ViewModel;
using CoreQuizz.WebService.ModelContract;
using CoreQuizz.WebService.ModelContract.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;

namespace CoreQuizz.WebService.Communication.ModelBinders
{
    public class SurveyViewModelBinder : IModelBinder
    {
        private readonly IQuestionRecognizer _recognizer;

        public SurveyViewModelBinder(IQuestionRecognizer recognizer)
        {
            _recognizer = recognizer;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            if (bindingContext.ModelMetadata.ModelType != typeof(SaveSurveyViewModel))
            {
                return;
            }

            var sr = new StreamReader(bindingContext.HttpContext.Request.Body);

            var body = await sr.ReadToEndAsync();

            Console.WriteLine(await sr.ReadToEndAsync());

            JToken token = JObject.Parse(body, new JsonLoadSettings());

            var definitionTokens = token["questionDefinitions"];
            var questions = new List<Question>();
            foreach (JToken definitionToken in definitionTokens)
            {
                var definitionObj = definitionToken as JObject;
                if (definitionObj == null)
                {
                    throw new ArgumentException();
                }

                Question question = _recognizer.Recognize(definitionObj);
                questions.Add(question);
            }

            bindingContext.Result = ModelBindingResult.Success(new SaveSurveyViewModel()
            {
                SurveyName = "tess",
                QuestionDefinitions = questions
            });
        }
    }
}