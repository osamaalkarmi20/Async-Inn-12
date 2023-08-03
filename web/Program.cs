using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models.Interfaces;
using web.Models.Services;

namespace web
{
    public class Program
    {
        //   public static void Main(string[] args)
        //   {
        //       var builder = WebApplication.CreateBuilder(args);
        //       builder.Services.AddControllers();
        //       string connString = builder.Configuration.GetConnectionString("DefaultConnection");
        //       builder.Services.AddDbContext<AsyncInnDbContext>(options=> options.UseSqlServer(connString));
        //       builder.Services.AddControllers().AddNewtonsoftJson(options =>
        //  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //);
        //       builder.Services.AddTransient<IRoom, RoomService>();
        //       builder.Services.AddTransient<IHotel, HotelService>();
        //       builder.Services.AddTransient<IAmenity, AmenityService>();
        //       builder.Services.AddTransient<IHotelRoom, HotelRoomService>();
        //       var app = builder.Build();
        //       app.MapControllers();

        //       app.MapGet("/", () => "Hello World!");

        //       app.Run();
        //   }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AsyncInnDbContext>(options => options.UseSqlServer(connString));

            builder.Services.AddTransient<IHotel, HotelServices>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IAmenity, AmenityService>();
            builder.Services.AddTransient<IHotelRoom, HotelRoomService>();

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddNewtonsoftJson(
        option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

            var app = builder.Build();
            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}