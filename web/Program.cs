using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models.Interfaces;
using web.Models.Services;

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

            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IHotel, HotelService>();
            builder.Services.AddTransient<IAmenity, AmenityService>();

            var app = builder.Build();
            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}