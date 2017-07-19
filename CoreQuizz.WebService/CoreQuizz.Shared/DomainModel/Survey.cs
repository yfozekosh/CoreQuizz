using System.Collections.Generic;

namespace CoreQuizz.Shared.DomainModel
{
    public class Survey : BaseEntity
    {
        public string Title { get; set; }

        public IList<Question> Questions { get; set; }

        public User CreatedBy { get; set; }
    }
}
