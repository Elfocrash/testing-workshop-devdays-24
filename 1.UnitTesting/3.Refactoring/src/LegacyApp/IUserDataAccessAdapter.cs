namespace LegacyApp;

public interface IUserDataAccessAdapter
{
    void AddUser(User user);
}

public class UserDataAccessAdapter : IUserDataAccessAdapter
{
    public void AddUser(User user)
    {
        UserDataAccess.AddUser(user);
    }
}
