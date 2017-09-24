using System;
using CoreQuizz.Shared.DomainModel.Survey.Question;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using CoreQuizz.WebService.ModelContract.Abstract;
using CoreQuizz.WebService.ModelContract.Contracts;
using Newtonsoft.Json.Linq;

namespace CoreQuizz.WebService.ModelContract
{
    internal class QuestionRecognizer : IQuestionRecognizer
    {
        private readonly IQuestionChainElement _first;

        public QuestionRecognizer()
        {
            _first = new InputQuestionRecognizer()
                .SetNext(new CheckboxQuestionRecognizer())
                .SetNext(new RadioQuestionRecgnizer());
        }

        public Question Recognize(JObject jObject)
        {
            QuestionChainResult res = _first.Process(jObject);
            return res.Result;
        }
    }

    internal class InputQuestionRecognizer : QuestionChainElement
    {
        protected override QuestionChainResult _Process(JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));
            JToken typeToken = jObject["type"];

            // TODO: Refactor
            if (typeToken.Value<string>() == "input")
            {
                JToken label;
                if (!jObject.TryGetValue("questionLabel", out label))
                {
                    label = null;
                }
                JToken defValue;
                if (!jObject.TryGetValue("defaultValue", out defValue))
                {
                    defValue = null;
                }

                var questionLabel = label?.Value<string>();
                var value = defValue?.Value<string>();
                
                return new QuestionChainResult
                {
                    IsFound = true,
                    Result = new InputQuestion()
                    {
                        QuestionLabel = questionLabel,
                        Value = value
                    }
                };
            }

            return new QuestionChainResult
            {
                IsFound = false
            };
        }
    }

    internal class RadioQuestionRecgnizer : QuestionChainElement
    {
        protected override QuestionChainResult _Process(JObject jObject)
        {
            throw new NotImplementedException();
        }
    }

    internal class CheckboxQuestionRecognizer : QuestionChainElement
    {
        protected override QuestionChainResult _Process(JObject jObject)
        {
            throw new NotImplementedException();
        }
    }
}