using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Contacts;
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
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SHRD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        public Profile()
        {
            this.InitializeComponent();
        }

        public Data data = new Data();
        public class Data : INotifyPropertyChanged
        {
            private User _me = new User()
            {
                statistics = new List<TestStats>(),
                tests = new List<Test>()
            };
            private Contact _contact = new Contact();
            public User Me
            {
                get { return _me; }
                set { 
                    _me = value;
                    RaisePropertyChanged(nameof(Me));
                    RaisePropertyChanged(nameof(SolvedTasks));
                    RaisePropertyChanged(nameof(AvgPerc));
                }
            }

            public Contact contact
            {
                get { return _contact; }
                set
                {
                    _contact = value;
                    RaisePropertyChanged(nameof(contact));
                }
            }

            public string SolvedTasks
            {
                get
                {
                    return $"{Me.statistics.Count} выполненных работ";
                }
            }

            public string AvgPerc
            {
                get
                {
                    if (Me.statistics.Count == 0) return "0% средний процент выполнения";
                    var sum = Me.statistics.Sum(x => x.result);
                    return $"{Math.Round(sum / Me.statistics.Count, 2) * 100}% средний процент выполнения";
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;
            protected void RaisePropertyChanged(string name)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }
        }





        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                base.OnNavigatedTo(e);
                data.Me = await UserController.Me();
                var c = new Contact();
                c.FirstName = data.Me.username;
                data.contact = c;
            } catch (Exception ex)
            {
                await AuthorizationController.Logout();
            }
        }

        protected async void Logout(object sender, RoutedEventArgs e)
        {
            await AuthorizationController.Logout();
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(EnterPage));
        }
    }
}
