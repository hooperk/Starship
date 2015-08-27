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
        public Starship Starship;
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
            Starship = new Starship();//fresh starship
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
            SP.Text = Starship.SP.ToString();
        }

        public void UpdateSpace()
        {
            Space.Text = usedspace + "/" + maxspace;
        }

        public void UpdateUsedSpace()
        {
            usedspace = Starship.UsedSpace;
            UpdateSpace();
        }

        public void UpdateMaxSpace()
        {
            maxspace = Starship.MaxSpace;
            UpdateSpace();
        }

        public void UpdatePower()
        {
            Power.Text = usedpower + "/" + maxpower;
        }

        public void UpdateUsedPower()
        {
            usedpower = Starship.UsedPower;
            UpdatePower();
        }

        public void UpdateMaxPower()
        {
            maxpower = Starship.MaxPower;
            UpdatePower();
        }

        public void UpdateCrewPopulation()
        {
            if (Starship.CurrentCrew > crewpopulation)
                Starship.CurrentCrew = crewpopulation;
            CrewPop.Text = Starship.CurrentCrew + "/" + crewpopulation;

        }

        public void UpdateMaxCrew()
        {
            crewpopulation = Starship.CrewPopulation;
            UpdateCrewPopulation();
        }

        public void UpdateMorale()
        {
            if (Starship.CurrentMorale > morale)
                Starship.CurrentMorale = morale;
            Morale.Text = Starship.CurrentMorale + "/" + morale;
        }

        public void UpdateMaxMorale()
        {
            morale = Starship.Morale;
            UpdateMorale();
        }

        public void UpdateHullIntegrity()
        {
            if (Starship.CurrentIntegrity > hullintegrity)
                Starship.CurrentIntegrity = hullintegrity;
            HullInt.Text = Starship.CurrentIntegrity + "/" + hullintegrity;
        }

        public void UpdateMaxHullIntegrity()
        {
            hullintegrity = Starship.HullIntegrity;
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
            if (Starship.Hull != null)
            {
                HullName.Text = Starship.Hull.Name;
                HullClass.Text = Starship.Hull.HullTypes.HighName();
                if (Starship.SupplementalComponents != null)
                {
                    foreach (String name in Starship.SupplementalComponents.Select(x => x.Name).Distinct())
                    {
                        IEnumerable<Supplemental> components = Starship.SupplementalComponents.Where(x => x.Name.Equals(name));
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
            if (Starship.Hull != null && Starship.Hull.History != ShipHistory.None)
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
            if (Starship.CurrentIntegrity == 0 || Starship.CurrentIntegrity > hullintegrity)
                Starship.CurrentIntegrity = hullintegrity;
            if (Starship.CurrentCrew == 0 && Starship.CurrentMorale == 0)
            {
                Starship.CurrentCrew = crewpopulation;
                Starship.CurrentMorale = morale;
            }
        }

        public void UpdateHullSpecial()
        {
            if (Starship.Hull == null || (Starship.Hull.Special == null && Starship.GMSpecial == null))
                HullSpecial.Visibility = Visibility.Collapsed;
            else
            {
                HullSpecialText.Text = Starship.Hull.Special + Starship.GMSpecial;
                HullSpecial.Visibility = Visibility.Visible;
            }

        }

        public void UpdateSpeed()
        {
            Speed.Text = Starship.Speed.ToString();
        }

        public void UpdateMan()
        {
            Manoeuvrability.Text = Starship.Manoeuvrability.ToString();
        }

        public void UpdateDet()
        {
            Detection.Text = Starship.DetectionRating.ToString();
        }

        public void UpdateArmour()
        {
            armour = Starship.Armour;
            prowarmour = Starship.ProwArmour;
            Armour.Text = armour + (armour != prowarmour ? "(" + prowarmour + ")" : "");
        }

        public void UpdateTurrets()
        {
            Turrets.Text = Starship.TurretRating.ToString();
        }

        public void UpdateShields()
        {
            Shields.Text = Starship.Shields.ToString();
        }

        public void UpdateCrewRating()
        {
            CrewRating.Text = Starship.CrewValue.ToString();
        }

        public void UpdateMachine(bool update = true)
        {
            if (!String.IsNullOrWhiteSpace(Starship.GMMachineSpirit))
                MachineSpiritDisplay.Text = Starship.GMMachineSpirit;
            else if (Starship.MachineSpirit == MachineSpirit.None)
            {
                MachineSpiritDisplay.Text = "Machine Spirit";
            }
            else
                MachineSpiritDisplay.Text = Starship.MachineSpirit.Name() + ": " + Starship.MachineSpirit.Special();
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
            if (!String.IsNullOrWhiteSpace(Starship.GMShipHistory))
                ShipHistoryDisplay.Text = Starship.GMShipHistory;
            else if (Starship.ShipHistory == ShipHistory.None)
                ShipHistoryDisplay.Text = "Ship History";
            else
                ShipHistoryDisplay.Text = Starship.ShipHistory.Name() + ": " + Starship.ShipHistory.Special();
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
            if (Starship.PlasmaDrive == null)
            {
                PlasmaDrive.Content = "Choose Plasma Drive";
                PlasmaSpecial.Text = "";
            }
            else
            {
                PlasmaDrive.Content = Starship.PlasmaDrive.QualityName;
                PlasmaSpecial.Text = Starship.PlasmaDrive.Special;
            }
            UpdateSP();
            UpdateMaxPower();
            UpdateUsedSpace();
            UpdateSpeed();
            UpdateMan();
        }

        public void UpdateWarp()
        {
            if (Starship.WarpDrive == null)
            {
                WarpDrive.Content = "Choose Warp Drive";
                WarpSpecial.Text = "";
            }
            else
            {
                WarpDrive.Content = Starship.WarpDrive.QualityName;
                WarpSpecial.Text = Starship.WarpDrive.Special;
            }
            UpdateSP();
            UpdateUsedPower();
            UpdateUsedSpace();
        }

        public void UpdateGellar()
        {
            if (Starship.GellarField == null)
            {
                GellarField.Content = "Choose Gellar Field";
                GellarSpecial.Text = "";
            }
            else
            {
                GellarField.Content = Starship.GellarField.QualityName;
                GellarSpecial.Text = Starship.GellarField.Special;
            }
            UpdateSP();
            UpdateUsedPower();
            UpdateNavigateWarp();
        }

        public void UpdateVoid()
        {
            if (Starship.VoidShield == null)
            {
                VoidShield.Content = "Choose Void Shield";
                VoidSpecial.Text = "";
            }
            else
            {
                VoidShield.Content = Starship.VoidShield.QualityName;
                VoidSpecial.Text = Starship.VoidShield.Special;
            }
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateShields();
        }

        public void UpdateBridge()
        {
            if (Starship.ShipBridge == null)
            {
                ShipBridge.Content = "Choose Ship's Bridge";
                BridgeSpecial.Text = "";
            }
            else
            {
                ShipBridge.Content = Starship.ShipBridge.QualityName;
                BridgeSpecial.Text = Starship.ShipBridge.Special;
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
            if (Starship.LifeSustainer == null)
            {
                LifeSustainer.Content = "Choose Life Sustainer";
                SustainerSpecial.Text = "";
            }
            else
            {
                LifeSustainer.Content = Starship.LifeSustainer.QualityName;
                SustainerSpecial.Text = Starship.LifeSustainer.Special;
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
            if (Starship.CrewQuarters == null)
            {
                CrewQuarters.Content = "Choose Crew Quarters";
                QuartersSpecial.Text = "";
            }
            else
            {
                CrewQuarters.Content = Starship.CrewQuarters.QualityName;
                QuartersSpecial.Text = Starship.CrewQuarters.Special;
            }
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateMaxMorale();
            UpdateMoraleLoss();
        }

        public void UpdateAugurs()
        {
            if (Starship.AugurArrays == null)
            {
                AugurArrays.Content = "Choose Augur Arrays";
                AugurSpecial.Text = "";
            }
            else
            {
                AugurArrays.Content = Starship.AugurArrays.QualityName;
                AugurSpecial.Text = Starship.AugurArrays.Special;
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
            MiningObjective.Text = Print(Starship.MiningObjective);
        }

        public void UpdateCreed()
        {
            CreedObjective.Text = Print(Starship.CreedObjective);
        }

        public void UpdateMilitary()
        {
            MilitaryObjective.Text = Print(Starship.MilitaryObjective);
        }

        public void UpdateTrade()
        {
            TradeObjective.Text = Print(Starship.TradeObjective);
        }

        public void UpdateCriminal()
        {
            TradeObjective.Text = Print(Starship.CriminalObjective);
        }

        public void UpdateExploration()
        {
            TradeObjective.Text = Print(Starship.ExplorationObjective);
        }

        public void UpdateRamming()
        {
            Ramming.Text = Starship.Ramming.ToString();
        }

        public void UpdateBS()
        {
            BSModifier.Text = Print(Starship.BSModifier);
        }

        public void UpdateCommand()
        {
            Command.Text = Print(Starship.Command);
        }

        public void UpdateCrewLoss()
        {
            crewloss = Starship.CrewLoss;
            CrewLoss.Text = Print(crewloss);
        }

        public void UpdateMoraleLoss()
        {
            moraleloss = Starship.MoraleLoss;
            MoraleLoss.Text = Print(moraleloss);
        }

        public void UpdatePilot()
        {
            PilotTest.Text = Print(Starship.PilotTest);
        }

        public void UpdateRepair()
        {
            RepairTest.Text = Print(Starship.RepairTest);
        }

        public void UpdateNavigateWarp()
        {
            NavigateWarp.Text = Print(Starship.NavigateTest);
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
            CrewRating.Text = Starship.CrewValue.ToString();
        }

        #endregion

        #region Weapons
        public void UpdateWeaponSlots(bool canupdate = true)
        {
            WeaponRowCount = 1;
            Weapons.Children.RemoveRange(8, Weapons.Children.Count - 7);
            if (Starship.Hull != null)
            {
                bool update = false;
                int count = 0;
                Weapon weapon;
                for (int i = 0; i < Starship.Hull.ProwSlots; i++)
                {
                    weapon = Starship.WeaponList[count].Item2;
                    if (weapon != null)
                        update = true;
                    if (i == 0 && Starship.Hull.DefaultProw != null)
                        AddWeapon(WeaponSlot.Prow, weapon, count++, false, false);
                    else
                        AddWeapon(WeaponSlot.Prow, weapon, count++, false);
                }
                for (int i = 0; i < Starship.Hull.DorsalSlots; i++)
                {
                    weapon = Starship.WeaponList[count].Item2;
                    if (weapon != null)
                        update = true;
                    AddWeapon(WeaponSlot.Dorsal, weapon, count++, false);
                }
                for (int i = 0; i < Starship.Hull.SideSlots; i++)
                {
                    weapon = Starship.WeaponList[count].Item2;
                    if (weapon != null)
                        update = true;
                    if (i == 0 && Starship.Hull.DefaultBroadside != null)
                        AddWeapon(WeaponSlot.Port, weapon, count++, false, false);
                    else
                        AddWeapon(WeaponSlot.Port, weapon, count++, false);
                }
                for (int i = 0; i < Starship.Hull.SideSlots; i++)
                {
                    weapon = Starship.WeaponList[count].Item2;
                    if (weapon != null)
                        update = true;
                    if (i == 0 && Starship.Hull.DefaultBroadside != null)
                        AddWeapon(WeaponSlot.Starboard, weapon, count++, false, false);
                    else
                        AddWeapon(WeaponSlot.Starboard, weapon, count++, false);
                }
                for (int i = 0; i < Starship.Hull.KeelSlots; i++)
                {
                    weapon = Starship.WeaponList[count].Item2;
                    if (weapon != null)
                        update = true;
                    AddWeapon(WeaponSlot.Keel, weapon, count++, false);
                }
                for (int i = 0; i < Starship.Hull.AftSlots; i++)
                {
                    weapon = Starship.WeaponList[count].Item2;
                    if (weapon != null)
                        update = true;
                    AddWeapon(WeaponSlot.Aft, weapon, count++, false);
                }
                foreach (Weapon aux in Starship.WeaponList.Where(x => x.Item1 == WeaponSlot.Auxiliary).Select(x => x.Item2))
                {
                    weapon = Starship.WeaponList[count].Item2;
                    if (weapon != null)
                        update = true;
                    AddWeapon(WeaponSlot.Auxiliary, weapon, count++, false, false);
                }
                if (update && canupdate)
                {
                    UpdateSP();
                    UpdateUsedSpace();
                    UpdateUsedPower();
                }
            }
        }

        public void AddWeapon(WeaponSlot facing, Weapon weapon, int count, bool update = true, bool enabled = true)
        {
            UserControl NewWeapon = null;
            if (weapon == null || weapon.Type == WeaponType.Macrobattery || weapon.Type == WeaponType.Lance)
                NewWeapon = new WeaponTemplate(facing, this, count, weapon, Starship.MacrobatteryModifier);
            else if (weapon.Type == WeaponType.TorpedoTube)
                NewWeapon = new AmmoWeapon(facing, this, count, weapon as TorpedoTubes, enabled);
            else if (weapon.Type == WeaponType.LandingBay)
                NewWeapon = new AmmoWeapon(facing, this, count, weapon as LandingBay, enabled);
            else if (weapon.Type == WeaponType.NovaCannon)
                NewWeapon = new NovaCannonTemplate(facing, this, count, weapon as NovaCannon, enabled);
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
                    weapon.Macrodamage = Starship.MacrobatteryModifier;
                if (control.Weapon != null)
                {
                    if (Starship.TargettingMatrix != Quality.Poor || control.Equals(Weapons.Children[Starship.Matrix + 7]))
                        control.Weapon.TargettingMatrix = Starship.TargettingMatrix;
                    else
                        control.Weapon.TargettingMatrix = Quality.None;
                }
            }
        }

        public void ChangeWeapon(int index)
        {
            WeaponSlot weaponType = Starship.WeaponList[index].Item1;
            if (weaponType == WeaponSlot.Prow)//Heavy weapons can be on all prows if they can be on a ship
                weaponType |= WeaponSlot.Heavy;
            if (((Starship.Hull.HullTypes & HullType.AllCruiser) != 0) || (weaponType & WeaponSlot.Prow) != 0)//lances can be on all prows or any slot of ships bigger than frigates, raiders or transports
                weaponType |= WeaponSlot.Lance;
            if ((Starship.Hull.HullTypes & (HullType.GrandCruiser | HullType.BattleShip)) != 0 && (weaponType & WeaponSlot.Dorsal) != 0)//Grand cruisers may have heavy weapons in dorsal slots
                weaponType |= WeaponSlot.Heavy;
            //Open weapon chooser
            WeaponChooser dialog = new WeaponChooser(Starship, weaponType, index, loader);
            dialog.ShowDialog();
            if (dialog.DialogResult ?? false)
                UpdateWeaponSlots(true);      
        }
        #endregion

        #region Supplementals
        public void AddNewSupplemental(Supplemental component, int count = 1, int min = 0, bool update = true)
        {
            SupplementalTemplate template = new SupplementalTemplate(this, component, count, min);
            Grid.SetRow(template, SupplementalRowCount++);
            Grid.SetColumn(template, 0);
            Supplementals.Children.Add(template);
            if (component.AuxiliaryWeapon != null)
                AddWeapon(WeaponSlot.Auxiliary, component.AuxiliaryWeapon, -1, false, false);
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
            Starship.CurrentCrew = (new CurrentCounters("Crew Population", Starship.CurrentCrew, crewpopulation)).ShowDialog();
            UpdateCrewPopulation();
        }

        public void Morale_Click(object sender, RoutedEventArgs e)
        {
            Starship.CurrentMorale = (new CurrentCounters("Morale", Starship.CurrentMorale, morale)).ShowDialog();
            UpdateMorale();
        }

        public void HullIntegrity_Click(object sender, RoutedEventArgs e)
        {
            Starship.CurrentIntegrity = (new CurrentCounters("Hull Integrity", Starship.CurrentIntegrity, hullintegrity)).ShowDialog();
            UpdateHullIntegrity();
        }

        private void Damage_Click(object sender, RoutedEventArgs e)
        {
            DamageBox box = new DamageBox(armour, prowarmour, crewloss, moraleloss);
            bool? ran = box.ShowDialog();
            if (ran.HasValue && ran.Value)
            {
                Starship.CurrentIntegrity -= box.HullLost;
                Starship.CurrentMorale -= box.MoraleLost;
                Starship.CurrentCrew -= box.CrewLost;
                UpdateHullIntegrity();
                UpdateMorale();
                UpdateCrewPopulation();
            }
        }

        private void CrewRating_Click(object sender, RoutedEventArgs e)
        {
            CrewRating dialog = new CrewRating(Starship);
            dialog.ShowDialog();
            UpdateCrewRating();
        }

        private void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            Upgrades dialog = new Upgrades(Starship);
            dialog.ShowDialog();
            UpdateBackgrounds();
        }

        private void Background_Click(object sender, RoutedEventArgs e)
        {
            Backgrounds dialog = new Backgrounds(Starship);
            dialog.ShowDialog();
            UpdateBackgrounds();
        }

        private void Hull_Click(object sender, RoutedEventArgs e)
        {
            HullChooser dialog = new HullChooser(Starship, loader.Hulls);
            dialog.ShowDialog();
            UpdateHull();
        }

        private void Machine_Click(object sender, RoutedEventArgs e)
        {
            Complication dialog = Complication.MachineSpirit(Starship);
            dialog.ShowDialog();
            UpdateMachine();
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null || Starship.Hull.History == ShipHistory.None)
            {
                Complication dialog = Complication.ShipHistory(Starship);
                dialog.ShowDialog();
                UpdateHistory();
            }
        }

        private void PlasmaDrive_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.PlasmaDrives.Where(x => (x.HullTypes & Starship.Hull.HullTypes) != 0).Highest(), typeof(PlasmaDrive), Starship.PlasmaDrive);
                //TODO: show only largest variant engines??
                Starship.PlasmaDrive = (PlasmaDrive)dialog.ShowDialog();
                UpdatePlasma();
            }
        }

        private void WarpDrive_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.WarpDrives.Where(x => (x.HullTypes & Starship.Hull.HullTypes) != 0).Highest(), typeof(WarpDrive), Starship.WarpDrive);
                Starship.WarpDrive = (WarpDrive)dialog.ShowDialog();
                UpdateWarp();
            }
        }

        private void GellarField_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.GellarFields.Where(x => (x.HullTypes & Starship.Hull.HullTypes) != 0).Highest(), typeof(GellarField), Starship.GellarField);
                Starship.GellarField = (GellarField)dialog.ShowDialog();
                UpdateGellar();
            }
        }

        private void VoidShield_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.VoidShields.Where(x => (x.HullTypes & Starship.Hull.VoidShields) != 0).Highest(), typeof(VoidShield), Starship.VoidShield);
                Starship.VoidShield = (VoidShield)dialog.ShowDialog();
                UpdateVoid();
            }
        }

        private void ShipBridge_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.Bridges.Where(x => (x.HullTypes & Starship.Hull.HullTypes) != 0).Highest(), typeof(Bridge), Starship.ShipBridge);
                Starship.ShipBridge = (Bridge)dialog.ShowDialog();
                UpdateBridge();
            }
        }

        private void LifeSustainer_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.LifeSustainers.Where(x => (x.HullTypes & Starship.Hull.HullTypes) != 0).Highest(), typeof(LifeSustainer), Starship.LifeSustainer);
                Starship.LifeSustainer = (LifeSustainer)dialog.ShowDialog();
                UpdateLife();
            }
        }

        private void CrewQuarters_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.CrewQuarters.Where(x => (x.HullTypes & Starship.Hull.HullTypes) != 0).Highest(), typeof(CrewQuarters), Starship.CrewQuarters);
                Starship.CrewQuarters = (CrewQuarters)dialog.ShowDialog();
                UpdateQuarters();
            }
        }

        private void AugurArrays_Click(object sender, RoutedEventArgs e)
        {
            if (Starship.Hull == null)
                MessageBox.Show("Can't select components until you've selected a hull");
            else
            {
                Essential dialog = new Essential(loader.AugurArrays.Where(x => (x.HullTypes & Starship.Hull.HullTypes) != 0).Highest(), typeof(Augur), Starship.AugurArrays);
                Starship.AugurArrays = (Augur)dialog.ShowDialog();
                UpdateAugurs();
            }
        }

        private void WeaponUpgrade_Click(object sender, RoutedEventArgs e)
        {
            //Upgrade Dialog

            UpdateWeapons();
            UpdateBS();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Starship.SupplementalComponents.Clear();
            SupplementalRowCount = 0;
            Supplementals.Children.Clear();
            if (Starship.Hull != null && Starship.Hull.DefaultComponents != null)
                foreach (Supplemental component in Starship.Hull.DefaultComponents)
                    Starship.SupplementalComponents.Add(component);
            if (Starship.SupplementalComponents != null)
            {
                foreach (String name in Starship.SupplementalComponents.Select(x => x.Name).Distinct())
                {
                    IEnumerable<Supplemental> components = Starship.SupplementalComponents.Where(x => x.Name.Equals(name));
                    int count = components.Count();
                    AddNewSupplemental(components.First(), count, count, false);
                }
            }
        }
        #endregion
    }
}
