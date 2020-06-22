using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Api.Models
{
    public class UserToken : IdentityUserToken<int>
    {
        //public string AccessTokenHash { get; set; }

        //public DateTimeOffset AccessTokenExpiresDateTime { get; set; }

        //public string RefreshTokenIdHash { get; set; }

        //public string RefreshTokenIdHashSource { get; set; }

        //public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }
    }
}
