namespace SecureDataStorageDemo.Services;

public interface IMessageCryptoService
{
    string EncryptToBase64(string plaintext);
    string DecryptFromBase64(string payloadBase64);
}