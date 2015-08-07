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
        public int WeaponRowCount { get; set; }
        public int SupplementalRowCount { get; set; }

        public StarshipCreator()
        {
            loader = new Loader();//initialise with default components TODO:Loading of saved custom components
            starship = new Starship();//fresh starship
            WeaponRowCount = 1;
            SupplementalRowCount = 0;
            InitializeComponent();
            //for test
            starship.Hull = loader.Hulls.Where(x => x.Name.Equals("Mars-class Battlecruiser", StringComparison.OrdinalIgnoreCase)).First();
            starship.PlasmaDrive = loader.PlasmaDrives.Where(x => (starship.Hull.HullTypes & x.HullTypes) != 0).First();
            UpdateHull();
            UpdatePlasma();
        }

        #region UpdateFields
        //TODO: Binding for Name
        private int usedspace = 0;
        private int maxspace = 0;
        private int usedpower = 0;
        private int maxpower = 0;
        private int crewpopulation = 0;
        private int morale = 0;
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
            CrewPop.Text = starship.CurrentCrew + "/" + crewpopulation;
        }

        public void UpdateMaxCrew()
        {
            crewpopulation = starship.CrewPopulation;
            UpdateCrewPopulation();
        }

        public void UpdateMorale()
        {
            Morale.Text = starship.CurrentMorale + "/" + morale;
        }

        public void UpdateMaxMorale()
        {
            morale = starship.Morale;
            UpdateMorale();
        }

        public void UpdateHullIntegrity()
        {
            HullInt.Text = starship.CurrentIntegrity + "/" + hullintegrity;
        }

        public void UpdateMaxHullIntegrity()
        {
            hullintegrity = starship.HullIntegrity;
            UpdateHullIntegrity();
        }

        public void UpdateHull()
        {
            HullName.Text = starship.Hull.Name;
            HullClass.Text = starship.Hull.HullTypes.HighName();
            UpdateSP();
            UpdateMaxSpace();
            UpdateMaxPower();
            UpdateUsedPower();
            UpdateMaxHullIntegrity();
            UpdateSpeed();
            UpdateMan();
            UpdateDet();
            UpdateArmour();
            UpdateTurrets();
            UpdateWeaponSlots();
            //UpdateComponents?
            UpdateCommand();
            UpdateHistory();
            UpdateRamming();
            UpdateBS();
            UpdateNavigateWarp();
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

        public void UpdateMachine()
        {
            MachineSpirit.Text = starship.MachineSpirit.Name() + ": " + starship.MachineSpirit.Special();
        }

        public void UpdateHistory()
        {
            ShipHistory.Text = starship.ShipHistory.Name() + ": " + starship.ShipHistory.Special();
        }

        public void UpdatePlasma()
        {
            PlasmaDrive.Content = starship.PlasmaDrive.Name;
            PlasmaSpecial.Text = starship.PlasmaDrive.Special;
            UpdateSP();
            UpdateMaxPower();
            UpdateUsedSpace();
            UpdateSpeed();
            UpdateMan();
        }

        public void UpdateWarp()
        {
            WarpDrive.Content = starship.WarpDrive.Name;
            WarpSpecial.Text = starship.WarpDrive.Special;
            UpdateSP();
            UpdateUsedPower();
            UpdateUsedSpace();
        }

        public void UpdateGellar()
        {
            GellarField.Content = starship.GellarField.Name;
            GellarSpecial.Text = starship.GellarField.Special;
            UpdateSP();
            UpdateUsedPower();
            UpdateNavigateWarp();
        }

        public void UpdateVoid()
        {
            VoidShield.Content = starship.VoidShield.Name;
            VoidSpecial.Text = starship.VoidShield.Special;
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateShields();
        }

        public void UpdateBridge()
        {
            ShipBridge.Content = starship.ShipBridge.Name;
            BridgeSpecial.Text = starship.ShipBridge.Special;
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
            LifeSustainer.Content = starship.LifeSustainer.Name;
            SustainerSpecial.Text = starship.LifeSustainer.Special;
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateCrewLoss();
            UpdateMaxMorale();
            UpdateMoraleLoss();
        }

        public void UpdateQuarters()
        {
            CrewQuarters.Content = starship.CrewQuarters.Name;
            QuartersSpecial.Text = starship.CrewQuarters.Special;
            UpdateSP();
            UpdateUsedSpace();
            UpdateUsedPower();
            UpdateMaxMorale();
            UpdateMoraleLoss();
        }

        public void UpdateAugurs()
        {
            AugurArrays.Content = starship.AugurArrays.Name;
            AugurSpecial.Text = starship.AugurArrays.Special;
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

        public void UpdateMining(){
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

        //add weapon

        //add component
        #endregion

        #region Weapons
        public void UpdateWeaponSlots()
        {
            Weapons.Children.RemoveRange(8,Weapons.Children.Count-7);
            if (starship.Hull != null)
            {
                for (int i = 0; i < starship.Hull.ProwSlots; i++)
                {
                    if (i == 0)
                        AddWeapon(WeaponSlot.Prow, starship.Hull.DefaultProw);
                    else
                        AddWeapon(WeaponSlot.Prow);
                }
                for (int i = 0; i < starship.Hull.DorsalSlots; i++)
                    AddWeapon(WeaponSlot.Dorsal);
                for (int i = 0; i < starship.Hull.SideSlots; i++)
                {
                    if (i == 0)
                        AddWeapon(WeaponSlot.Port, starship.Hull.DefaultBroadside);
                    else
                        AddWeapon(WeaponSlot.Port);
                }
                for (int i = 0; i < starship.Hull.SideSlots; i++)
                {
                    if (i == 0)
                        AddWeapon(WeaponSlot.Starboard, starship.Hull.DefaultBroadside);
                    else
                        AddWeapon(WeaponSlot.Starboard);
                }
                for (int i = 0; i < starship.Hull.KeelSlots; i++)
                    AddWeapon(WeaponSlot.Keel);
                for (int i = 0; i < starship.Hull.AftSlots; i++)
                    AddWeapon(WeaponSlot.Aft);
            }
        }

        public void AddWeapon(WeaponSlot facing, Weapon weapon=null)
        {
            UserControl NewWeapon = null;
            if (weapon == null || weapon.Type == WeaponType.Macrobattery || weapon.Type == WeaponType.Lance)
                NewWeapon = new WeaponTemplate(facing, weapon);
            else if (weapon.Type == WeaponType.TorpedoTube)
                NewWeapon = new AmmoWeapon(facing, weapon as TorpedoTubes);
            else if (weapon.Type == WeaponType.LandingBay)
                NewWeapon = new AmmoWeapon(facing, weapon as LandingBay);
            else if (weapon.Type == WeaponType.NovaCannon)
                NewWeapon = new NovaCannonTemplate(facing, weapon as NovaCannon);
            if (NewWeapon != null)
            {
                Grid.SetRow(NewWeapon, WeaponRowCount++);//Add the extra row as you place this one
                Grid.SetColumnSpan(NewWeapon, 7);
                Weapons.Children.Add(NewWeapon);
                if (weapon != null)
                {
                    UpdateSP();
                    UpdateUsedSpace();
                    UpdateUsedPower();
                }
            }
        }
        #endregion

        #region Clear
        public void ClearHull()
        {
            HullName.Text = "None";
            HullClass.Text = "";
            Space.Text = "";
            Manoeuvrability.Text = "";
            Detection.Text = "";
            Armour.Text = "";
            Turrets.Text = "";
        }

        public void ClearMachine()
        {
            MachineSpirit.Text = "Machine Spirit";
        }

        public void ClearHistory()
        {
            ShipHistory.Text = "Ship History";
        }

        public void ClearPlasma()
        {
            PlasmaDrive.Content = "Plasma Drive";
            PlasmaSpecial.Text = "";
        }

        public void ClearWarp() 
        {
            WarpDrive.Content = "Warp Drive";
            WarpSpecial.Text = "";
        }

        public void ClearGellar()
        {
            GellarField.Content = "Gellar Field";
            GellarSpecial.Text = "";
        }

        public void ClearBridge()
        {
            ShipBridge.Content = "Ship's Bridge";
            BridgeSpecial.Text = "";
        }

        public void ClearSustainer()
        {
            LifeSustainer.Content = "Life Sustainer";
            SustainerSpecial.Text = "";
        }

        public void ClearQuarters()
        {
            CrewQuarters.Content = "Crew Quarters";
            QuartersSpecial.Text = "";
        }

        public void ClearAugurs()
        {
            AugurArrays.Content = "Augur Arrays";
            AugurSpecial.Text = "";
        }
        #endregion
    }
}
