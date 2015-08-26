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
using StarshipGenerator.Components;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for WeaponChooser.xaml
    /// </summary>
    public partial class WeaponChooser : Window
    {
        Starship Starship;
        int Index;
        public int WeaponRowCount { get; set; }
        Weapon Current;

        public WeaponChooser(Starship starship, WeaponSlot slot, int index, Loader loader)
        {
            if (starship == null || starship.Hull == null)
                throw new ArgumentException("Cannot create weapon chooser without starship or hull");
            this.Starship = starship;
            this.Index = index;
            WeaponRowCount = 0;
            InitializeComponent();
            Label label;
            Button button;
            TextBox textbox;
            TextBlock textblock;
            foreach (var group in loader.Weapons.Where(x => (x.HullTypes & starship.Hull.HullTypes) != 0 && (x.Slots & slot) != 0).GroupBy(x => x.ComponentOrigin).OrderBy(x => x.Key))
            {
                if (group.Key != ComponentOrigin.Standard)
                {
                    label = new Label();
                    label.Content = group.Key.ToString();
                    Grid.SetRow(label, WeaponRowCount++);
                    Grid.SetColumn(label, 0);
                    WeaponGrid.Children.Add(label);
                }
                foreach (Weapon weapon in group)
                {
                    //programatically show all buttons
                }
            }
        }

        private void SetCurrent(Weapon weapon)
        {
            Current = weapon;
        }

        private void ChangeQuality(Quality quality, WeaponQuality wq)
        {
            if (Current is NovaCannon)
                Current = new NovaCannon(Current.Name, Current.HullTypes, Current.RawPower, Current.RawSpace, Current.RawSP, Current.RawDamage, Current.RawRange, Current.Origin, Current.PageNumber, Current.RawSpecial,
                    quality, wq, Current.ComponentOrigin, Current.Condition, ((NovaCannon)Current).Ammo);
            else if (Current is LandingBay)
                Current = new LandingBay(Current.Name, Current.HullTypes, Current.Slots, Current.RawPower, Current.RawSpace, Current.RawSP, Current.RawStrength, Current.Origin, Current.PageNumber, quality, wq,
                    Current.RawSpecial, Current.ComponentOrigin, Current.Condition);
            else if (Current is TorpedoTubes)
                Current = new TorpedoTubes(Current.Name, Current.HullTypes, Current.RawPower, Current.RawSpace, Current.RawSP, Current.RawStrength, ((TorpedoTubes)Current).Capacity, Current.Origin, Current.PageNumber, 
                    quality, wq, Current.RawSpecial, Current.ComponentOrigin, Current.Condition);
            else
                Current = new Weapon(Current.Name, Current.Type, Current.HullTypes, Current.Slots, Current.RawPower, Current.RawSpace, Current.RawSP, Current.RawStrength, Current.RawDamage, Current.RawCrit, Current.RawRange,
                Current.Origin, Current.PageNumber, quality, wq, Current.RawSpecial, Quality.None, Current.ComponentOrigin, Current.Condition);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogResult ?? false)
                Starship.Weapons[Index] = Current;
        }
    }
}
