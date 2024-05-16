using SHRD.Models;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SHRD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateTestPage : Page
    {
        public CreateTestPage()
        {
            this.InitializeComponent();
            test.id = "";
        }

        public Test test = new Test();
        public ObservableCollection<TestTask> tasks = new ObservableCollection<TestTask>();

        private async void SaveTest(object sender, RoutedEventArgs e)
        {
            string course = "";
            switch (Course.SelectedItem as String)
            {
                case "Алгебра":
                    course = "algebra";
                    break;
                case "Геометрия":
                    course = "geometry";
                    break;
                case "Теория Вероятностей":
                    course = "statistics";
                    break;
            }
            test.course = course;
            test.name = Name.Text;
            test.tasks = tasks.ToList();
            await TestController.CreateTest(test);
            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(TestsPage));
        }

        private void SaveTask(object sender, RoutedEventArgs e)
        {
            TestTask task = new TestTask();
            string r;
            CurrentText.Document.GetText(Windows.UI.Text.TextGetOptions.NoHidden, out r);
            task.text = r;
            task.answer = CurrentAnswer.Text;
            task.id = "";
            tasks.Add(task);   
        }

    }
}
