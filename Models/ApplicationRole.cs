using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Title { get; set; }
    }
}
