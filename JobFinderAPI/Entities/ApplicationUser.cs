﻿using Microsoft.AspNetCore.Identity;

namespace JwtAuthAspNet7WebAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
