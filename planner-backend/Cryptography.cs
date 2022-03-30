using System.Security.Cryptography;

namespace planner_backend
{
  public class Cryptography
  {
    public static byte[] ConvertToSHA256(string _input)
    {
      var sha = SHA256.Create();
      byte[] data = new byte[_input.Length];

      for(int i = 0; i < _input.Length; i++)
      {
        data[i] = (byte)_input[i];
      }

      byte[] sha256hash = sha.ComputeHash(data);

      return sha256hash;
    }
  }
}
