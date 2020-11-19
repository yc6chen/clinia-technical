using MartinCostello.Logging.XUnit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using TechnicalTest.Project;
using Xunit.Abstractions;

namespace TechnicalTest.Tests
{
    /// <summary>
    /// A test fixture representing an HTTP server hosting the sample application. This class cannot be inherited.
    /// </summary>
    public class HttpServerFixture : WebApplicationFactory<Startup>, ITestOutputHelperAccessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpServerFixture"/> class.
        /// </summary>
        public HttpServerFixture()
            : base()
        {
        }
        
        public ITestOutputHelper OutputHelper { get; set; }

        /// <inheritdoc />
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureLogging((p) =>
            {
                p.ClearProviders();
                p.AddXUnit(this);
            });
        }
    }
}