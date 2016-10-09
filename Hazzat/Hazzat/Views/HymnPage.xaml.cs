﻿using Hazzat.Abstract;
using HazzatService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class HymnPage : TabbedPage
    {
        private const string HtmlHeaderFormatString = @"
                                  <html><body>
                                    <style>
                                        body
                                        {{ 
                                            background-color: rgb({0}); 
                                            color: rgb({1});
                                        }}
                                   </style>
                                    <link href=""Fonts/fonts.css"" rel=""stylesheet"" type=""text/css"" />
";

        public int HymnID;

        public HymnPage(string breadcrumbName, string HymnName, int HymnId)
        {
            InitializeComponent();

            Title = $"{breadcrumbName} - {HymnName}";

            this.HymnID = HymnId;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            IReadOnlyList<Page> navStack = Navigation.NavigationStack;

            Navigation.RemovePage(navStack[1]);
        }

        public void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithHymnText", (sender) =>
            {
                if (App.NameViewModel?.HymnContentInfo != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();
                    var html = new StringBuilder();
                    source.BaseUrl = DependencyService.Get<IWebAssets>().Get();

                    string foreground = DependencyService.Get<IColorRender>().GetDefault();
                    string background = DependencyService.Get<IColorRender>().GetBackground();
                    //Append Coptic Font css
                    html.Append(string.Format(HtmlHeaderFormatString, background, foreground));

                    foreach (var hymnContent in App.NameViewModel.HymnContentInfo)
                    {
                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_English))
                        {
                            html.Append(@"<div><font class=""EnglishFont"">");
                            html.Append(hymnContent.Content_English);
                            html.Append(@"</font></div>");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Coptic))
                        {
                            html.Append(@"<div><font class=""CopticFont"">");
                            html.Append(hymnContent.Content_Coptic);
                            html.Append(@"</font></div>");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Arabic))
                        {
                            html.Append(@"<div><font class=""ArabicFont"">");
                            html.Append(hymnContent.Content_Arabic);
                            html.Append(@"</font></div>");
                        }

                        // hymn content separator
                        html.Append("<br /><br />");
                        html.Append("<br /><br />");
                        html.Append("<hr /><br />");
                    }

                    source.Html = html.Append("</body></html>").ToString();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TextWebView.Source = source;
                    });
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithHazzat", (sender) =>
            {
                if (App.NameViewModel?.HazzatHymnContentInfo != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();

                    var html = new StringBuilder();
                    source.BaseUrl = DependencyService.Get<IWebAssets>().Get();

                    string foreground = DependencyService.Get<IColorRender>().GetDefault();
                    string background = DependencyService.Get<IColorRender>().GetBackground();
                    //Append Coptic Font css
                    html.Append(string.Format(HtmlHeaderFormatString, background, foreground));

                    foreach (var hymnContent in App.NameViewModel.HazzatHymnContentInfo)
                    {
                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_English))
                        {
                            html.Append(hymnContent.Content_English);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Coptic))
                        {
                            html.Append(hymnContent.Content_Coptic);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Arabic))
                        {
                            html.Append(hymnContent.Content_Arabic);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        // hymn content separator
                        html.Append("<br /><br />");
                        html.Append("<br /><br />");
                        html.Append("<hr /><br />");
                    }
                    source.Html = html.Append("</body></html>").ToString();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        HazzatWebView.Source = source;
                    });
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithVerticalHazzat", (sender) =>
            {
                if (App.NameViewModel?.VerticalHazzatHymnContent != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();

                    var html = new StringBuilder();

                    string foreground = DependencyService.Get<IColorRender>().GetDefault();
                    string background = DependencyService.Get<IColorRender>().GetBackground();

                    html.Append(string.Format(HtmlHeaderFormatString, background, foreground));

                    foreach (var hymnContent in App.NameViewModel.VerticalHazzatHymnContent)
                    {
                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_English))
                        {
                            html.Append(hymnContent.Content_English);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Coptic))
                        {
                            html.Append(hymnContent.Content_Coptic);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Arabic))
                        {
                            html.Append(hymnContent.Content_Arabic);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        // hymn content separator
                        html.Append("<br /><br />");
                        html.Append("<br /><br />");
                        html.Append("<hr /><br />");
                    }

                    source.Html = html.ToString();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        VerticalHazzatWebView.Source = source;
                    });
                }
            });
        }
    }
}
