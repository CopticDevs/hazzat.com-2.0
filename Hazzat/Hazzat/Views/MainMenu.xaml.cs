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

            SubscribeMessages();

            App.NameViewModel.createSeasonsViewModel(true);
            App.NameViewModel.GetHymnsByType();
            App.NameViewModel.GetHymnsByTune();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<ByNameMainViewModel>(this, "Done");
        }

        private void SubscribeMessages()
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

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithTypeList", (sender) =>
            {
                if (App.NameViewModel?.TypeList != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MenuStack2.ItemsSource = App.NameViewModel.TypeList;
                    });
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithTuneList", (sender) =>
            {
                if (App.NameViewModel?.TuneList != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MenuStack3.ItemsSource = App.NameViewModel.TuneList;
                    });
                }
            });
        }

        protected void SeasonSelected(object sender, ItemTappedEventArgs e)
        {
            SeasonInfo item = (SeasonInfo)e.Item;
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, item.ItemId, "Season");
        }
        protected void TypeSelected(object sender, ItemTappedEventArgs e)
        {
            //Unfortunate type name collision
            hazzat.com.TypeInfo item = (hazzat.com.TypeInfo)e.Item;
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, item.ItemId, "Type");
        }
        protected void TuneSelected(object sender, ItemTappedEventArgs e)
        {
            TuneInfo item = (TuneInfo)e.Item;
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, item.ItemId, "Tune");
        }
    }
}

