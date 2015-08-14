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
using Up = StarshipGenerator.Components.Upgrades;
using StarshipGenerator.Utils;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for Upgrades.xaml
    /// </summary>
    public partial class Upgrades : Window
    {
        Starship Starship { get; set; }
        public int UpgradeCount { get; set; }

        public Upgrades(Starship starship)
        {
            if (starship == null)
                throw new ArgumentNullException("Cannot initialise Upgrades with a null ship");
            Closed += onClose;
            this.Starship = starship;
            UpgradeCount = 0;
            InitializeComponent();
            generateUpgrades();
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

        private void generateUpgrades()
        {
            Label label;
            Button button;
            TextBlock textblock;
            String description;
            //Cherubim Aerie
            label = new Label();
            label.Content = "Cherubim Aerie";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.IntoTheStorm.Name();
            label.ToolTip = RuleBook.IntoTheStorm.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 163";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => CherubimAerieClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach(Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.CherubimAerieDescription(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => CherubimAerieClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //crew improvements
            label = new Label();
            label.Content = "Crew Improvements";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.IntoTheStorm.Name();
            label.ToolTip = RuleBook.IntoTheStorm.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 163";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => CrewImprovementsClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.CrewImprovements(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => CrewImprovementsClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //Ostentatious...
            label = new Label();
            label.Content = "Ostentatious Display of Wealth";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.IntoTheStorm.Name();
            label.ToolTip = RuleBook.IntoTheStorm.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 163";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => OstentatiousClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.OstentatiousDisplayOfWealthDescription(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => OstentatiousClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //Starchart collection
            label = new Label();
            label.Content = "Starchart Collection";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.IntoTheStorm.Name();
            label.ToolTip = RuleBook.IntoTheStorm.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 164";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => StarchartCollectionClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.StarchartCollectionDescription(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => StarchartCollectionClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //storm troopers
            label = new Label();
            label.Content = "Storm Troopers";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.IntoTheStorm.Name();
            label.ToolTip = RuleBook.IntoTheStorm.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 164";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => StormTrooperClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.StormTrooperDetachment(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => StormTrooperClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //vaulted Ceilings
            label = new Label();
            label.Content = "Vaulted Ceilings";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.IntoTheStorm.Name();
            label.ToolTip = RuleBook.IntoTheStorm.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 164";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => VaultedCeilingsClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.VaultedCeilings(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => VaultedCeilingsClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //arrestor Engines
            label = new Label();
            label.Content = "Arrestor Engines";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.HostileAcquisition.Name();
            label.ToolTip = RuleBook.HostileAcquisition.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 76";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => ArrestorEnginesClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.ArresterEngines(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => ArrestorEnginesClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //Distributed Holds
            label = new Label();
            label.Content = "Distributed Cargo Holds";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.HostileAcquisition.Name();
            label.ToolTip = RuleBook.HostileAcquisition.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 76";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => DistributedClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.DistributedCargoHoldDescription(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => DistributedClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //Disciplinarium
            label = new Label();
            label.Content = "Disciplinarium";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.HostileAcquisition.Name();
            label.ToolTip = RuleBook.HostileAcquisition.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 76";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => DisciplinariumClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.DisciplinariumDescription(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => DisciplinariumClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //Mimic Drive
            label = new Label();
            label.Content = "Mimic Drive";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.HostileAcquisition.Name();
            label.ToolTip = RuleBook.HostileAcquisition.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 77";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => MimicDriveClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.MimicDrive(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => MimicDriveClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //Overload Shield Capacitors
            label = new Label();
            label.Content = "Overload Shield Capacitors";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.HostileAcquisition.Name();
            label.ToolTip = RuleBook.HostileAcquisition.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 77";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => OverloadShieldClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.OverloadShieldCapacitors(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => OverloadShieldClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //resolution arena
            label = new Label();
            label.Content = "Resolution Arena";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.HostileAcquisition.Name();
            label.ToolTip = RuleBook.HostileAcquisition.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 77";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => ResolutionArenaClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.ResolutionArenaDescription(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => ResolutionArenaClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //Secondary Reactor
            label = new Label();
            label.Content = "Secondary Reactor";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.HostileAcquisition.Name();
            label.ToolTip = RuleBook.HostileAcquisition.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 77";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => SecondaryReactorClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.SecondaryReactorDescription(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => SecondaryReactorClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
            //superior damage control
            label = new Label();
            label.Content = "Superior Damage Control";
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 0);
            Grid.SetColumnSpan(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = RuleBook.HostileAcquisition.Name();
            label.ToolTip = RuleBook.HostileAcquisition.LongName();
            Grid.SetRow(label, UpgradeCount);
            Grid.SetColumn(label, 2);
            UpgradeGrid.Children.Add(label);
            label = new Label();
            label.Content = "Page 78";
            Grid.SetRow(label, UpgradeCount++);
            Grid.SetColumn(label, 3);
            UpgradeGrid.Children.Add(label);
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => SuperiorDamageControlClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.SuperiorDamageControl(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => SuperiorDamageControlClick(quality));
                    Grid.SetRow(button, UpgradeCount);
                    Grid.SetColumn(button, 0);
                    UpgradeGrid.Children.Add(button);
                    textblock = new TextBlock();
                    textblock.Text = description;
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, UpgradeCount++);
                    Grid.SetColumn(textblock, 1);
                    Grid.SetColumnSpan(textblock, 3);
                    UpgradeGrid.Children.Add(textblock);
                }
            }
        }

        #region UpgradeButtons
        private void CherubimAerieClick(Quality quality)
        {
            Starship.CherubimAerie = quality;
        }

        private void CrewImprovementsClick(Quality quality)
        {
            Starship.CrewImprovements = quality;
        }

        private void OstentatiousClick(Quality quality)
        {
            Starship.OstentatiousDisplayOfWealth = quality;
        }

        private void StarchartCollectionClick(Quality quality)
        {
            Starship.StarchartCollection = quality;
        }

        private void StormTrooperClick(Quality quality)
        {
            Starship.StormTrooperDetachment = quality;
        }

        private void VaultedCeilingsClick(Quality quality)
        {
            Starship.VaultedCeilings = quality;
        }

        private void ArrestorEnginesClick(Quality quality)
        {
            Starship.ArresterEngines = quality;
        }

        private void DistributedClick(Quality quality)
        {
            Starship.DistributedCargoHold = quality;
        }

        private void DisciplinariumClick(Quality quality)
        {
            Starship.Disciplinarium = quality;
        }

        private void MimicDriveClick(Quality quality)
        {
            Starship.MimicDrive = quality;
        }

        private void OverloadShieldClick(Quality quality)
        {
            Starship.OverloadShieldCapacitors = quality;
        }

        private void ResolutionArenaClick(Quality quality)
        {
            Starship.ResolutionArena = quality;
        }

        private void SecondaryReactorClick(Quality quality)
        {
            Starship.SecondaryReactor = quality;
        }

        private void SuperiorDamageControlClick(Quality quality)
        {
            Starship.SuperiorDamageControl = quality;
        }
        #endregion

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
