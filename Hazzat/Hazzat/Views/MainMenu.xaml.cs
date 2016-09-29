﻿using hazzat.com;
using Hazzat.Views;
using HazzatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat
{
    public partial class MainMenu : TabbedPage
    {
        public MainMenu()
        {
            InitializeComponent();

            SubscribeMessage();

            App.NameViewModel.createSeasonsViewModel(true);
        }

        private void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "Done", (sender) =>
            {
                if (App.NameViewModel?.Seasons != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MenuStack.ItemsSource = App.NameViewModel.Seasons;
                    });
                }
            });
        }

        protected void OnToolbarItemClicked(object sender, EventArgs args) {
            ToolbarItem toolbarItem = (ToolbarItem)sender; DisplayAlert("Yo!","ToolbarItem '" + toolbarItem.Text + "' clicked","okay");
        }

        protected async void SeasonSelected(object sender, ItemTappedEventArgs e) {
            SeasonInfo item = (SeasonInfo) e.Item;

            await Navigation.PushAsync(new SectionMenu(item.Name, item.ItemId));
        }

    }
}

