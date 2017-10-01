using System.Runtime.Serialization;
using CoreQuizz.Shared.DomainModel.Abstract;

namespace CoreQuizz.Shared.DomainModel.Survey.Question.Abstract
{
    public abstract class Question : ModifiableBaseEntity
    {
        public int? ResultId { get; set; }
        public string QuestionLabel { get; set; }
        
        [IgnoreDataMember]
        public virtual Survey Survey {get; set; }

        public virtual string Type { get; set; }
    }
}
