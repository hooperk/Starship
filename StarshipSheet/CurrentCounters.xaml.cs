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
using System.Windows.Shapes;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for CurrentCounters.xaml
    /// </summary>
    public partial class CurrentCounters : Window
    {
        private int originalValue;
        public int MaxValue;

        public CurrentCounters(String name, int current, int max)
        {
            InitializeComponent();
            Subject.Content = name;
            Current.Value = originalValue = current;
            Max.Content = MaxValue = max;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Current.Value = MaxValue;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Current.Value = originalValue;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public new int ShowDialog()
        {
            bool? ran = base.ShowDialog();
            if (ran.HasValue && ran.Value)
                return Current.Value;
            else return originalValue;
        }
    }
}
