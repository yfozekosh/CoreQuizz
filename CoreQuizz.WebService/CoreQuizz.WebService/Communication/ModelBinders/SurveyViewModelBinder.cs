using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using CoreQuizz.WebService.Communication.ViewModel;
using CoreQuizz.WebService.ModelContract.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreQuizz.WebService.Communication.ModelBinders
{
    public class SurveyViewModelBinder : IModelBinder
    {
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

            JToken token = JObject.Parse(body);
            var serializer = new Newtonsoft.Json.JsonSerializer()
            {
                Converters = { new QuestionJsonConverter()},
                NullValueHandling = NullValueHandling.Include,
                TypeNameHandling = TypeNameHandling.None,
                Formatting = Formatting.Indented
            };
            SaveSurveyViewModel survey = token.ToObject<SaveSurveyViewModel>(serializer);
            
            bindingContext.Result = ModelBindingResult.Success(survey);
        }
    }
}