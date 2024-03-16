// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SparkNest.Services.IdentityServiceAPI
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_fakepayment")
            {
                Scopes={ "fakepayment_full_permission" }
            },
            new ApiResource("resource_order")
            {
                Scopes={"order_full_permission"}
            },
            new ApiResource("resource_product")
            {
                Scopes={"product_full_permission"}
            },
            new ApiResource("resource_basket")
            {
                Scopes={"basket_full_permission"}
            },
            new ApiResource("resource_file_stock")
            {
                Scopes={"file_stock_full_permission"}
            },
            new ApiResource("resource_discount")
            {
                Scopes={ "discount_full_permission" }
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.Email(),
                        new IdentityResources.OpenId(),
                        new IdentityResource(){Name = "roles",DisplayName="roles",Description="user roles",UserClaims=new []{"role"}},
                        new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("product_full_permission","full access for catalog api."),
                new ApiScope("file_stock_full_permission","full access for file stock api."),
                new ApiScope("basket_full_permission","full access for file basket api."),
                new ApiScope("discount_full_permission","full access for file basket api."),
                new ApiScope("order_full_permission","full access for file basket api."),
                new ApiScope("fakepayment_full_permission","full access for file basket api."),
                new ApiScope("gatewaymvc_full_permission","full access for file basket api."),

                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                //new ApiScope("scope1"),
                //new ApiScope("scope2"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "Credentials for SparkNest Clients MVC",
                    ClientId="SparkNest.Clients.MVC",
                    ClientSecrets = {new Secret("secret".Sha256()) },
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes = { "product_full_permission", "file_stock_full_permission","gatewaymvc_full_permission",IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client()
                {
                    ClientName = "Credentials for SparkNest Clients MVC",
                    ClientId="SparkNest.Clients.MVC.For.User",
                    AllowOfflineAccess =true,
                    ClientSecrets = {new Secret("secret".Sha256()) },
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "discount_full_permission", "order_full_permission","basket_full_permission", "fakepayment_full_permission", "gatewaymvc_full_permission", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess ,
                     "roles",
                        IdentityServerConstants.LocalApi.ScopeName },
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime  =(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage =TokenUsage.ReUse,

                }
            };
    }
}