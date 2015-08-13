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
using StarshipGenerator.Utils;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for Upgrades.xaml
    /// </summary>
    public partial class Upgrades : Window
    {
        Starship Starship { get; set; }

        public Upgrades(Starship starship)
        {
            Closed += onClose;
            this.Starship = starship;
            InitializeComponent();
            Special.Text = Starship.GMSpecial;
            Machine.Text = Starship.GMMachineSpirit;
            History.Text = Starship.GMShipHistory;
            d5.Value = 1;
            switch (Starship.Background)
            {
                case StarshipGenerator.Utils.Background.ThulianExploratorVessel:
                    ThulianExploratorVessel.IsChecked = true;
                    break;
                case StarshipGenerator.Utils.Background.ReaverOfTheUnbeholdenReaches:
                    ReaverOfTheUnbeholdenReaches.IsChecked = true;
                    break;
                case StarshipGenerator.Utils.Background.VeteranOfTheAngevinCrusade:
                    VeteranOfTheAngevinCrusade.IsChecked = true;
                    break;
                case StarshipGenerator.Utils.Background.ImplacableFoeOfTheFleet:
                    ImplacableFoeOfTheFleet.IsChecked = true;
                    break;
                case StarshipGenerator.Utils.Background.SteadfastAllyofTheFleet:
                    SteadfastAllyofTheFleet.IsChecked = true;
                    break;
                default:
                    StarshipGenerator.Utils.Background planetbound = Starship.Background & StarshipGenerator.Utils.Background.PlanetBoundForMillenia;
                    if (planetbound != 0)
                    {
                        PlanetBoundForMillenia.IsChecked = true;
                        d5.Value = (int)planetbound;
                    }
                    break;
            }
        }

        public void onClose(object sender, EventArgs e)
        {
            if (ThulianExploratorVessel.IsChecked ?? false)
                Starship.Background = StarshipGenerator.Utils.Background.ThulianExploratorVessel;
            else if (ReaverOfTheUnbeholdenReaches.IsChecked ?? false)
                Starship.Background = StarshipGenerator.Utils.Background.ReaverOfTheUnbeholdenReaches;
            else if (VeteranOfTheAngevinCrusade.IsChecked ?? false)
                Starship.Background = StarshipGenerator.Utils.Background.VeteranOfTheAngevinCrusade;
            else if (ImplacableFoeOfTheFleet.IsChecked ?? false)
                Starship.Background = StarshipGenerator.Utils.Background.ImplacableFoeOfTheFleet;
            else if (SteadfastAllyofTheFleet.IsChecked ?? false)
                Starship.Background = StarshipGenerator.Utils.Background.SteadfastAllyofTheFleet;
            else if (PlanetBoundForMillenia.IsChecked ?? false)
                Starship.Background = StarshipGenerator.Utils.Background.PlanetBoundForMillenia & (StarshipGenerator.Utils.Background)d5.Value;
            else
                Starship.Background = StarshipGenerator.Utils.Background.None;//if none of them are selected then clear
            Starship.GMSpecial = Special.Text;
            if (!String.IsNullOrWhiteSpace(Machine.Text))
            {
                Starship.GMMachineSpirit = Machine.Text;
                Starship.MachineSpirit = MachineSpirit.None;
            }
            if (!String.IsNullOrWhiteSpace(History.Text))
            {
                Starship.GMShipHistory = History.Text;
                Starship.ShipHistory = ShipHistory.None;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ThulianExploratorVessel.IsChecked = false;
            ReaverOfTheUnbeholdenReaches.IsChecked = false;
            VeteranOfTheAngevinCrusade.IsChecked = false;
            ImplacableFoeOfTheFleet.IsChecked = false;
            SteadfastAllyofTheFleet.IsChecked = false;
            PlanetBoundForMillenia.IsChecked = false;
        }
    }
}
