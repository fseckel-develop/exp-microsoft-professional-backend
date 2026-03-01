using SecureJsonDemo.Models;
using SecureJsonDemo.Presentation;
using SecureJsonDemo.Services;

namespace SecureJsonDemo;

internal static class Program
{
    private static void Main()
    {
        var writer = new ConsoleWriter();
        var securePackageService = new SecurePackageService();

        var package = new DocumentPackage
        {
            DocumentName = "Onboarding-Notes.txt",
            Recipient = "team@company.example",
            Contents = "Here are the onboarding steps for the new hire..."
        };

        writer.WriteTitle("Secure Packaging Demo");

        var serialized = securePackageService.SerializePackage(package);
        writer.WriteSerializedPackage(serialized);

        var restored = securePackageService.DeserializePackage(serialized, isTrustedSource: true);

        if (restored is not null)
        {
            writer.WriteRestoredPackage(restored);
        }
        else
        {
            writer.WriteMessage("Deserialization failed for trusted source.");
        }

        var blocked = securePackageService.DeserializePackage(serialized, isTrustedSource: false);
        writer.WriteBlockedResult(blocked is null);
    }
}
