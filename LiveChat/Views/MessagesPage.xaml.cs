using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LiveChat
{
    public partial class MessagesPage : ContentPage
    {
        public MessagesPage(MessagesViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
