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
using StarshipGenerator.Ammo;
using StarshipGenerator.Components;
using StarshipGenerator.Utils;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for AmmoWeapon.xaml
    /// </summary>
    public partial class AmmoWeapon : UserControl, IWeaponSlot
    {
        public Weapon Weapon
        {
            get
            {
                return _weapon;
            }
            set
            {
                _weapon = value;
                UpdateWeapon();
            }
        }
        private Weapon _weapon;
        public WeaponSlot WeaponFacing { get; set; }
        private bool Tubes;//if torpedo tubes or landing bay true for tubes

        public AmmoWeapon(WeaponSlot facing, TorpedoTubes weapon)
            : this(facing, (Weapon)weapon)
        {
            AmmoButton.Content = "Torpedoes";
            Tubes = true;
        }

        public AmmoWeapon(WeaponSlot facing, LandingBay weapon)
            : this(facing, (Weapon)weapon)
        {
            AmmoButton.Content = "Squadrons";
            Tubes = false;
        }

        /// <summary>
        /// Public constructors called for TorpedoTubes or Landing Bay, private constructor to handle either in same manner as WeaponTemplate
        /// </summary>
        /// <param name="facing">Weapon slot</param>
        /// <param name="weapon">Torpedo tube or Landing Bay</param>
        private AmmoWeapon(WeaponSlot facing, Weapon weapon)
        {
            InitializeComponent();
            this.WeaponFacing = facing;
            Facing.Content = WeaponFacing.ToString();
            this.Weapon = weapon;
        }

        public void UpdateWeapon()
        {
            if (Weapon == null)
            {
                WeaponChoice.Content = "Weapon";
                WeaponType.Text = "";
                WeaponStrength.Text = "";
            }
            else
            {
                WeaponChoice.Content = Weapon.QualityName;
                WeaponType.Text = Weapon.Type.Name();
                WeaponStrength.Text = Weapon.Strength.ToString();
            }
        }

        private void Ammo_Click(object sender, RoutedEventArgs e)
        {
            //Open an ammo dialog
            if (Tubes)
            {
                //...for torpedo tuebs
            }
            else
            {
                //...for landing bays
            }
        }
    }
}
