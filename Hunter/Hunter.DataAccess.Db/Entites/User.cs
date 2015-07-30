using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hunter.DataAccess.Db
{
    public partial class User : IUser<int>, IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Login { get; set; }

        [Required]
        [StringLength(300)]
        public string PasswordHash { get; set; }

        public int State { get; set; }

        public int RoleId { get; set; }

        public virtual UserRole UserRole { get; set; }

        public string UserName
        {
            get
            {
                return Login;
            }

            set
            {
                Login = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User,int> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
