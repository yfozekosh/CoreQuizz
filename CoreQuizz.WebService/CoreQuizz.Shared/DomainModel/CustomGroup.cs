using System.Collections.Generic;

namespace CoreQuizz.Shared.DomainModel
{
    public class CustomGroup : BaseEntity
    {
        public List<User> UsersInGroup { get; set; }
        
        public string Name { get; set; }
        
        public List<CustomGroupGrant> Grants { get; set; }
    }
}