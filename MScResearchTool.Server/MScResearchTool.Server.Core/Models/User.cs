namespace MScResearchTool.Server.Core.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual string Salt { get; set; }
    }
}
