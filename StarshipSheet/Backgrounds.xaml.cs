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
    /// Interaction logic for Background.xaml
    /// </summary>
    public partial class Backgrounds : Window
    {
        Starship Starship { get; set; }

        public Backgrounds(Starship starship)
        {
            if (starship == null)
                throw new ArgumentNullException("Cannot initialise Backgrounds with a null ship");
            Starship = starship;
            this.Closed += onClose;
            InitializeComponent();
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
            Speed.Value = Starship.GMSpeed;
            HullIntegrity.Value = Starship.GMHullIntegrity;
            Detection.Value = Starship.GMDetection;
            Manoeuvrability.Value = Starship.GMManoeuvrability;
            Armour.Value = Starship.GMArmour;
            TurretRating.Value = Starship.GMTurretRating;
            Morale.Value = Starship.GMMorale;
            CrewPopulation.Value = Starship.GMCrewPopulation;
            Shields.Value = Starship.GMShields;
            CrewRating.Value = Starship.GMCrewRating;
            Special.Text = Starship.GMSpecial;
            Machine.Text = Starship.GMMachineSpirit;
            History.Text = Starship.GMShipHistory;
        }

        private void onClose(object sender, EventArgs e)
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
            Starship.GMSpeed = Speed.Value;
            Starship.GMHullIntegrity = HullIntegrity.Value;
            Starship.GMDetection = Detection.Value;
            Starship.GMManoeuvrability = Manoeuvrability.Value;
            Starship.GMArmour = Armour.Value;
            Starship.GMTurretRating = TurretRating.Value;
            Starship.GMMorale = Morale.Value;
            Starship.GMCrewPopulation = CrewPopulation.Value;
            Starship.GMShields = Shields.Value;
            Starship.GMCrewRating = CrewRating.Value;
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
