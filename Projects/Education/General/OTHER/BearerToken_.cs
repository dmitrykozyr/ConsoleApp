using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Education.General;

public class BearerToken_
{
    public void Main_()
    {
        var randomNumber = new Random(42);
        var oneTimeKey = new byte[32];
        var tokenHandler = new JwtSecurityTokenHandler();
        randomNumber.NextBytes(oneTimeKey);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(oneTimeKey),
            SecurityAlgorithms.HmacSha256Signature);

        var token = tokenHandler.CreateJwtSecurityToken(
            issuer: "wx_api",
            audience: "wx_b2c_subscription",
            subject: new ClaimsIdentity(),
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: signingCredentials);

        token.Payload["OwnerId"] = new Guid("FCD2EFC9-518E-419F-A1AA-61E220932A98");
        var bearerToker = "Bearer " + tokenHandler.WriteToken(token);
    }
}
