using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend.Helpers;

public class JWT
{
    public string Key { get; set; }
    public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
    public SecurityKey SecurityKey => new SymmetricSecurityKey(KeyBytes);

}