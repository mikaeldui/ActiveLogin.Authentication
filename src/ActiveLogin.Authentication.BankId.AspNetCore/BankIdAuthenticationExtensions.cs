﻿using System;
using ActiveLogin.Authentication.BankId.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace ActiveLogin.Authentication.BankId.AspNetCore
{
    public static class BankIdAuthenticationExtensions
    {
        public static BankIdAuthenticationBuilder AddBankId(this AuthenticationBuilder builder)
        {
            return AddBankId(
                builder,
                BankIdAuthenticationDefaults.AuthenticationScheme,
                BankIdAuthenticationDefaults.DisplayName,
                options => { }
            );
        }

        public static BankIdAuthenticationBuilder AddBankId(this AuthenticationBuilder builder, string authenticationScheme)
        {
            return AddBankId(
                builder,
                authenticationScheme,
                BankIdAuthenticationDefaults.DisplayName,
                options => { }
            );
        }

        public static BankIdAuthenticationBuilder AddBankId(this AuthenticationBuilder builder, string authenticationScheme, string displayName)
        {
            return AddBankId(
                builder,
                authenticationScheme,
                displayName,
                options => { }
            );
        }

        public static BankIdAuthenticationBuilder AddBankId(this AuthenticationBuilder builder, Action<BankIdAuthenticationOptions> configureOptions)
        {
            return AddBankId(
                builder,
                BankIdAuthenticationDefaults.AuthenticationScheme,
                BankIdAuthenticationDefaults.DisplayName,
                configureOptions
            );
        }

        public static BankIdAuthenticationBuilder AddBankId(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<BankIdAuthenticationOptions> configureOptions)
        {
            AddBankIdServices(builder.Services);

            builder.AddScheme<BankIdAuthenticationOptions, BankIdAuthenticationHandler>(
                authenticationScheme,
                displayName,
                configureOptions
            );

            return new BankIdAuthenticationBuilder(builder, authenticationScheme);
        }

        private static void AddBankIdServices(IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<BankIdAuthenticationOptions>, BankIdAuthenticationPostConfigureOptions>());

            services.TryAddSingleton<IBankIdOrderRefProtector, BankIdOrderRefProtector>();
            services.TryAddSingleton<IBankIdLoginResultProtector, BankIdLoginResultProtector>();
        }
    }
}