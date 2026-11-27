using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace TuCuota.IntegrationTests;

public sealed class TechnicalObservabilityTests
{
    [Fact]
    [Trait("ADR", "ADR-0013")]
    public async Task ApiPreservesSafeCorrelationIdentifier()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/system");
        request.Headers.Add("X-Correlation-ID", "r1-observability-test");

        using var response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("r1-observability-test", response.Headers.GetValues("X-Correlation-ID").Single());
    }

    [Fact]
    [Trait("ADR", "ADR-0013")]
    public async Task ApiReplacesUnsafeCorrelationIdentifier()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/system");
        request.Headers.Add("X-Correlation-ID", "unsafe value with spaces");

        using var response = await client.SendAsync(request, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var correlationId = response.Headers.GetValues("X-Correlation-ID").Single();
        Assert.Matches("^[a-f0-9]{32}$", correlationId);
    }
}
