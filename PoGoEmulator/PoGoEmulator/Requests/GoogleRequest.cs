using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator.Requests
{
    /// <summary>
    /// is it necessary or not , i dont know much more about JWT validation 
    /// </summary>
    public static class GoogleRequest
    {
        public static bool IsValidToken(RequestEnvelope.Types.AuthInfo auth)
        {
            if (auth.Provider != "google") new Exception("not implemented yet");//login with username and password is not implemented

            JwtSecurityTokenHandler jwth = new JwtSecurityTokenHandler();
            var userJwtToken = jwth.ReadJwtToken(auth.Token.Contents).Payload;
            object userEmail;
            userJwtToken.TryGetValue("email", out userEmail);
            string cachedJwtToken;
            var isExists = RequestHandler.AuthedUserTokens.TryGetValue(userEmail.ToString(), out cachedJwtToken);
            if (isExists)
            {
                if (cachedJwtToken == auth.Token.Contents)
                    return true;
            }
            return GoogleValidator(userEmail.ToString(), auth.Token.Contents);
        }

        private static bool GoogleValidator(string userEmail, string userJwtToken)
        {
            var request = WebRequest.Create($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={userJwtToken}");
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var googleJwtToken = JsonConvert.DeserializeObject<JwtPayload>(reader.ReadToEnd());
            object googleEmail;
            googleJwtToken.TryGetValue("email", out googleEmail);
            if (userEmail.ToString() == googleEmail.ToString())
            {
                if (RequestHandler.AuthedUserTokens.AddOrUpdate(googleEmail.ToString(), userJwtToken,
                        (key, oldValue) => userJwtToken) != null)
                    return true;
            }
            throw new Exception($"INVALID TOKEN DETECTED..!!! requestedUserEmail: {userEmail} googleEmail: {googleEmail}");
        }
    }
}