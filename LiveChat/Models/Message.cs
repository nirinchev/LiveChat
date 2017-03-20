using System;
using Realms;

namespace LiveChat
{
    public class Message : RealmObject
    {
        public DateTimeOffset Date { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
        public bool IsSent { get; set; }
    }
}
