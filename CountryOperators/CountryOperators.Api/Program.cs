using CountryOperators.Data;
using CountryOperators.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace CountryOperators
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });

            // Load configuration
            var configuration = builder.Configuration;
            string databaseName = configuration.GetValue<string>("DatabaseConfig:DatabaseName");

            // Configure in-memory database
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName));

            // Adding Swagger documentation
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Country Operator", Version = "v1.0.0" });
            });

            var app = builder.Build();

            // Seed the database
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                SeedDatabase(dbContext);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void SeedDatabase(AppDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Countries.AddRange(
                new Country { Id = 1, CountryCode = "234", Name = "Nigeria", CountryIso = "NG" },
                new Country { Id = 2, CountryCode = "233", Name = "Ghana", CountryIso = "GH" },
                new Country { Id = 3, CountryCode = "229", Name = "Benin Republic", CountryIso = "BN" },
                new Country { Id = 4, CountryCode = "225", Name = "Côte d'Ivoire", CountryIso = "CIV" }
            );

            dbContext.CountryDetails.AddRange(
                new CountryDetails { Id = 1, CountryId = 1, Operator = "MTN Nigeria", OperatorCode = "MTN NG" },
                new CountryDetails { Id = 2, CountryId = 1, Operator = "Airtel Nigeria", OperatorCode = "ANG" },
                new CountryDetails { Id = 3, CountryId = 1, Operator = "9Mobile Nigeria", OperatorCode = "ETN" },
                new CountryDetails { Id = 4, CountryId = 1, Operator = "Globacom Nigeria", OperatorCode = "GLO NG" },
                new CountryDetails { Id = 5, CountryId = 2, Operator = "Vodafone Ghana", OperatorCode = "Vodafone GH" },
                new CountryDetails { Id = 6, CountryId = 2, Operator = "MTN Ghana", OperatorCode = "MTN Ghana" },
                new CountryDetails { Id = 7, CountryId = 2, Operator = "Tigo Ghana", OperatorCode = "Tigo Ghana" },
                new CountryDetails { Id = 8, CountryId = 3, Operator = "MTN Benin", OperatorCode = "MTN Benin" },
                new CountryDetails { Id = 9, CountryId = 3, Operator = "Moov Benin", OperatorCode = "Moov Benin" },
                new CountryDetails { Id = 10, CountryId = 4, Operator = "MTN Côte d'Ivoire", OperatorCode = "MTN CIV" }
            );

            dbContext.SaveChanges();
        }
    }
}
