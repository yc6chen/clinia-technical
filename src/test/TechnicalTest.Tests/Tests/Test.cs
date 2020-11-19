using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechnicalTest.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace TechnicalTest.Tests.Tests
{
    public class Test : IClassFixture<HttpServerFixture>, IDisposable
    {
        private readonly HttpServerFixture _fixture;
        private readonly TestDbContext _dbContext;

        public Test(HttpServerFixture fixture, ITestOutputHelper outputHelper)
        {
            _fixture = fixture;
            _fixture.OutputHelper = outputHelper;

            _dbContext = _fixture.Services.GetService<TestDbContext>();
        }
        
        public void Dispose()
        {
            _fixture.OutputHelper = null;
        }

        [Fact]
        public async Task ScenarioOne_()
        {
            var healthFacilityCount = await _dbContext.HealthFacilities.CountAsync();
            var practitioners = await _dbContext.Practitioners.CountAsync();
            var services = await _dbContext.Services.CountAsync();
            
            Assert.Equal(50, healthFacilityCount);
        }
    }
}