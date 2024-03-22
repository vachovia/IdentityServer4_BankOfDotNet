//using AutoMapper;
//using BankOfDotNet.IdentityServer.ServerConfiguration;
//using IdentityServer4.EntityFramework.DbContexts;
//using IdentityServer4.EntityFramework.Mappers;
//using IdentityServer4Entities = IdentityServer4.EntityFramework.Entities;

//namespace BankOfDotNet.IdentityServer.Data
//{
//    public static class InitializeDatabase
//    {
//        private readonly IMapper _mapper;
//        private readonly IApplicationBuilder _app;

//        public InitializeDatabase(IApplicationBuilder app, IMapper mapper)
//        {
//            _app = app;
//            _mapper = mapper;
//            InitializeIdentityServerDb();
//        }

//        public void InitializeIdentityServerDb()
//        {
//            using (var scope = _app.ApplicationServices?.GetService<IServiceScopeFactory>()?.CreateScope())
//            {
//                // In case if we didn't do Update-Database
//                // scope?.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
//                var configContext = scope?.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
//                // configContext.Database.Migrate();

//                // Seed data

//                if (configContext != null)
//                {
//                    if (!configContext.ApiResources.Any())
//                    {
//                        foreach (var apiResource in Config.GetAllApiResources())
//                        {
//                            var entityApiResource = _mapper.Map<IdentityServer4Entities.ApiResource>(apiResource);
//                            configContext.ApiResources.Add(entityApiResource);
//                        }
//                        configContext.SaveChanges();
//                    }

//                    if (!configContext.ApiScopes.Any())
//                    {
//                        foreach (var apiScope in Config.GetApiScopes())
//                        {
//                            var entityApiScope = _mapper.Map<IdentityServer4Entities.ApiScope>(apiScope);
//                            configContext.ApiScopes.Add(entityApiScope);
//                        }
//                        configContext.SaveChanges();
//                    }

//                    if (!configContext.Clients.Any())
//                    {
//                        foreach (var client in Config.GetClients())
//                        {
//                            var entityClient = _mapper.Map<IdentityServer4Entities.Client>(client);
//                            configContext.Clients.Add(entityClient);
//                        }
//                        configContext.SaveChanges();
//                    }

//                    if (!configContext.IdentityResources.Any())
//                    {
//                        foreach (var identityResource in Config.GetIdentityResources())
//                        {
//                            var entityIdentityResource = _mapper.Map<IdentityServer4Entities.IdentityResource>(identityResource);
//                            configContext.IdentityResources.Add(entityIdentityResource);
//                        }
//                        configContext.SaveChanges();
//                    }
//                }
//            }
//        }
//    }
//}
