﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
	public class AppRole : IdentityRole<string>
    {
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}

