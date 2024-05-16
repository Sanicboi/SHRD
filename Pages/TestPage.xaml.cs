using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using SHRD.Models;


namespace SHRD
{
    public sealed partial class TestPage : Page
    {
        public TestPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            currentTest = e.Parameter as Test;
            for (int i = 0; i < currentTest.tasks.Count; i++)
            {
                currentTest.tasks[i].answer = "";
            }
        }

        private async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var result = await TestController.SubmitStats(currentTest);
            var dialog = new ContentDialog()
            {
                Title = "Результат",
                MaxWidth = this.ActualWidth,
                Content = $"Вы прошли тест на {Math.Round(result.result, 2)* 100}%"
            };

            dialog.PrimaryButtonText = "На главную";
            dialog.IsPrimaryButtonEnabled = true;
            dialog.PrimaryButtonClick += delegate
            {
                Frame root = Window.Current.Content as Frame;
                root.Navigate(typeof(HomePage));
            };
            await dialog.ShowAsync();
        }
        public Test currentTest { get; set; }
    }
}
