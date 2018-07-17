using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TicketTrader.Identity.Data
{
    public static class ApplicationDbInitializer
    {
        public static void InitializeDatabase(IApplicationBuilder app, IConfigurationRoot configuration, IHostingEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                InitializePersistedGrantDbContext(scope, configuration, env);

                InitializeConfigurationDbContext(scope, configuration, env);

                InitializeApplicationDbContext(scope, configuration, env);
            }
        }

        private static void InitializePersistedGrantDbContext(IServiceScope scope, IConfigurationRoot configuration, IHostingEnvironment env)
        {
            PersistedGrantDbContext persistedGrantDb = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();

            if (env.IsDevelopment())
                persistedGrantDb.Database.EnsureDeleted();

            persistedGrantDb.Database.Migrate();
        }

        private static void InitializeConfigurationDbContext(IServiceScope scope, IConfigurationRoot configuration, IHostingEnvironment env)
        {
            var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                foreach (var client in SeedClientStore.Get(configuration))
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in SeedIdentityResouces.Get())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in SeedApiResources.Get())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
        private static void InitializeApplicationDbContext(IServiceScope scope, IConfigurationRoot configuration, IHostingEnvironment env)
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            var manager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if (!manager.Users.Any())
            {
                foreach (var user in SeedUsers.Get())
                {
                    var newUser = new IdentityUser(user.Username) {Id = user.SubjectId};

                    var result = manager.CreateAsync(newUser, user.Password).GetAwaiter().GetResult();

                    if (result.Succeeded == false)
                    {
                        throw new AggregateException("Seeding user failed because of errors",
                            result.Errors.Select(x => new Exception(x.Description)));
                    }

                    result = manager.AddClaimsAsync(newUser, user.Claims).GetAwaiter().GetResult();

                    if (result.Succeeded == false)
                    {
                        throw new AggregateException("Seeding user claims failed because of errors",
                            result.Errors.Select(x => new Exception(x.Description)));
                    }
                }
            }
        }
    }
}
