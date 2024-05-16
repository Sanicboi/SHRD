using SHRD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SHRD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestsPage : Page
    {
        public TestsPage()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<Test> AlgebraTests = new ObservableCollection<Test>();
        public ObservableCollection<Test> GeometryTests = new ObservableCollection<Test>();
        public ObservableCollection<Test> StatisticsTests = new ObservableCollection<Test>();


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var tests = await TestController.GetTests();
            foreach (var i in tests)
            {
                switch (i.course)
                {
                    case "algebra":
                        AlgebraTests.Add(i); 
                        break;
                    case "geometry":
                        GeometryTests.Add(i);
                        break;
                    case "statistics":
                        StatisticsTests.Add(i);
                        break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(CreateTestPage));
        }

        private void Selected_Algebra(object sender, SelectionChangedEventArgs e)
        {
            Test current = Algebra.SelectedItem as Test;
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TestPage), current);
        }

        private void Selected_Geometry(object sender, SelectionChangedEventArgs e)
        {
            Test current = Geometry.SelectedItem as Test;
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TestPage), current);
        }

        private void Selected_Statistics(object sender, SelectionChangedEventArgs e)
        {
            Test current = Statistics.SelectedItem as Test;
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TestPage), current);
        }
    }
}
