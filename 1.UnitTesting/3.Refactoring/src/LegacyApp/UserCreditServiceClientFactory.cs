namespace LegacyApp;

public interface IUserCreditServiceClientFactory
{
    public IUserCreditService CreateClient();
}

public class UserCreditServiceClientFactory : IUserCreditServiceClientFactory
{
    public IUserCreditService CreateClient()
    {
        return new UserCreditServiceClient();
    }
}
