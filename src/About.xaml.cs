using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IHC_Project
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void Tiago_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/tigas.gomess");
        }

        private void Paulo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/paulogil93");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window1 Main = (Window1)Application.Current.MainWindow;
            Main.LayoutRoot.Children.Clear();
            Main.LayoutRoot.Children.Add(Main.PopStack());
        }
    }
}
