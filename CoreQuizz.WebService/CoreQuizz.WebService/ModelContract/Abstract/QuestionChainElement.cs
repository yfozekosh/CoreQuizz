using System;
using Newtonsoft.Json.Linq;

namespace CoreQuizz.WebService.ModelContract.Abstract
{
    internal abstract class QuestionChainElement : IQuestionChainElement
    {
        private IQuestionChainElement _nextChainElement;

        protected QuestionChainElement(IQuestionChainElement nextChainElement = null)
        {
            _nextChainElement = nextChainElement;
        }

        public QuestionChainResult Process(JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));

            QuestionChainResult result = this._Process(jObject);
            if (result.IsFound)
            {
                return result;
            }
            if (_nextChainElement == null)
            {
                throw new QuestionNotRecognizedException();
            }
            
            return _nextChainElement.Process(jObject);
        }

        public IQuestionChainElement SetNext(IQuestionChainElement next)
        {
            this._nextChainElement = next;
            return this;
        }

        protected abstract QuestionChainResult _Process(JObject jObject);
    }
}