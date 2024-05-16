using SHRD.Controllers;
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
using Windows.UI.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SHRD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TheoryCreate : Page
    {
        public TheoryCreate()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string txt;
                string c;
                switch (course.SelectedItem as String)
                {
                    case "Алгебра":
                        c = "algebra";
                        break;
                    case "Геометрия":
                        c = "geometry";
                        break;
                    default:
                        c = "statistics";
                        break;
                }
                text.TextDocument.GetText(TextGetOptions.NoHidden, out txt);
                await TheoryController.CreateTheory(name.Text, txt, c);
                Frame root = Window.Current.Content as Frame;
                root.Navigate(typeof(Profile));
            } catch (Exception ex)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Ошибка",
                    MaxWidth = this.ActualWidth,
                    Content = "Произошла ошибка. Попробуйте ещё раз."
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
