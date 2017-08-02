﻿namespace CoreQuizz.Shared.DomainModel
{
    public abstract class Question : BaseEntity
    {
        public int? ResultId { get; set; }
        public string QuestionLabel { get; set; }
        public virtual Survey Survey {get; set; }
    }
}
