// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Lin.IDP
{
    public static class Config
    {
        /************************
        Default profile Claims
        - name
        - family_name
        - given_name
        - middle_name
        - nickname
        - preferred_username
        - profile
        - picture
        - website
        - gender
        - birthdate
        - zoneinfo
        - locale
        - updated_at
        ************************/
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()// is a mandatory
                ,new IdentityResources.Profile()
                ,new IdentityResources.Email()
                ,new IdentityResources.Address()
                ,new IdentityResource("roles","Your Role (s)", new List<string>{"role"})
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("audiobooksapi","Access granted to Audiobooks Api")
            };

        public static IEnumerable<ApiResource> ApiResources =>
             new ApiResource[]
            {
                new ApiResource
                {
                    Name="audiobooksapi",
                    DisplayName="Audiobooks Api",
                    Scopes={ "audiobooksapi" },
                    UserClaims = new List<string>() { "role", "given_name" }
                }
            };



        public static IEnumerable<Client> Clients =>
            new Client[]
            {
            new Client
            {
                ClientName="Audiobooks Web",
                ClientId="audiobookwebclient",
                AllowedGrantTypes=GrantTypes.Code,
                RedirectUris=new List<string>{ "https://localhost:44317/signin-oidc" },
                PostLogoutRedirectUris=new List<string>{ "https://localhost:44317/signout-callback-oidc" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Address,
                    "roles",
                    "audiobooksapi"
                },
                ClientSecrets={new Secret("test_secret".Sha256())},
               // AlwaysIncludeUserClaimsInIdToken = true,// bad practice to turn this on as user claims will be send into ID_Token



            }

            };
    }
}