// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Idp
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("menuapi", "MENU API"),
                new ApiResource("infoapi", "INFO API"),
                new ApiResource("productapi", "PRODUCT API"),
                new ApiResource("orderapi", "ORDER API"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // authorization code
                new Client
                {
                    ClientId = "shop client",
                    ClientName = "Shop Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    ClientSecrets = { new Secret("demo secret".Sha256()) },

                    RedirectUris = { "http://localhost:10888/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:10888/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:10888/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 60, // 60 seconds

                    AllowedScopes = {
                        "menuapi",
                        "infoapi",
                        "productapi",
                        "orderapi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
            };
        }
    }
}