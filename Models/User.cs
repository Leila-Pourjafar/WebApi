using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Api.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(500)]
        public string City { get; set; }

        [StringLength(500)]
        public string Province { get; set; }

        public bool Gender { get; set; }

        public bool MaritalStatus { get; set; }

        public string Education { get; set; }

        public string Address { get; set; }

        [StringLength(200)]
        public string ProfileImage { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
