// using Microsoft.EntityFrameworkCore;
    // using Microsoft.Extensions.Options;
    //
    // namespace IsItUpOrDown
    // {
    //     public class AppContext : DbContext
    //     {
    //         public AppContext(DbContextOptions<AppContext> options): base(options)
    //         {
    //         }
    //         //
    //         public DbSet<Check> Checks { get; set; }
    //         
    //         public static AppContext Injector()
    //         {
    //             var connectionString = "DataSource=app.db";
    //             var optionBuilder = new DbContextOptionsBuilder<AppContext>();
    //             optionBuilder.UseSqlite(connectionString); 
    //             return new AppContext(optionBuilder.Options);
    //         }
    //     }
    //     
    // }