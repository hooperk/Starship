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
using StarshipGenerator.Components;
using StarshipGenerator.Utils;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for NovaCannonTemplate.xaml
    /// </summary>
    public partial class NovaCannonTemplate : UserControl, IWeaponSlot
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
        StarshipCreator Parent;
        int Index;

        public NovaCannonTemplate(WeaponSlot facing, StarshipCreator parent, int index, NovaCannon weapon, bool enabled = true)//Also pass in method for button - do after making said method
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
                WeaponDamage.Text = "";
                WeaponRange.Text = "";
                WeaponSpecial.Text = "";
                WeaponSpecial.Visibility = Visibility.Collapsed;
            }
            else
            {
                WeaponChoice.Content = Weapon.QualityName;
                WeaponName.Text = Weapon.QualityName;
                WeaponType.Text = Weapon.Type.Name();
                WeaponDamage.Text = Weapon.Damage.ToString();
                WeaponRange.Text = Weapon.DisplayRange;
                string special = Weapon.Special;
                WeaponSpecial.Text = special;
                if (!String.IsNullOrWhiteSpace(special))
                    WeaponSpecial.Visibility = Visibility.Visible;
                else
                    WeaponSpecial.Visibility = Visibility.Collapsed;
            }
        }

        private void WeaponChoice_Click(object sender, RoutedEventArgs e)
        {
            Parent.ChangeWeapon(Index);
        }
    }
}
