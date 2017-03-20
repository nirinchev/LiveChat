using System;
using Realms.Sync;
using Xamarin.Forms;

namespace LiveChat
{
    public class LoginViewModel : ViewModelBase
    {
        public Action<User> OnUserLoggedIn { get; set; }

        public UserDetails Details { get; } = UserDetails.Current;

        public string Password { get; set; }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
        }

        private async void Login()
        {
            try
            {
                IsBusy = true;

                var credentials = Credentials.UsernamePassword(Details.Username, Password, false);
                var user = await User.LoginAsync(credentials, new Uri($"http://{Details.ServerUrl}:9080/"));

                OnUserLoggedIn(user);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
