using Veloci.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureBuilder(builder);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app);

try
{
    await DefaultInit.InitializeAsync(builder.Configuration, app);
    app.Run();
}
catch (Exception ex)
{
    // ignored
}
finally
{
}