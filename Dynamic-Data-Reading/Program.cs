using API.StartupConfiguration;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddConnection();
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseConnection();
        }
    }
}
