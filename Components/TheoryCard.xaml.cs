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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SHRD
{
    public sealed partial class TheoryCard : UserControl
    {
        public TheoryCard()
        {
            this.SubjectName = "Hello world";
            this.ImageUrl = "https://t3.ftcdn.net/jpg/03/66/39/30/360_F_366393058_zniuIqgkVTfnoL7eqTshVYpwi9yhJVU8.jpg";
            this.InitializeComponent();
        }

        public string SubjectName
        {
            get; set;
        }

        public string ImageUrl
        {
            get; set;
        }
    }
}
