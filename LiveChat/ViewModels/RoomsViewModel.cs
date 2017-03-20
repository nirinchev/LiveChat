using System;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using Xamarin.Forms;

namespace LiveChat
{
    public class RoomsViewModel : ViewModelBase
    {
        private readonly User _user;

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Set(ref _name, value);
            }
        }

        public Command CreateCommand { get; }
        public Command JoinCommand { get; }
        public Command LogoutCommand { get; }

        public INavigation Navigation { get; set; }

        public RoomsViewModel(User user)
        {
            _user = user;
            JoinCommand = new Command(Join);
            CreateCommand = new Command(Create);
            LogoutCommand = new Command(Logout);
        }

        private void Logout()
        {
            _user.LogOut();
            App.DisplayLogin();
        }

        private void Join()
        {
            var url = $"realm://{UserDetails.Current.ServerUrl}:9080/rooms/{Name}";
            var realm = GetMessagesRealm(url);
            NavigateToMessages(realm);
        }

        private async void Create()
        {
            try
            {
                IsBusy = true;

                var url = $"realm://{UserDetails.Current.ServerUrl}:9080/rooms/{Name}";
                var realm = GetMessagesRealm(url);

                await GrantAccess(url);

                NavigateToMessages(realm);
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

        private Realm GetMessagesRealm(string url)
        {
            var config = new SyncConfiguration(_user, new Uri(url))
            {
                ObjectClasses = new[] { typeof(Message) }
            };

            return Realm.GetInstance(config);
        }

        private async Task GrantAccess(string url)
        {
            var mr = _user.GetManagementRealm();

            var change = new PermissionChange("*", url, mayRead: true, mayWrite: true, mayManage: false);

            var tcs = new TaskCompletionSource<object>();
            change.PropertyChanged += (sender, e) => 
            {
                if (e.PropertyName == nameof(PermissionChange.Status))
                {
                    tcs.TrySetResult(null);
                }
            };

            mr.Write(() => mr.Add(change));

            await tcs.Task;

            if (change.Status == ManagementObjectStatus.Error)
            {
                throw new Exception(change.StatusMessage);
            }
        }

        private void NavigateToMessages(Realm realm)
        {
            var vm = new MessagesViewModel(realm, Name);
            Navigation.PushAsync(new MessagesPage(vm));
        }
    }
}
