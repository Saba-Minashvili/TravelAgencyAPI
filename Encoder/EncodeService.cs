using Encoder.Abstraction;

namespace Encoder
{
    public class EncodeService : IEncodeService
    {
        public string DecodeFromBase64(string? toDecode)
        {
            if (string.IsNullOrEmpty(toDecode))
            {
                return "";
            }

            byte[] encodedStringAsBytes = System.Convert.FromBase64String(toDecode);
            string result = System.Text.ASCIIEncoding.ASCII.GetString(encodedStringAsBytes);

            return result;
        }

        public string EncodeToBase64(string? toEncode)
        {
            if (string.IsNullOrEmpty(toEncode))
            {
                return "";
            }

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string result = System.Convert.ToBase64String(toEncodeAsBytes);

            return result;
        }
    }
}
