using System;
using Microsoft.AspNetCore.Identity;

namespace Haushaltsbuch.WebApi.Benutzerkonto.Models
{
    public class Benutzerkonto : IdentityUser<Guid>
    {
        public Benutzerkonto()
        {
        }
    }
}