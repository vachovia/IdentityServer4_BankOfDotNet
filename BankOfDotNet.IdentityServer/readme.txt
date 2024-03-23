// InMemory means for development
// correct way is to use EF
//builder.Services.AddIdentityServer()
//    .AddDeveloperSigningCredential()
//    .AddInMemoryIdentityResources(Config.GetIdentityResources) // for implicit flow
//    .AddInMemoryApiScopes(Config.GetApiScopes)
//    .AddInMemoryApiResources(Config.GetAllApiResources)
//    .AddInMemoryClients(Config.GetClients)
//    .AddTestUsers(Config.GetUsers());

//builder.Services.AddIdentityServer()
//    .AddTestUsers(Config.GetUsers())
//    //.AddAspNetIdentity<IdentityUser>()    
//    .AddConfigurationStore(options =>
//    { // clients and resources
//        options.ConfigureDbContext = b => b.UseSqlServer(
//            dbConnectionString,
//            opt => opt.MigrationsAssembly(assembly)
//        );
//    })
//    .AddOperationalStore(options =>
//    { // tokens, consents, codes etc.
//        options.ConfigureDbContext = b => b.UseSqlServer(
//            dbConnectionString,
//            opt => opt.MigrationsAssembly(assembly)
//        );
//    }).AddDeveloperSigningCredential();


// Add-Migration InitialIdentityServerMigration -c PersistedGrantDbContext => Update-Database -Context PersistedGrantDbContext
// Added two tables: DeviceCodes and PesistedGrants
// Add-Migration InitialIdentityServerMigration -c ConfigurationDbContext => Update-Database -Context ConfigurationDbContext
// Added two tables: DeviceCodes and PesistedGrants
// Add-Migration AppIdentityUserRoleMigration -c AppDbContext => Update-Database -Context AppDbContext