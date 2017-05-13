﻿using hazzat.com;
using Hazzat.HazzatService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hazzat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMenu : MasterDetailPage
    {
        public static SectionMenu Menu { get; set; }

        public MasterDetailMenu(string Season, int SeasonId)
        {
            InitializeComponent();

            MessagingCenter.Subscribe<MainMenu>(this, "MenuItemSelected", HideMasterPage);

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "Loading", ShowReload);

            Menu = new SectionMenu(Season, SeasonId, NavigationType.Season);

            Detail = new NavigationPage(Menu);
        }

        private void ShowReload(ByNameMainViewModel obj)
        {
            MessagingCenter.Unsubscribe<ByNameMainViewModel>(this, "Loading");

            Timer time = new Timer(Reload, null, 10000, Timeout.Infinite);

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "Loading", ShowReload);
        }

        private void Reload(object state)
        {
            if (!App.IsLoaded)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    StackLayout layout = new StackLayout()
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    };

                    Button btn = new Button()
                    {
                        Text = "Retry",
                        TextColor = Color.Blue,
                    };

                    layout.Children.Add(new Label()
                    {
                        Text = "No Internet Connection"
                    });

                    btn.Clicked += Reset;

                    layout.Children.Add(btn);
                    Detail = new ContentPage()
                    {
                        Content = layout
                    };
                });
            }
        }

        private void Reset(object sender, EventArgs e)
        {
            Detail = new NavigationPage(Menu);
        }

        public void HideMasterPage(MainMenu obj)
        {
            if (Device.OS != TargetPlatform.Windows)
            {
                IsPresented = false;
            }
        }

        protected void OnToolbarItemClicked(object sender, EventArgs args)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender; DisplayAlert("Hi!", "ToolbarItem '" + toolbarItem.Text + "' clicked", "okay");
        }
    }
}
