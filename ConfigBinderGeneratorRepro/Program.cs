namespace ConfigBinderGeneratorRepro;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        var configurationSection = builder.Configuration.GetSection("AppConfiguration");
        builder.Services.Configure<AppConfiguration>(configurationSection);

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
