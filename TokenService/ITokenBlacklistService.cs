namespace BirdPlatFormEcommerce.TokenService
{
    public interface ITokenBlacklistService
    {
        void revokeToken(string token);
        bool IsrevokeToken(string token);
    }
}
