using Hunter.DataAccess.Interface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

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
    }
}
