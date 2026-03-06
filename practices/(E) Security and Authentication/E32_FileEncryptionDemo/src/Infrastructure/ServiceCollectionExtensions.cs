using FileEncryptionDemo.Services;

namespace FileEncryptionDemo.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCrypto(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<EncryptionOptions>()
            .Bind(config.GetSection(EncryptionOptions.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.KeyBase64), "Encryption:KeyBase64 is missing.")
            .ValidateOnStart();

        services.AddSingleton<IFileCryptoService, AesFileCryptoService>();

        return services;
    }
}