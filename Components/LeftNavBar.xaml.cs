using System;
using System.Collections.Generic;
using System.Drawing;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SHRD
{
    public sealed partial class LeftNavBar : UserControl
    {
        private TextBlock GetBtn()
        {
            switch (_tabIdx)
            {
                case 1:
                    return TheoryBtn;
                case 2:
                    return TestBtn;
                case 3:
                    return ProfileBtn;
                default:
                    return SupportBtn;
            }
        }

        private int _tabIdx = 1;
        public LeftNavBar()
        {
            this.InitializeComponent();
        }

        private void Theory(object sender, TappedRoutedEventArgs e)
        {
            var block = GetBtn();
            block.Foreground = (SolidColorBrush)Resources["Deselected"];
            _tabIdx = 1;
            TheoryBtn.Foreground = (SolidColorBrush)Resources["Selected"];
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(HomePage));
        }

        private void Tests(object sender, TappedRoutedEventArgs e)
        {
            var block = GetBtn();
            block.Foreground = (SolidColorBrush)Resources["Deselected"];
            _tabIdx = 2;
            TestBtn.Foreground = (SolidColorBrush)Resources["Selected"];
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TestsPage));
        }

        private void Profile(object sender, TappedRoutedEventArgs e)
        {
            var block = GetBtn();
            block.Foreground = (SolidColorBrush)Resources["Deselected"];
            _tabIdx = 3;
            ProfileBtn.Foreground = (SolidColorBrush)Resources["Selected"];
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(Profile));
        }

        private void Support(object  sender, TappedRoutedEventArgs e)
        {
            var block = GetBtn();
            block.Foreground = (SolidColorBrush)Resources["Deselected"];
            _tabIdx = 4;
            SupportBtn.Foreground = (SolidColorBrush)Resources["Selected"];
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(SupportPage));
        }
    }
}
