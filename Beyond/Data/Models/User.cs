﻿using System;
using System.Collections.Generic;
using Beyond.Migrations;
using Microsoft.AspNetCore.Identity;

namespace Beyond.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

    }
}