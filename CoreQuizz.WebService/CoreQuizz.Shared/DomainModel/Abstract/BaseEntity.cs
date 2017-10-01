using System;

namespace CoreQuizz.Shared.DomainModel.Abstract
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;            
        }
    }
}