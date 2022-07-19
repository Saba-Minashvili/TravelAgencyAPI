namespace Encoder.Abstraction
{
    public interface IEncodeService
    {
        string EncodeToBase64(string? toEncode);
        string DecodeFromBase64(string? toDecode);
    }
}
