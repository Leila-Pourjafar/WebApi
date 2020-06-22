using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.ViewModel
{
    public class Profile
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int Id { get; set; }
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
    }
}
