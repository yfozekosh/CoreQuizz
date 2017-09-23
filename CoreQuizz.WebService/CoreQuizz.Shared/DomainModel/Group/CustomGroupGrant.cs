using CoreQuizz.Shared.DomainModel.Abstract;

namespace CoreQuizz.Shared.DomainModel.Group
{
    public class CustomGroupGrant : ModifiableBaseEntity
    {
        public CustomGroup Group { get; set; }
        public User GrantedUser { get; set; }
    }
}