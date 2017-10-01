using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreQuizz.Shared.DomainModel.Survey.Question;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreQuizz.WebService.Communication.ModelBinders
{  
    public class QuestionJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            var type = jObject["type"]?.Value<string>();
            if (type == null)
            {
                throw new ArgumentException("type not specified in question json");
            }

            Type qType = QuestionTypeMap.GetFromMap(type);
            return jObject.ToObject(qType);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Question));
        }
    }

    public static class QuestionTypeMap
    {
        private static readonly Dictionary<Type, string> Map = new Dictionary<Type, string>
        {
            {typeof(InputQuestion), "input"},
            {typeof(CheckboxQuestion), "checkbox"},
            {typeof(RadioQuestion), "radio"}
        };

        private static readonly Dictionary<string,Type> ReverseMap = new Dictionary<string, Type>();
        
        static QuestionTypeMap()
        {
            var assembly = typeof(Question).GetTypeInfo().Assembly;
            var questionType = typeof(Question);

            IEnumerable<Type> questionTypes = assembly.GetTypes().Where(t => t.GetTypeInfo().IsSubclassOf(questionType) && !t.GetTypeInfo().IsAbstract);
            Dictionary<Type, string>.KeyCollection keysInMap = Map.Keys;

            var notMappedQuestionTypes = questionTypes.Where(q => !keysInMap.Contains(q)).ToList();
            if (notMappedQuestionTypes.Count != 0)
            {
                throw new QuestionsNotMappedException(notMappedQuestionTypes);
            }

            foreach (var keyValuePair in Map)
            {
                ReverseMap.Add(keyValuePair.Value, keyValuePair.Key);
            }
        }

        public static Type GetFromMap(string typeName)
        {
            if (!ReverseMap.ContainsKey(typeName))
            {
                return null;
            }

            return ReverseMap[typeName];
        }
    }

    public class QuestionsNotMappedException : Exception
    {
        public QuestionsNotMappedException(List<Type> notMappedQuestionTypes) : base(
            $" Questins: {String.Join(" , ", notMappedQuestionTypes)} are not mapped in json formatter")
        {
        }
    }
}