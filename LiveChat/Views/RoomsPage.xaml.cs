using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LiveChat
{
    public partial class RoomsPage : ContentPage
    {
        public RoomsPage(RoomsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}
