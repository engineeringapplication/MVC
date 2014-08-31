namespace MVCEngineeringSystemApplication.Interfaces
{
    interface ILogin
    {
        User ValidateLogin(string username, string password);
    }
}
