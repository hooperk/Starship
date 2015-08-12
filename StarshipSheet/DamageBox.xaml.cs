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
    /// Interaction logic for DamageBox.xaml
    /// </summary>
    public partial class DamageBox : Window
    {
        private int armour = 0;
        private int prowarmour = 0;
        private int crewlossmodifier = 0;
        private int moralelossmodifier = 0;
        public int HullLost
        {
            get { return _hullLost; }
            set
            {
                _hullLost = value;
                HullLoss.Text = HullLost.ToString();
            }
        }
        private int _hullLost = 0;
        public int MoraleLost
        {
            get { return _moraleLost; }
            set
            {
                _moraleLost = value;
                MoraleLoss.Text = MoraleLost.ToString();
            }
        }
        private int _moraleLost = 0;
        public int CrewLost
        {
            get { return _crewLost; }
            set
            {
                _crewLost = value;
                CrewLoss.Text = CrewLost.ToString();
            }
        }
        private int _crewLost = 0;

        public DamageBox(int armour, int prowarmour, int crewloss, int moraleloss)
        {
            this.armour = armour;
            this.prowarmour = prowarmour;
            this.crewlossmodifier = crewloss;
            this.moralelossmodifier = moraleloss;
            InitializeComponent();
            ArmourRating.Content = armour + "(" + prowarmour + ")";
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            int damage = Damage.Value;
            if (Lance.IsChecked.HasValue && !Lance.IsChecked.Value)
            {
                if (Prow.IsChecked.HasValue && Prow.IsChecked.Value)
                    damage -= prowarmour;
                else
                    damage -= armour;
            }
            if (damage > 0)
            {
                HullLost = damage;
                MoraleLost = Math.Max(damage + moralelossmodifier, 1);//don't display less than a loss of 1
                CrewLost = Math.Max(damage + crewlossmodifier, 1);//don't display less than a loss of one
            }
            else
            {
                HullLost = 0;
                MoraleLost = 0;
                CrewLost = 0;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
