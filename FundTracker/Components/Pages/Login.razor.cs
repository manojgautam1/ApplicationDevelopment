using Microsoft.AspNetCore.Components;
using FundTracker.Model;
using FundTracker.Services;

namespace FundTracker.Components.Pages
{
    public partial class Login
    {

        private User Users { get; set; } = new();

        private string ErrorMessage { get; set; } = string.Empty;

        [CascadingParameter]
        private UserState _LiveState { get; set; }


        private void HandleLogin()
        {
            _LiveState.LiveUser = Users;
            if (UserService.Login(Users))
            {
                ErrorMessage = "User registered successfully!";
                Nav.NavigateTo("/home");
            }
            else
            {
                ErrorMessage = "Username or password is invalid";
            }
        }
    }
}
