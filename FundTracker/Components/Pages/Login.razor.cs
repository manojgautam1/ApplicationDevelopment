using FundTracker.Model;
using Microsoft.AspNetCore.Components;

namespace FundTracker.Components.Pages
{
    public partial class Login
    {

        private User Users { get; set; } = new();

        private string ErrorMessage { get; set; } = string.Empty;

        [CascadingParameter]
        private StateUser _Status { get; set; }


        private void HandleLogin()
        {
            
            _Status.UserStatus = Users;
            if (UserService.Login(Users))
            {
                ErrorMessage = "User Login is successfull";
                Nav.NavigateTo("/dashboard");
            }
            else
            {
                ErrorMessage = "Username or password is invalid";
            }
        }
    }
}