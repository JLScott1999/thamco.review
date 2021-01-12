namespace ThAmCo.Review
{
    using System;
    using System.Net.Http;
    using System.Net.Sockets;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Polly;
    using Polly.Extensions.Http;
    using ThAmCo.Review.Data;
    using ThAmCo.Review.Repositories.Review;
    using ThAmCo.Review.Services.Order;
    using ThAmCo.Review.Services.Review;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup database connection
            string databaseConnectionURL = this.Configuration.GetConnectionString("DatabaseConnection");
            if (string.IsNullOrWhiteSpace(databaseConnectionURL))
            {
                services.AddTransient<IReviewRepository, FakeReviewRepository>();
            }
            else
            {
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(databaseConnectionURL)
                );
                services.AddTransient<IReviewRepository, ReviewRepository>();
            }

            // Setup Service Layer
            services.AddTransient<IReviewService, ReviewService>();

            string orderServiceURL = this.Configuration["Services:OrderService:URL"];
            if (string.IsNullOrWhiteSpace(orderServiceURL))
            {
                services.AddTransient<IOrderService, FakeOrderService>();
            }
            else
            {
                services.AddHttpClient<IOrderService, OrderService>(client =>
                {
                    client.BaseAddress = new Uri(orderServiceURL);
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());
            }

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            Random jitterer = new Random();
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<SocketException>()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3,
                    retryAttempt =>
                    {
                        var waitTime = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 100));
                        Console.WriteLine($"Retry attempt {retryAttempt} waiting {waitTime.TotalSeconds} seconds");
                        return waitTime;
                    }
                );
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<SocketException>()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30),
                onBreak: (exception, timeSpan) =>
                {
                    Console.WriteLine("Breaking circuit for " + timeSpan.TotalSeconds + "seconds due to " + exception.Exception.Message);
                },
                onReset: () =>
                {
                    Console.WriteLine("Trial call succeeded: circuit closing again.");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("Circuit break time elapsed. Circuit now half open: permitting a trial call.");
                });
        }

    }
}
