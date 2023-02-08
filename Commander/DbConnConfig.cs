namespace Commander;

public class DbConnConfig
{
    private readonly WebApplicationBuilder builder;
    private readonly IConfiguration config;
    private string server;
    private string port;
    private string user;
    private string password;
    private string database;

    public string DbConnectionString { get; }

    public DbConnConfig(WebApplicationBuilder builder)
    {
        this.builder = builder;
        config = builder.Configuration;
        server = GetServer();
        port = GetPort();
        user = GetUser();
        password = GetPassword();
        database = GetDatabase();
        DbConnectionString = GetDbConn();
    }

    private string GetServer() {
        var defaultServer = @"ms-sql-server";
        //var defaultServer = @"host.docker.internal\ms-sql-server";
        //var defaultServer = "localhost";
        return //config["DbServer"] ?? 
            defaultServer;
    }

    private string GetPort() => config["DbPort"] ?? "1433";

    private string GetUser() => config["DbUser"] ?? "SA";

    private string GetPassword() => config["DbPassword"] ?? "";

    private string GetDatabase() => config["Database"] ?? "";

    public string GetDbConn() {
        return $"Data Source={server},{port}; Initial Catalog={database}; User Id ={user}; Password={password}";
    }
}