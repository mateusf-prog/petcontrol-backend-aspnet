using System;
using Microsoft.AspNetCore.Identity;

namespace UnitTests.Fakers
{
    public static class IdentityUserFaker
    {
        public static IdentityUser GetIdentityUser()
        {
            return new IdentityUser()
            {
                Id = "1",
                UserName = "test",
                Email = "usertest@hotmail.com",
                PhoneNumber = "11999999999"
            };
        }
    }
}