// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "800001",
                        Username = "superman",
                        Password = "Password_123",
                        Claims =
                        {
                          new Claim("given_name","Henry"),
                          new Claim("family_name","Cavill"),
                          new Claim("email","henry@superman.com"),
                          new Claim("address","15 krypton ave "),
                          new Claim("gender","male"),
                          new Claim("role","customer"),
                              new Claim("operations","view_customer|view_publishers")
                        }},
                    new TestUser
                    {
                        SubjectId = "800002",
                        Username = "spiderman",
                        Password = "Password_123",
                        Claims =
                        {
                          new Claim("given_name","Tobby"),
                          new Claim("family_name","Mcguire"),
                          new Claim("email","tobby@spiderman.com"),
                          new Claim("address","18 brooklyn ave "),
                          new Claim("gender","male"),
                             new Claim("role","admin"),
                               new Claim("operations","view_customer|view_publishers|manage_publishers|manage_audiobooks")
                        }
                    }
                };
            }
        }


        /*
        public static List<TestUser> Users
                {
                    get
                    {
                        var address = new
                        {
                            street_address = "One Hacker Way",
                            locality = "Heidelberg",
                            postal_code = 69118,
                            country = "Germany"
                        };

                        return new List<TestUser>
                        {
                            new TestUser
                            {
                                SubjectId = "818727",
                                Username = "alice",
                                Password = "alice",
                                Claims =
                                {
                                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                                    new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                                }
                            },
                            new TestUser
                            {
                                SubjectId = "88421113",
                                Username = "bob",
                                Password = "bob",
                                Claims =
                                {
                                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                                    new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                                }
                            }
                        };
                    }
                }*/


    }
}