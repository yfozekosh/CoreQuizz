using System;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.Shared.DomainModel
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifieDateTime { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            ModifieDateTime = DateTime.Now;
        }
    }
}
