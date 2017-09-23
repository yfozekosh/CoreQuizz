using System;

namespace CoreQuizz.Shared.DomainModel.Abstract
{
    public class ModifiableBaseEntity : BaseEntity
    {
        public DateTime ModifieDateTime { get; set; }

        public ModifiableBaseEntity() : base()
        {
            ModifieDateTime = DateTime.Now;
        }
    }
}
