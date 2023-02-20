using System.Security.Claims;

namespace IMS_Example.Helpers
{
    public class TokenHelper
    {
        public class Token
        {
            public int User { get; set; }
            public int Group { get; set; }
        }

        public static Token GetUserId(ClaimsPrincipal user)
        {
            Token token = new Token();
            // return 0 if can't get id user in token
            var claimUser = user.Claims.FirstOrDefault(x => x.Type.ToString().Equals("id", StringComparison.InvariantCultureIgnoreCase));

            var claimGroup = user.Claims.FirstOrDefault(x => x.Type.ToString().Equals("IdGroup", StringComparison.InvariantCultureIgnoreCase));

            if(claimUser != null)
            {
                token.User = int.Parse(claimUser.Value);
            }
            else
            {
                token.User = 0;
            }

            if (claimGroup != null)
            {
                token.Group = int.Parse(claimGroup.Value);
            }
            else
            {
                token.Group = 0;
            }
            return token;
        }

    }
}
