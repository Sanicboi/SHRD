﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.Json;
using System.Text;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SHRD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignupPage : Page
    {

        public SignupPage()
        {
            this.InitializeComponent();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await AuthorizationController.Signup(Username.Text, Password.Password);
                Frame root = Window.Current.Content as Frame;
                root.Navigate(typeof(HomePage));
            } catch (Exception ex) {
                var dialog = new ContentDialog()
                {
                    Title = "Ошибка",
                    MaxWidth = this.ActualWidth,
                    Content = "Произошла ошибка авторизации. Попробуйте ещё раз."
                };

                dialog.PrimaryButtonText = "OK";
                dialog.IsPrimaryButtonEnabled = true;
                dialog.PrimaryButtonClick += delegate
                {

                };
                await dialog.ShowAsync();
            }
        }
    }
}
