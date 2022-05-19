using Library.Data;

namespace Library.Web.Models
{
    public interface IAccountService
    {
        String? CurrentUserName { get; }

        Visitor? GetVisitor(String userName);

        Boolean Login(LoginViewModel user);

        Boolean Logout();

        Boolean Register(RegistrationViewModel visitor);
    }
}
