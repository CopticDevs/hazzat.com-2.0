﻿using HazzatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class SectionMenu : ContentPage
    {
        public SectionMenu(string Season, int SeasonId)
        {
            Title = Season;

            InitializeComponent();

            SubscribeMessage();

            App.NameViewModel.createViewModelBySeason(SeasonId);
        }

        private void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "Done", (sender) =>
            {
                if (App.NameViewModel?.HymnsBySeason != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StructList.ItemsSource = App.NameViewModel.HymnsBySeason;
                    });
                }
            });
        }
    }
}
