﻿using Mobile.Client.Configuration;
using Xamarin.Forms;

namespace Mobile.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var bootstrapper = new Bootstrapper(this);
            bootstrapper.Run();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}