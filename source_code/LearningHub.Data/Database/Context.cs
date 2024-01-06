namespace LearningHub.Data.Database;

public class Context(IConfiguration configuration) : DbContext
{
    public virtual DbSet<SuccessLog> SuccessLog => Set<SuccessLog>();

    public virtual DbSet<FailureLog> FailureLog => Set<FailureLog>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var databaseUrl = configuration["DATABASE_URL"];

        var databaseUri = new Uri(databaseUrl ?? throw new InvalidOperationException());
        var userInfo = databaseUri.UserInfo.Split(':');

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/')
        };

        optionsBuilder.UseNpgsql(builder.ToString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}
