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
using StarshipGenerator.Utils;
using StarshipGenerator.Components;

namespace StarshipSheet
{


    /// <summary>
    /// Interaction logic for StarshipCreator.xaml
    /// </summary>
    public partial class StarshipCreator : Window
    {
        Loader loader;
        Starship starship;
        public int WeaponRowCount
        {
            get
            {
                return GridHelpers.GetRowCount(Weapons);
            }
            set
            {
                GridHelpers.SetRowCount(Weapons, value);
            }
        }
        public int SupplementalRowCount
        {
            get
            {
                return GridHelpers.GetRowCount(Supplementals);
            }
            set
            {
                GridHelpers.SetRowCount(Supplementals, value);
            }
        }

        public StarshipCreator()
        {
            loader = new Loader();//initialise with default components TODO:Loading of saved custom components
            starship = new Starship();//fresh starship
            InitializeComponent();
        }

        #region UpdateFields
        //TODO: Binding for Name
        private int usedspace = 0;
        private int maxspace = 0;
        private int usedpower = 0;
        private int maxpower = 0;
        private int crewpopulation = 100;
        private int morale = 100;
        private int hullintegrity = 0;
        private int armour = 0;
        private int prowarmour = 0;
        private int crewloss = 0;
        private int moraleloss = 0;

        public static string Print(int input)
        {
            if (input == 0)
                return "";
            return (input < 0 ? input.ToString() : "+" + input);
        }

        public void UpdateSP()
        {
            SP.Text = starship.SP.ToString();
        }

        public void UpdateSpace()
        {
            Space.Text = usedspace + "/" + maxspace;
        }

        public void UpdateUsedSpace()
        {
            usedspace = starship.UsedSpace;
            UpdateSpace();
        }

        public void UpdateMaxSpace()
        {
            maxspace = starship.MaxSpace;
            UpdateSpace();
        }

        public void UpdatePower()
        {
            Power.Text = usedpower + "/" + maxpower;
        }

        public void UpdateUsedPower()
        {
            usedpower = starship.UsedPower;
            UpdatePower();
        }

        public void UpdateMaxPower()
        {
            maxpower = starship.MaxPower;
            UpdatePower();
        }

        public void UpdateCrewPopulation()
        {
            if (starship.CurrentCrew > crewpopulation)
                starship.CurrentCrew = crewpopulation;
            CrewPop.Text = starship.CurrentCrew + "/" + crewpopulation;

        }

        public void UpdateMaxCrew()
        {
            crewpopulation = starship.CrewPopulation;
            UpdateCrewPopulation();
        }

        public void UpdateMorale()
        {
            if (starship.CurrentMorale > morale)
                starship.CurrentMorale = morale;
            Morale.Text = starship.CurrentMorale + "/" + morale;
        }

        public void UpdateMaxMorale()
        {
            morale = starship.Morale;
            UpdateMorale();
        }

        public void UpdateHullIntegrity()
        {
            if (starship.CurrentIntegrity > hullintegrity)
                starship.CurrentIntegrity = hullintegrity;
            HullInt.Text = starship.CurrentIntegrity + "/" + hullintegrity;
        }

        public void UpdateMaxHullIntegrity()
        {
            hullintegrity = starship.HullIntegrity;
            UpdateHullIntegrity();
        }

        public void UpdateBackgrounds()
        {
            UpdateSP();
            UpdateSpeed();
            UpdateDet();
            UpdateArmour();
            UpdateMaxSpace();
            UpdateArmour();
            UpdateTurrets();
            UpdateMaxMorale();
            UpdateMaxCrew();
            UpdateShields();
            UpdateCrewRating();
            UpdateMaxHullIntegrity();
            UpdateBS();
            UpdateCommand();
            UpdateRepair();
            UpdatePilot();
            UpdateWeapons();
            UpdateHullSpecial();
        }

        public void UpdateUpgrades()
        {
            UpdateMaxSpace();
            UpdateMaxMorale();
            UpdateShields();
            UpdateCommand();
            UpdateTrade();
            UpdateCriminal();
            UpdateExploration();
        }

        public void UpdateHull()
        {
            ClearSupplementals();
            if (starship.Hull != null)
            {
                HullName.Text = starship.Hull.Name;
                HullClass.Text = starship.Hull.HullTypes.HighName();
                if (starship.SupplementalComponents != null)
                {
                    foreach (String name in starship.SupplementalComponents.Select(x => x.Name).Distinct())
                    {
                        IEnumerable<Supplemental> components = starship.SupplementalComponents.Where(x => x.Name.Equals(name));
                        int count = components.Count();
                        AddNewSupplemental(components.First(), count, count, false);
                    }
                }
            }
            else
            {
                HullName.Text = "None";
                HullClass.Text = "";
            }
            if (starship.Hull != null && starship.Hull.History != ShipHistory.None)
                ShipHistoryButton.ToolTip = "Cannot change the history of this hull";
            else
                ShipHistoryButton.ToolTip = null;
            UpdateWeaponSlots(false);
            UpdateMaxSpace();
            UpdateCommand();
            UpdateHistory();
            UpdateSupplementals();//wraps up a lot of the hull updates into what is updated by this anyway
            UpdateHullSpecial();
            //if the ship was empty or the hull integrity is now greater than max, reset it
            if (starship.CurrentIntegrity == 0 || starship.CurrentIntegrity > hullintegrity)
                starship.CurrentIntegrity = hullintegrity;
            if (starship.CurrentCrew == 0 && starship.CurrentMorale == 0)
            {
                starship.CurrentCrew = crewpopulation;
                starship.CurrentMorale = morale;
            }
        }

        public void UpdateHullSpecial()
        {
            if (starship.Hull == null || (starship.Hull.Special == null && starship.GMSpecial == null))
                HullSpecial.Visibility = Visibility.Collapsed;
            else
            {
                HullSpecialText.Text = starship.Hull.Special + starship.GMSpecial;
                HullSpecial.Visibility = Visibility.Visible;
            }

        }

        public void UpdateSpeed()
        {
            Speed.Text = starship.Speed.ToString();
        }

        public void UpdateMan()
        {
            Manoeuvrability.Text = starship.Manoeuvrability.ToString();
        }

        public void UpdateDet()
        {
            Detection.Text = starship.DetectionRating.ToString();
        }

        public void UpdateArmour()
        {
            armour = starship.Armour;
            prowarmour = starship.ProwArmour;
            Armour.Text = armour + (armour != prowarmour ? "(" + prowarmour + ")" : "");
        }

        public void UpdateTurrets()
        {
            Turrets.Text = starship.TurretRating.ToString();
        }

        public void UpdateShields()
        {
            Shields.Text = starship.Shields.ToString();
        }

        public void UpdateCrewRating()
        {
            CrewRating.Text = starship.CrewValue.ToString();
        }

        public void UpdateMachine(bool update = true)
        {
            if (!String.IsNullOrWhiteSpace(starship.GMMachineSpirit))
                MachineSpiritDisplay.Text = starship.GMMachineSpirit;
            else if (starship.MachineSpirit == MachineSpirit.None)
            {
                MachineSpiritDisplay.Text = "Machine Spirit";
            }
            else
                MachineSpiritDisplay.Text = starship.MachineSpirit.Name() + ": " + starship.MachineSpirit.Special();
            if (update)
            {
                UpdateSpeed();
                UpdateDet();
                UpdateArmour();
                UpdateMaxHullIntegrity();
                UpdateBS();
                UpdateRepair();
                UpdatePilot();
            }
        }

        public void UpdateHistory(bool update = true)
        {
            if (!String.IsNullOrWhiteSpace(starship.GMShipHistory))
                ShipHistoryDisplay.Text = starship.GMShipHistory;
            else if (starship.ShipHistory == ShipHistory.None)
                ShipHistoryDisplay.Text = "Ship History";
            else
                ShipHistoryDisplay.Text = starship.ShipHistory.Name() + ": " + starship.ShipHistory.Special();
            if (update)
            {
                UpdateSpeed();
                UpdateMan();
                UpdateDet();
                UpdateArmour();
                UpdateMaxPower();
                UpdateMaxCrew();
                UpdateMaxMorale();
                UpdateRepair();
            }
        }

        public void UpdatePlasma()
        {
            if (starship.PlasmaDrive == null)
            {
                PlasmaDrive.Content = "Choose Plasma Drive";
                PlasmaSpecial.Text = "";
            }
            else
            {
                PlasmaDrive.Content = starship.PlasmaDrive.QualityName;
                PlasmaSpecial.Text = starship.PlasmaDrive.Special;
            }
            UpdateSP();
            UpdateMaxPower();
            UpdateUsedSpace();
            UpdateSpeed();
            UpdateMan();
        }

        public void UpdateWarp()
        {
            if (starship.WarpDrive == null)
            {
                WarpDrive.Content = "Choose Warp Drive";
                WarpSpecial.Text = "";
            }
            else
            {
                WarpDrive.Content = starship.WarpDrive.QualityName;
                WarpSpecial.Text = starship.WarpDrive.Special;
            }
            UpdateSP();
            UpdateUsedPower();
            UpdateUsedSpace();
        }

        public void UpdateGellar()
        {
            if (starship.GellarField == null)
            {
                GellarField.Content = "Choose Gellar Field";
                GellarSpecial.Text = "";
            }
            else
            {
                GellarField.Content = starship.GellarField.QualityName;
                GellarSpecial.Text = starship.GellarField.Special;
            }
            UpdateSP();
            UpdateUsedPower();
            UpdateNavigateWarp();
        }

        public void UpdateVoid()
        {
            if (starship.VoidShield == null)
            {
                VoidShield.Content = "Choose Void Shield";
                VoidSpecial.Text = "";
            }
            else
            {
                VoidShield.Content = starship.VoidShield.QualityName;
                VoidSpecial.Text = starship.VoidShield.Special;
            }
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateShields();
        }

        public void UpdateBridge()
        {
            if (starship.ShipBridge == null)
            {
                ShipBridge.Content = "Choose Ship's Bridge";
                BridgeSpecial.Text = "";
            }
            else
            {
                ShipBridge.Content = starship.ShipBridge.QualityName;
                BridgeSpecial.Text = starship.ShipBridge.Special;
            }
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateMan();
            UpdateBS();
            UpdateCommand();
            UpdateRepair();
            UpdatePilot();
            UpdateNavigateWarp();
            UpdateMining();
            UpdateCreed();
            UpdateMilitary();
            UpdateTrade();
            UpdateCriminal();
            UpdateExploration();
        }

        public void UpdateLife()
        {
            if (starship.LifeSustainer == null)
            {
                LifeSustainer.Content = "Choose Life Sustainer";
                SustainerSpecial.Text = "";
            }
            else
            {
                LifeSustainer.Content = starship.LifeSustainer.QualityName;
                SustainerSpecial.Text = starship.LifeSustainer.Special;
            }
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateCrewLoss();
            UpdateMaxMorale();
            UpdateMoraleLoss();
        }

        public void UpdateQuarters()
        {
            if (starship.CrewQuarters == null)
            {
                CrewQuarters.Content = "Choose Crew Quarters";
                QuartersSpecial.Text = "";
            }
            else
            {
                CrewQuarters.Content = starship.CrewQuarters.QualityName;
                QuartersSpecial.Text = starship.CrewQuarters.Special;
            }
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateMaxMorale();
            UpdateMoraleLoss();
        }

        public void UpdateAugurs()
        {
            if (starship.AugurArrays == null)
            {
                AugurArrays.Content = "Choose Augur Arrays";
                AugurSpecial.Text = "";
            }
            else
            {
                AugurArrays.Content = starship.AugurArrays.QualityName;
                AugurSpecial.Text = starship.AugurArrays.Special;
            }
            UpdateSP();
            UpdateUsedPower();
            UpdateMan();
            UpdateDet();
            UpdateBS();
            UpdateMining();
            UpdateCreed();
            UpdateMilitary();
            UpdateTrade();
            UpdateCriminal();
            UpdateExploration();
        }

        public void UpdateMining()
        {
            MiningObjective.Text = Print(starship.MiningObjective);
        }

        public void UpdateCreed()
        {
            CreedObjective.Text = Print(starship.CreedObjective);
        }

        public void UpdateMilitary()
        {
            MilitaryObjective.Text = Print(starship.MilitaryObjective);
        }

        public void UpdateTrade()
        {
            TradeObjective.Text = Print(starship.TradeObjective);
        }

        public void UpdateCriminal()
        {
            TradeObjective.Text = Print(starship.CriminalObjective);
        }

        public void UpdateExploration()
        {
            TradeObjective.Text = Print(starship.ExplorationObjective);
        }

        public void UpdateRamming()
        {
            Ramming.Text = starship.Ramming.ToString();
        }

        public void UpdateBS()
        {
            BSModifier.Text = Print(starship.BSModifier);
        }

        public void UpdateCommand()
        {
            Command.Text = Print(starship.Command);
        }

        public void UpdateCrewLoss()
        {
            crewloss = starship.CrewLoss;
            CrewLoss.Text = Print(crewloss);
        }

        public void UpdateMoraleLoss()
        {
            moraleloss = starship.MoraleLoss;
            MoraleLoss.Text = Print(moraleloss);
        }

        public void UpdatePilot()
        {
            PilotTest.Text = Print(starship.PilotTest);
        }

        public void UpdateRepair()
        {
            RepairTest.Text = Print(starship.RepairTest);
        }

        public void UpdateNavigateWarp()
        {
            NavigateWarp.Text = Print(starship.NavigateTest);
        }

        public void UpdateSupplementals()
        {
            UpdateSP();
            UpdateUsedPower();
            UpdateMaxPower();
            UpdateUsedSpace();
            UpdateRamming();
            UpdateSpeed();
            UpdateMan();
            UpdateMaxHullIntegrity();
            UpdateArmour();
            UpdateTurrets();
            UpdateMaxMorale();
            UpdateMaxCrew();
            UpdateRating();
            UpdateMining();
            UpdateCreed();
            UpdateMilitary();
            UpdateTrade();
            UpdateCriminal();
            UpdateExploration();
            UpdateDet();
            UpdateWeapons();
            UpdateBS();
            UpdateNavigateWarp();
            UpdateCrewLoss();
            UpdateMoraleLoss();
        }

        public void UpdateRating()
        {
            CrewRating.Text = starship.CrewValue.ToString();
        }

        #endregion

        #region Weapons
        public void UpdateWeaponSlots(bool canupdate = true)
        {
            WeaponRowCount = 1;
            Weapons.Children.RemoveRange(8, Weapons.Children.Count - 7);
            if (starship.Hull != null)
            {
                bool update = false;
                int count = 0;
                Weapon weapon;
                for (int i = 0; i < starship.Hull.ProwSlots; i++)
                {
                    weapon = starship.WeaponList[count++].Item2;
                    if (weapon != null)
                        update = true;
                    if (i == 0 && starship.Hull.DefaultProw != null)
                        AddWeapon(WeaponSlot.Prow, weapon, update, false);
                    else
                        AddWeapon(WeaponSlot.Prow, weapon, false);
                }
                for (int i = 0; i < starship.Hull.DorsalSlots; i++)
                {
                    weapon = starship.WeaponList[count++].Item2;
                    if (weapon != null)
                        update = true;
                    AddWeapon(WeaponSlot.Dorsal, weapon, false);
                }
                for (int i = 0; i < starship.Hull.SideSlots; i++)
                {
                    weapon = starship.WeaponList[count++].Item2;
                    if (weapon != null)
                        update = true;
                    if (i == 0 && starship.Hull.DefaultBroadside != null)
                        AddWeapon(WeaponSlot.Port, weapon, update, false);
                    else
                        AddWeapon(WeaponSlot.Port, weapon, false);
                }
                for (int i = 0; i < starship.Hull.SideSlots; i++)
                {
                    weapon = starship.WeaponList[count++].Item2;
                    if (weapon != null)
                        update = true;
                    if (i == 0 && starship.Hull.DefaultBroadside != null)
                        AddWeapon(WeaponSlot.Starboard, weapon, update, false);
                    else
                        AddWeapon(WeaponSlot.Starboard, weapon, false);
                }
                for (int i = 0; i < starship.Hull.KeelSlots; i++)
                {
                    weapon = starship.WeaponList[count++].Item2;
                    if (weapon != null)
                        update = true;
                    AddWeapon(WeaponSlot.Keel, weapon, false);
                }
                for (int i = 0; i < starship.Hull.AftSlots; i++)
                {
                    weapon = starship.WeaponList[count++].Item2;
                    if (weapon != null)
                        update = true;
                    AddWeapon(WeaponSlot.Aft, weapon, false);
                }
                foreach (Weapon aux in starship.WeaponList.Where(x => x.Item1 == WeaponSlot.Auxiliary).Select(x => x.Item2))
                {
                    weapon = starship.WeaponList[count++].Item2;
                    if (weapon != null)
                        update = true;
                    AddWeapon(WeaponSlot.Auxiliary, weapon, false);
                }
                if (update && canupdate)
                {
                    UpdateSP();
                    UpdateUsedSpace();
                    UpdateUsedPower();
                }
            }
        }

        public void AddWeapon(WeaponSlot facing, Weapon weapon = null, bool update = true, bool enabled = true)
        {
            UserControl NewWeapon = null;
            if (weapon == null || weapon.Type == WeaponType.Macrobattery || weapon.Type == WeaponType.Lance)
                NewWeapon = new WeaponTemplate(facing, weapon, starship.MacrobatteryModifier);
            else if (weapon.Type == WeaponType.TorpedoTube)
                NewWeapon = new AmmoWeapon(facing, weapon as TorpedoTubes, enabled);
            else if (weapon.Type == WeaponType.LandingBay)
                NewWeapon = new AmmoWeapon(facing, weapon as LandingBay, enabled);
            else if (weapon.Type == WeaponType.NovaCannon)
                NewWeapon = new NovaCannonTemplate(facing, weapon as NovaCannon, enabled);
            if (NewWeapon != null)
            {
                Grid.SetRow(NewWeapon, WeaponRowCount++);//Add the extra row as you place this one
                Grid.SetColumnSpan(NewWeapon, 7);
                Weapons.Children.Add(NewWeapon);
                if (weapon != null && update)
                {
                    UpdateSP();
                    UpdateUsedSpace();
                    UpdateUsedPower();
                }
            }
        }

        public void UpdateWeapons()
        {
            foreach (IWeaponSlot control in Weapons.Children.OfType<IWeaponSlot>())
            {
                WeaponTemplate weapon = control as WeaponTemplate;
                if (weapon != null)
                    weapon.Macrodamage = starship.MacrobatteryModifier;
                if (control.Weapon != null && (starship.TargettingMatrix != Quality.Poor || control.Equals(Weapons.Children[starship.Matrix + 7])))
                {
                    control.Weapon.TargettingMatrix = starship.TargettingMatrix;
                }
            }
        }
        #endregion

        #region Supplementals
        public void AddNewSupplemental(Supplemental component, int count = 1, int min = 0, bool update = true)
        {
            SupplementalTemplate template = new SupplementalTemplate(component, count, min);
            Grid.SetRow(template, SupplementalRowCount++);
            Grid.SetColumn(template, 0);
            Supplementals.Children.Add(template);
            if (component.AuxiliaryWeapon != null)
                AddWeapon(WeaponSlot.Auxiliary, component.AuxiliaryWeapon, false);
            if (update)
                UpdateSupplementals();

        }

        public void ClearSupplementals()
        {
            Supplementals.Children.Clear();
            SupplementalRowCount = 0;
        }
        #endregion

        #region buttons
        public void CrewPop_Click(object sender, RoutedEventArgs e)
        {
            starship.CurrentCrew = (new CurrentCounters("Crew Population", starship.CurrentCrew, crewpopulation)).ShowDialog();
            UpdateCrewPopulation();
        }

        public void Morale_Click(object sender, RoutedEventArgs e)
        {
            starship.CurrentMorale = (new CurrentCounters("Morale", starship.CurrentMorale, morale)).ShowDialog();
            UpdateMorale();
        }

        public void HullIntegrity_Click(object sender, RoutedEventArgs e)
        {
            starship.CurrentIntegrity = (new CurrentCounters("Hull Integrity", starship.CurrentIntegrity, hullintegrity)).ShowDialog();
            UpdateHullIntegrity();
        }

        private void Damage_Click(object sender, RoutedEventArgs e)
        {
            DamageBox box = new DamageBox(armour, prowarmour, crewloss, moraleloss);
            bool? ran = box.ShowDialog();
            if (ran.HasValue && ran.Value)
            {
                starship.CurrentIntegrity -= box.HullLost;
                starship.CurrentMorale -= box.MoraleLost;
                starship.CurrentCrew -= box.CrewLost;
                UpdateHullIntegrity();
                UpdateMorale();
                UpdateCrewPopulation();
            }
        }

        private void CrewRating_Click(object sender, RoutedEventArgs e)
        {
            CrewRating dialog = new CrewRating(starship);
            dialog.ShowDialog();
            UpdateCrewRating();
        }

        private void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            Upgrades dialog = new Upgrades(starship);
            dialog.ShowDialog();
            UpdateBackgrounds();
        }

        private void Background_Click(object sender, RoutedEventArgs e)
        {
            Backgrounds dialog = new Backgrounds(starship);
            dialog.ShowDialog();
            UpdateBackgrounds();
        }

        private void Hull_Click(object sender, RoutedEventArgs e)
        {
            HullChooser dialog = new HullChooser(starship, loader.Hulls);
            dialog.ShowDialog();
            UpdateHull();
        }

        private void Machine_Click(object sender, RoutedEventArgs e)
        {
            Complication dialog = Complication.MachineSpirit(starship);
            dialog.ShowDialog();
            UpdateMachine();
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null || starship.Hull.History == ShipHistory.None)
            {
                Complication dialog = Complication.ShipHistory(starship);
                dialog.ShowDialog();
                UpdateHistory();
            }
        }

        private void PlasmaDrive_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.PlasmaDrives.Where(x => (x.HullTypes & starship.Hull.HullTypes) != 0).Highest(), typeof(PlasmaDrive), starship.PlasmaDrive);
                //TODO: show only largest variant engines??
                starship.PlasmaDrive = (PlasmaDrive)dialog.ShowDialog();
                UpdatePlasma();
            }
        }

        private void WarpDrive_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.WarpDrives.Where(x => (x.HullTypes & starship.Hull.HullTypes) != 0).Highest(), typeof(WarpDrive), starship.WarpDrive);
                starship.WarpDrive = (WarpDrive)dialog.ShowDialog();
                UpdateWarp();
            }
        }

        private void GellarField_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.GellarFields.Where(x => (x.HullTypes & starship.Hull.HullTypes) != 0).Highest(), typeof(GellarField), starship.GellarField);
                starship.GellarField = (GellarField)dialog.ShowDialog();
                UpdateGellar();
            }
        }

        private void VoidShield_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.VoidShields.Where(x => (x.HullTypes & starship.Hull.VoidShields) != 0).Highest(), typeof(VoidShield), starship.VoidShield);
                starship.VoidShield = (VoidShield)dialog.ShowDialog();
                UpdateVoid();
            }
        }

        private void ShipBridge_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else 
            {
                Essential dialog = new Essential(loader.Bridges.Where(x => (x.HullTypes & starship.Hull.HullTypes) != 0).Highest(), typeof(Bridge), starship.ShipBridge);
                starship.ShipBridge = (Bridge)dialog.ShowDialog();
                UpdateBridge();
            }
        }

        private void LifeSustainer_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.LifeSustainers.Where(x => (x.HullTypes & starship.Hull.HullTypes) != 0).Highest(), typeof(LifeSustainer), starship.LifeSustainer);
                starship.LifeSustainer = (LifeSustainer)dialog.ShowDialog();
                UpdateLife();
            }
        }

        private void CrewQuarters_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.CrewQuarters.Where(x => (x.HullTypes & starship.Hull.HullTypes) != 0).Highest(), typeof(CrewQuarters), starship.CrewQuarters);
                starship.CrewQuarters = (CrewQuarters)dialog.ShowDialog();
                UpdateQuarters();
            }
        }

        private void AugurArrays_Click(object sender, RoutedEventArgs e)
        {
            if (starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.AugurArrays.Where(x => (x.HullTypes & starship.Hull.HullTypes) != 0).Highest(), typeof(Augur), starship.AugurArrays);
                starship.AugurArrays = (Augur)dialog.ShowDialog();
                UpdateAugurs();
            }
        }
        #endregion
    }
}
