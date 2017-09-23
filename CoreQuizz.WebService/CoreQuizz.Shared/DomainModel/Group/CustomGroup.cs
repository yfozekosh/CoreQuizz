using System.Collections.Generic;
using CoreQuizz.Shared.DomainModel.Abstract;

namespace CoreQuizz.Shared.DomainModel.Group
{
    public class CustomGroup : ModifiableBaseEntity
    {
        public List<User> UsersInGroup { get; set; }
        
        public string Name { get; set; }
        
        public List<CustomGroupGrant> Grants { get; set; }
    }
}