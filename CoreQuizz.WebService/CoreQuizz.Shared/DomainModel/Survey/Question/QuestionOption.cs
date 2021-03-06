﻿using System.Runtime.Serialization;
using CoreQuizz.Shared.DomainModel.Abstract;

namespace CoreQuizz.Shared.DomainModel.Survey.Question
{
    public class QuestionOption : ModifiableBaseEntity
    {
        public string Value { get; set; }
        
        public bool? IsSelected { get; set; }

        [IgnoreDataMember]
        public Abstract.Question Question { get; set; }
    }
}