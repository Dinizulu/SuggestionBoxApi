using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SuggestionBoxApi.Models.JWT
{
    public class JwtSettings
    {
        public string SecreteKey { get; set; } = string.Empty;
        public string Issuer {  get; set; } = string.Empty;
        public string Audience {  get; set; } = string.Empty;

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecreteKey));
        }
    }
}
