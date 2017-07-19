using System.Collections.Generic;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public class Survey : BaseEntity
    {
        public string Title { get; set; }

        public IList<Question> Questions { get; set; }

        public User CreatedBy { get; set; }
    }
}
