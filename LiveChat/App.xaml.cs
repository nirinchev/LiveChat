using System;
using Realms.Sync;
using Xamarin.Forms;

namespace LiveChat
{
    public partial class App : Application
    {
        public static Action DisplayLogin { get; private set; }

        public App()
        {
            InitializeComponent();

            DisplayLogin = () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    MainPage = GetLoginPage();
                });
            };

            Page page;
            if (User.Current == null)
            {
                page = GetLoginPage();
            }
            else
            {
                page = GetRoomsPage(User.Current);
            }

            MainPage = page;
        }

        private Page GetRoomsPage(User user)
        {
            var roomsViewModel = new RoomsViewModel(user);
            return new NavigationPage(new RoomsPage(roomsViewModel));
        }

        private Page GetLoginPage()
        {
            var vm = new LoginViewModel
            {
                OnUserLoggedIn = user =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MainPage = GetRoomsPage(user);
                    });
                }
            };

            return new LoginPage(vm);
        }
    }
}
