using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LiveChat
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
