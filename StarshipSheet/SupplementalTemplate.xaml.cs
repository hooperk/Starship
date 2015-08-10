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
using StarshipGenerator.Components;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for Supplemental.xaml
    /// </summary>
    public partial class SupplementalTemplate : UserControl
    {
        public Supplemental Component;

        public int Max;
        public int Min;
        public int Count;

        public SupplementalTemplate(Supplemental component, int count = 1, int min = 0)
        {
            InitializeComponent();
            Component = component;
            Min = min;
            Max = Component.Max;
            Count = count;
            CountDisplay.Content = Count.ToString();
            ComponentName.Text = Component.QualityName;
            Special.Text = Component.Special;
            CheckAdd();
            CheckRemove();
        }

        public void CheckAdd()
        {
            if (Max == 0 || Count < Max)
                AddButton.IsEnabled = true;
            else
                AddButton.IsEnabled = false;
        }

        public void CheckRemove()
        {
            if (Count > Min)
                RemoveButton.IsEnabled = true;
            else
                RemoveButton.IsEnabled = false;
        }
    }
}
