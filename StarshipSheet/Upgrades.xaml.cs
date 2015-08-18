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
            this.Starship = starship;
            UpgradeCount = 0;
            InitializeComponent();
            generateUpgrades();
            UpdateAll();
        }

        private void UpdateAll()
        {
            UpdateCherubimAerie();
            UpdateCrewImprovements();
            UpdateOstentatious();
            UpdateStarchartCollection();
            UpdateStormTrooper();
            UpdateVaultedCeilings();
            UpdateArresterEngines();
            UpdateDisciplinarium();
            UpdateDistributed();
            UpdateMimicDrive();
            UpdateOverloadShield();
            UpdateResolutionArena();
            UpdateSecondaryReactor();
            UpdateSuperiorDamageControl();
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
            //Arrester Engines
            label = new Label();
            label.Content = "Arrester Engines";
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
            button.Click += ((s, e) => ArresterEnginesClick(Quality.None));
            Grid.SetRow(button, UpgradeCount++);
            UpgradeGrid.Children.Add(button);
            foreach (Quality quality in Enum.GetValues(typeof(Quality)))
            {
                description = Up.ArresterEngines(quality);
                if (description != null)
                {
                    button = new Button();
                    button.Content = quality + " Quality";
                    button.Click += ((s, e) => ArresterEnginesClick(quality));
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
            UpdateCherubimAerie();
        }

        private void UpdateCherubimAerie()
        {
            Quality quality = Starship.CherubimAerie;
            if (quality == Quality.None)
            {
                CherubimAerieName.Visibility = System.Windows.Visibility.Collapsed;
                CherubimAerieText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                CherubimAerieName.Visibility = System.Windows.Visibility.Visible;
                CherubimAerieName.Content = quality + " Quality Cherubim Aerie";
                CherubimAerieText.Visibility = System.Windows.Visibility.Visible;
                CherubimAerieText.Text = Up.CherubimAerieDescription(quality);
            }
        }

        private void CrewImprovementsClick(Quality quality)
        {
            Starship.CrewImprovements = quality;
            UpdateCrewImprovements();
        }

        private void UpdateCrewImprovements()
        {
            Quality quality = Starship.CrewImprovements;
            if (quality == Quality.None)
            {
                CrewImprovementsName.Visibility = System.Windows.Visibility.Collapsed;
                CrewImprovementsText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                CrewImprovementsName.Visibility = System.Windows.Visibility.Visible;
                CrewImprovementsName.Content = quality + " Quality Crew Improvements";
                CrewImprovementsText.Visibility = System.Windows.Visibility.Visible;
                CrewImprovementsText.Text = Up.CrewImprovements(quality);
            }
        }

        private void OstentatiousClick(Quality quality)
        {
            Starship.OstentatiousDisplayOfWealth = quality;
            UpdateOstentatious();
        }

        private void UpdateOstentatious()
        {
            Quality quality = Starship.OstentatiousDisplayOfWealth;
            if (quality == Quality.None)
            {
                OstentatiousName.Visibility = System.Windows.Visibility.Collapsed;
                OstentatiousText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                OstentatiousName.Visibility = System.Windows.Visibility.Visible;
                OstentatiousName.Content = quality + " Quality Ostentatious Display of Wealth";
                OstentatiousText.Visibility = System.Windows.Visibility.Visible;
                OstentatiousText.Text = Up.OstentatiousDisplayOfWealthDescription(quality);
            }
        }

        private void StarchartCollectionClick(Quality quality)
        {
            Starship.StarchartCollection = quality;
            UpdateStarchartCollection();
        }

        private void UpdateStarchartCollection()
        {
            Quality quality = Starship.StarchartCollection;
            if (quality == Quality.None)
            {
                StarchartCollectionName.Visibility = System.Windows.Visibility.Collapsed;
                StarchartCollectionText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                StarchartCollectionName.Visibility = System.Windows.Visibility.Visible;
                StarchartCollectionName.Content = quality + " Quality Starchart Collection";
                StarchartCollectionText.Visibility = System.Windows.Visibility.Visible;
                StarchartCollectionText.Text = Up.StarchartCollectionDescription(quality);
            }
        }

        private void StormTrooperClick(Quality quality)
        {
            Starship.StormTrooperDetachment = quality;
            UpdateStormTrooper();
        }

        private void UpdateStormTrooper()
        {
            Quality quality = Starship.StormTrooperDetachment;
            if (quality == Quality.None)
            {
                StormTrooperName.Visibility = System.Windows.Visibility.Collapsed;
                StormTrooperText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                StormTrooperName.Visibility = System.Windows.Visibility.Visible;
                StormTrooperName.Content = quality + " Quality Storm Trooper Detachment";
                StormTrooperText.Visibility = System.Windows.Visibility.Visible;
                StormTrooperText.Text = Up.StormTrooperDetachment(quality);
            }
        }

        private void VaultedCeilingsClick(Quality quality)
        {
            Starship.VaultedCeilings = quality;
            UpdateVaultedCeilings();
        }

        private void UpdateVaultedCeilings()
        {
            Quality quality = Starship.VaultedCeilings;
            if (quality == Quality.None)
            {
                VaultedCeilingsName.Visibility = System.Windows.Visibility.Collapsed;
                VaultedCeilingsText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                VaultedCeilingsName.Visibility = System.Windows.Visibility.Visible;
                VaultedCeilingsName.Content = quality + " Quality Vaulted Ceilings";
                VaultedCeilingsText.Visibility = System.Windows.Visibility.Visible;
                VaultedCeilingsText.Text = Up.VaultedCeilings(quality);
            }
        }

        private void ArresterEnginesClick(Quality quality)
        {
            Starship.ArresterEngines = quality;
            UpdateArresterEngines();
        }

        private void UpdateArresterEngines()
        {
            Quality quality = Starship.ArresterEngines;
            if (quality == Quality.None)
            {
                ArresterEnginesName.Visibility = System.Windows.Visibility.Collapsed;
                ArresterEnginesText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ArresterEnginesName.Visibility = System.Windows.Visibility.Visible;
                ArresterEnginesName.Content = quality + " Quality Arrester Engines";
                ArresterEnginesText.Visibility = System.Windows.Visibility.Visible;
                ArresterEnginesText.Text = Up.ArresterEngines(quality);
            }
        }

        private void DistributedClick(Quality quality)
        {
            Starship.DistributedCargoHold = quality;
            UpdateDistributed();
        }

        private void UpdateDistributed()
        {
            Quality quality = Starship.DistributedCargoHold;
            if (quality == Quality.None)
            {
                DistributedName.Visibility = System.Windows.Visibility.Collapsed;
                DistributedText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                DistributedName.Visibility = System.Windows.Visibility.Visible;
                DistributedName.Content = quality + " Quality Distributed Cargo Holds";
                DistributedText.Visibility = System.Windows.Visibility.Visible;
                DistributedText.Text = Up.DistributedCargoHoldDescription(quality);
            }
        }

        private void DisciplinariumClick(Quality quality)
        {
            Starship.Disciplinarium = quality;
            UpdateDisciplinarium();
        }

        private void UpdateDisciplinarium()
        {
            Quality quality = Starship.Disciplinarium;
            if (quality == Quality.None)
            {
                DisciplinariumName.Visibility = System.Windows.Visibility.Collapsed;
                DisciplinariumText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                DisciplinariumName.Visibility = System.Windows.Visibility.Visible;
                DisciplinariumName.Content = quality + " Quality Disciplinarium";
                DisciplinariumText.Visibility = System.Windows.Visibility.Visible;
                DisciplinariumText.Text = Up.DisciplinariumDescription(quality);
            }
        }

        private void MimicDriveClick(Quality quality)
        {
            Starship.MimicDrive = quality;
            UpdateMimicDrive();
        }

        private void UpdateMimicDrive()
        {
            Quality quality = Starship.MimicDrive;
            if (quality == Quality.None)
            {
                MimicDriveName.Visibility = System.Windows.Visibility.Collapsed;
                MimicDriveText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                MimicDriveName.Visibility = System.Windows.Visibility.Visible;
                MimicDriveName.Content = quality + " Quality Mimic Drive";
                MimicDriveText.Visibility = System.Windows.Visibility.Visible;
                MimicDriveText.Text = Up.MimicDrive(quality);
            }
        }

        private void OverloadShieldClick(Quality quality)
        {
            Starship.OverloadShieldCapacitors = quality;
            UpdateOverloadShield();
        }

        private void UpdateOverloadShield()
        {
            Quality quality = Starship.OverloadShieldCapacitors;
            if (quality == Quality.None)
            {
                OverloadShieldName.Visibility = System.Windows.Visibility.Collapsed;
                OverloadShieldText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                OverloadShieldName.Visibility = System.Windows.Visibility.Visible;
                OverloadShieldName.Content = quality + " Quality Overload Shield Capacitors";
                OverloadShieldText.Visibility = System.Windows.Visibility.Visible;
                OverloadShieldText.Text = Up.OverloadShieldCapacitors(quality);
            }
        }

        private void ResolutionArenaClick(Quality quality)
        {
            Starship.ResolutionArena = quality;
            UpdateResolutionArena();
        }

        private void UpdateResolutionArena()
        {
            Quality quality = Starship.ResolutionArena;
            if (quality == Quality.None)
            {
                ResolutionArenaName.Visibility = System.Windows.Visibility.Collapsed;
                ResolutionArenaText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ResolutionArenaName.Visibility = System.Windows.Visibility.Visible;
                ResolutionArenaName.Content = quality + " Quality Resolution Arena";
                ResolutionArenaText.Visibility = System.Windows.Visibility.Visible;
                ResolutionArenaText.Text = Up.ResolutionArenaDescription(quality);
            }
        }

        private void SecondaryReactorClick(Quality quality)
        {
            Starship.SecondaryReactor = quality;
            UpdateSecondaryReactor();
        }

        private void UpdateSecondaryReactor()
        {
            Quality quality = Starship.SecondaryReactor;
            if (quality == Quality.None)
            {
                SecondaryReactorName.Visibility = System.Windows.Visibility.Collapsed;
                SecondaryReactorText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                SecondaryReactorName.Visibility = System.Windows.Visibility.Visible;
                SecondaryReactorName.Content = quality + " Quality Secondary Reactor";
                SecondaryReactorText.Visibility = System.Windows.Visibility.Visible;
                SecondaryReactorText.Text = Up.SecondaryReactorDescription(quality);
            }
        }

        private void SuperiorDamageControlClick(Quality quality)
        {
            Starship.SuperiorDamageControl = quality;
            UpdateSuperiorDamageControl();
        }

        private void UpdateSuperiorDamageControl()
        {
            Quality quality = Starship.SuperiorDamageControl;
            if (quality == Quality.None)
            {
                SuperiorDamageControlName.Visibility = System.Windows.Visibility.Collapsed;
                SuperiorDamageControlText.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                SuperiorDamageControlName.Visibility = System.Windows.Visibility.Visible;
                SuperiorDamageControlName.Content = quality + " Quality Superior Damage Control";
                SuperiorDamageControlText.Visibility = System.Windows.Visibility.Visible;
                SuperiorDamageControlText.Text = Up.SuperiorDamageControl(quality);
            }
        }
        #endregion
    }
}
