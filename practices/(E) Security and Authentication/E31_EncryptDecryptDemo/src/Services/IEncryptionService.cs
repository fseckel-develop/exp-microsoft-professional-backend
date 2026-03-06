namespace EncryptDecryptDemo.Services;

public interface IEncryptionService
{
    string EncryptToBase64(string plaintext);
    string DecryptFromBase64(string payloadBase64);
}