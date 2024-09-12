using Newtonsoft.Json.Linq;

namespace API.StartupConfiguration
{
    public static class StartupConfiguration
    {
        public static void AddConnection(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddHttpClient();
        }

        public static void UseConnection(this WebApplication app)
        {
            app.MapPost("/user", async (HttpRequest httpRequest) =>
            {
                // Gelen isteğin Body'sini oku
                var requestBody = await new StreamReader(httpRequest.Body).ReadToEndAsync();

                // İstekten JSON verisini deserialize et
                var jsonObject = JObject.Parse(requestBody);

                // Modelde tanımlı alanlar
                var name = jsonObject["name"]?.ToString();
                var surname = jsonObject["surname"]?.ToString();


                // Dinamik olarak gelen tüm diğer alanlar
                Dictionary<string, JToken> additionalFields = jsonObject
                    .Properties()
                    .Where(p => p.Name != "name" && p.Name != "surname")
                    .ToDictionary(p => p.Name, p => p.Value);
                
                //JsonSerializer.Deserialize();

                return Results.Ok(new { Name = name, Surname = surname, AdditionalFields = additionalFields });
            });

            app.Run();
        }
    }
}
