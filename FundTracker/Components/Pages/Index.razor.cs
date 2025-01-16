using FundTracker.Model;
using Microsoft.AspNetCore.Components;

namespace FundTracker.Components.Pages
{
    public partial class Index : ComponentBase
    {
        [CascadingParameter]
        private StateUser _Status { get; set; }
        protected override void OnInitialized()
        {
            if (_Status.UserStatus == null)
            {
                Nav.NavigateTo("/login");
            }
            else
            {
                Nav.NavigateTo("/Dashboard");
            }
        }
    }
}