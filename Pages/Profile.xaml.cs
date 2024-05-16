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
            this.me = AuthorizationController.me;
            this.otherMe = AuthorizationController.instance;
        }

        public Contact me;
        public User otherMe;

        public string SolvedTasks {
            get
            {
                return $"{otherMe.statistics.Count} выполненных работ";
            }
        }

        public string AvgPerc
        {
            get
            {
                if (otherMe.statistics.Count == 0) return "0% средний процент выполнения";
                var sum = otherMe.statistics.Sum(x => x.result);
                return $"{Math.Round(sum / otherMe.statistics.Count, 2) * 100}% средний процент выполнения";
            }
        }
    }
}
