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
        private new StarshipCreator Parent;
        int Index;

        public AmmoWeapon(WeaponSlot facing, StarshipCreator parent, int index, TorpedoTubes weapon, bool enabled = true)
            : this(facing, parent, index, (Weapon)weapon, enabled)
        {
            AmmoButton.Content = "Torpedoes";
            Tubes = true;
        }

        public AmmoWeapon(WeaponSlot facing, StarshipCreator parent, int index, LandingBay weapon, bool enabled = true)
            : this(facing, parent, index, (Weapon)weapon, enabled)
        {
            AmmoButton.Content = "Squadrons";
            Tubes = false;
        }

        /// <summary>
        /// Public constructors called for TorpedoTubes or Landing Bay, private constructor to handle either in same manner as WeaponTemplate
        /// </summary>
        /// <param name="facing">Weapon slot</param>
        /// <param name="weapon">Torpedo tube or Landing Bay</param>
        private AmmoWeapon(WeaponSlot facing, StarshipCreator parent, int index, Weapon weapon, bool enabled)
        {
            this.Parent = parent;
            this.Index = index;
            InitializeComponent();
            this.WeaponFacing = facing;
            Facing.Content = WeaponFacing.ToString();
            this.Weapon = weapon;
            if (!enabled)
            {
                WeaponChoice.Visibility = Visibility.Collapsed;
                WeaponName.Visibility = Visibility.Visible;
            }
        }

        public void UpdateWeapon()
        {
            if (Weapon == null)
            {
                WeaponChoice.Content = "Weapon";
                WeaponType.Text = "";
                WeaponStrength.Text = "";
                WeaponSpecial.Text = "";
                WeaponSpecial.Visibility = Visibility.Collapsed;
            }
            else
            {
                WeaponChoice.Content = Weapon.QualityName;
                WeaponName.Text = Weapon.QualityName;
                WeaponType.Text = Weapon.Type.Name();
                WeaponStrength.Text = Weapon.Strength.ToString();
                string special = Weapon.Special;
                WeaponSpecial.Text = special;
                if (!String.IsNullOrWhiteSpace(special))
                    WeaponSpecial.Visibility = Visibility.Visible;
                else
                    WeaponSpecial.Visibility = Visibility.Collapsed;
            }
        }

        private void Ammo_Click(object sender, RoutedEventArgs e)
        {
            //Open an ammo dialog
            if (Tubes)
            {
                //...for torpedo tubes
            }
            else
            {
                //...for landing bays
            }
        }

        private void WeaponChoice_Click(object sender, RoutedEventArgs e)
        {
            Parent.ChangeWeapon(Index);
        }
    }
}
