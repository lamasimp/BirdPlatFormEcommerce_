namespace BirdPlatFormEcommerce.TokenService
{
    public class TokenBlackList : ITokenBlacklistService
    {
        private readonly List<string> _blacklist;

        public TokenBlackList()
        {
            _blacklist = new List<string>();
        }
        bool ITokenBlacklistService.IsrevokeToken(string token)
        {
            return _blacklist.Contains(token);
        }

        void ITokenBlacklistService.revokeToken(string token)
        {
            _blacklist.Add(token);
        }
    }
}
