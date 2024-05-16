using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SHRD.Controllers;
using static System.Net.Mime.MediaTypeNames;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SHRD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            
        }

        public List<Theory> theory = new List<Theory>();

        public ObservableCollection<Theory> AlgebraTheory = new ObservableCollection<Theory>();
        public ObservableCollection<Theory> GeometryTheory = new ObservableCollection<Theory>();
        public ObservableCollection<Theory> StatisticsTheory = new ObservableCollection<Theory>();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            theory = await TheoryController.GetTheory();
            foreach (var i in theory)
            {
                switch (i.course)
                {
                    case "algebra":
                        AlgebraTheory.Add(i);
                        break;
                    case "geometry":
                        GeometryTheory.Add(i);
                        break;
                    case "statistics":
                        StatisticsTheory.Add(i);
                        break;
                }
            }
            base.OnNavigatedTo(e);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TheoryCreate));
        }

        private void Algebra_Select(object sender, SelectionChangedEventArgs e)
        {
            Theory current = Algebra.SelectedItem as Theory;
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TextPage), current);
        }

        private void Geometry_Select(object sender, SelectionChangedEventArgs e)
        {
            Theory current = Geometry.SelectedItem as Theory;
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TextPage), current);
        }

        private void Statistics_Select(object sender, SelectionChangedEventArgs e)
        {
            Theory current = Statistics.SelectedItem as Theory;
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TextPage), current);
        }

    }
}
