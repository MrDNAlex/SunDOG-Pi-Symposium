using Radzen;
using SunDOG.Symposium.Pi.Display.Components;
using SunDOG.Symposium.Pi.Core;

namespace SunDOG.Symposium.Pi.Display
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSingleton<SensorDataStats>();
            builder.Services.AddHostedService<DataCollectionService>();
            
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<ContextMenuService>();

            // Give the first sensor the key "Sensor1"
            builder.Services.AddKeyedSingleton<INA219Service>("Sensor1", (sp, key) =>
            {
                return new INA219Service(1);
            });

            // Give the second sensor the key "Sensor7"
            builder.Services.AddKeyedSingleton<INA219Service>("Sensor2", (sp, key) =>
            {
                return new INA219Service(7);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
