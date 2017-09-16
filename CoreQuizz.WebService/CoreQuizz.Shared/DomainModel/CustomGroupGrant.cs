namespace CoreQuizz.Shared.DomainModel
{
    public class CustomGroupGrant : BaseEntity
    {
        public CustomGroup Group { get; set; }
        public User GrantedUser { get; set; }
    }
}