using FluentAssertions;
using LogiTrack.Api.Services.Email;

namespace LogiTrack.Api.Tests.Services.Email;

public class MockEmailSenderTests
{
    [Fact]
    public async Task SendEmailAsync_ShouldCompleteWithoutThrowing()
    {
        var sender = new MockEmailSender();

        var act = async () => await sender.SendEmailAsync(
            "test@example.com",
            "Test Subject",
            "<p>Hello</p>");

        await act.Should().NotThrowAsync();
    }
}