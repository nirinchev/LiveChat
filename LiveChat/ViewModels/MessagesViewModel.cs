using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using Xamarin.Forms;

namespace LiveChat
{
    public class MessagesViewModel : ViewModelBase
    {
        private readonly Realm _realm;
        private readonly string _currentUsername = UserDetails.Current.Username;

        public string Title { get; }

        public IEnumerable<Message> Messages { get; }

        public Message CurrentMessage { get; set; }

        public Command SendCommand { get; }

        public MessagesViewModel(Realm realm, string name)
        {
            Title = "Room: " + name;
            _realm = realm;

            Messages = _realm.All<Message>()
                             .Where(m => m.IsSent || m.Sender != _currentUsername)
                             .Where(m => !string.IsNullOrEmpty(m.Text))
                             .OrderByDescending(m => m.Date);

            CurrentMessage = _realm.All<Message>()
                                  .FirstOrDefault(m => !m.IsSent && m.Sender == _currentUsername);

            if (CurrentMessage == null)
            {
                _realm.Write(() =>
                {
                    CurrentMessage = _realm.Add(new Message
                    {
                        Sender = _currentUsername,
                        Date = DateTimeOffset.UtcNow
                    });
                });
            }

            SendCommand = new Command(Send);
        }

        private void Send()
        {
            _realm.Write(() =>
            {
                CurrentMessage.IsSent = true;
                CurrentMessage.Date = DateTimeOffset.UtcNow;

                CurrentMessage = _realm.Add(new Message
                {
                    Sender = _currentUsername,
                    Date = DateTimeOffset.UtcNow
                });
            });

            RaisePropertyChanged(nameof(CurrentMessage));
        }
    }
}
