using Microsoft.EntityFrameworkCore;
using web.Data;

namespace web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AsyncInnDbContext>(options=> options.UseSqlServer(connString));
           
            var app = builder.Build();
            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}