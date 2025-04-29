
using LudoAPI.Models;
using LudoAPI.Repositories;
using LudoAPI.Services;

namespace LudoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDiceService, DiceService>();
            
            builder.Services.AddSingleton<ILobbyRepository, LobbyRepository>();
            builder.Services.AddScoped<ILobbyService, LobbyService>();
            
            builder.Services.AddScoped<IBoardService, BoardService>();
            
            builder.Services.AddScoped<IStartingService, StartingService>();

            builder.Services.AddSingleton<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IGameService, GameService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
