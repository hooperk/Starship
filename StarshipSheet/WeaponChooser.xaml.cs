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
            button = new Button();
            button.Content = "Clear Weapon";
            button.Click += ((s, e) => SetCurrent(null));
            Grid.SetRow(button, WeaponRowCount);
            Grid.SetColumn(button, 0);
            WeaponGrid.Children.Add(button);
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
                    button = new Button();
                    button.Content = weapon.Name;
                    button.Click += ((s,e) => SetCurrent(weapon));
                    Grid.SetRow(button, WeaponRowCount);
                    Grid.SetColumn(button, 0);
                    WeaponGrid.Children.Add(button);
                    textbox = new TextBox();
                    textbox.Text = weapon.Type.ToString();
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 1);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = weapon.Power.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 2);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = weapon.Space.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 3);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = weapon.SP.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 4);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = weapon.Strength.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 5);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = weapon.Damage.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 6);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = weapon.DisplayRange;
                    textbox.TextAlignment = TextAlignment.Center;
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 7);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = weapon.Crit.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 8);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = weapon.Origin.Name();
                    textbox.ToolTip = weapon.Origin.LongName() + ", Page: " + weapon.PageNumber;
                    Grid.SetRow(textbox, WeaponRowCount++);
                    Grid.SetColumn(textbox, 10);
                    WeaponGrid.Children.Add(textbox);
                    if (!String.IsNullOrWhiteSpace(weapon.Description))
                    {
                        textblock = new TextBlock();
                        textblock.Text = weapon.Description;
                        textblock.Margin = new Thickness(2, 2, 2, 2);
                        textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                        Grid.SetRow(textblock, WeaponRowCount++);
                        Grid.SetColumnSpan(textblock, 11);
                        WeaponGrid.Children.Add(textblock);
                    }
                }
            }
            Current = Starship.Weapons[Index];
            UpdateCurrent();
        }

        private void SetCurrent(Weapon weapon)
        {
            Current = weapon;
            UpdateCurrent();
        }

        private void ChangeQuality(Quality quality, WeaponQuality wq)
        {
            if (Current == null)
                return;
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
            UpdateCurrent();
        }

        private void UpdateCurrent()
        {
            if (Current == null)
            {
                CurrentName.Text = "";
                CurrentType.Text = "";
                CurrentPower.Text = "";
                CurrentSpace.Text = "";
                CurrentSP.Text = "";
                CurrentStr.Text = "";
                CurrentDamage.Text = "";
                CurrentDamage.ToolTip = null;
                CurrentCrit.Text = "";
                CurrentCrit.ToolTip = null;
                CurrentRange.Text = "";
                CurrentRange.ToolTip = null;
                CurrentQuality.Text = "Choose Quality";
                CurrentSpecial.Text = "";
            }
            else
            {
                CurrentName.Text = Current.Name;
                CurrentType.Text = Current.Type.ToString();
                CurrentPower.Text = Current.Power.ToString();
                CurrentSpace.Text = Current.Space.ToString();
                CurrentSP.Text = Current.SP.ToString();
                CurrentStr.Text = Current.SP.ToString();
                if (Current.Type == WeaponType.LandingBay)
                {
                    CurrentDamage.Text = "";
                    CurrentDamage.ToolTip = "Landing Bays do not deal damage directly and don't have their own damage rating";
                    CurrentCrit.Text = "";
                    CurrentCrit.ToolTip = "Landing Bays do not deal damage directly and don't have their own crit rating";
                    CurrentRange.Text = "";
                    CurrentRange.ToolTip = "Landing Bays do not have a range; They release their attack craft which then fly on their own power until returning or dying";
                }
                else if (Current.Type == WeaponType.TorpedoTube)
                {
                    CurrentDamage.Text = "";
                    CurrentDamage.ToolTip = "Damage rating is determined by the Torpedoes equipped so Torpedo Tubes have no damage rating";
                    CurrentCrit.Text = "";
                    CurrentCrit.ToolTip = "Crit rating is determined by the Torpedoes equipped so Torpedo Tubes have no crit rating";
                    CurrentRange.Text = "";
                    CurrentRange.ToolTip = "Torpedo Tubes do not have a range; They fire the torpedoes whcih travel under their own power until they hit something or reach their own range";
                }
                else
                {
                    CurrentDamage.Text = Current.Damage.ToString();
                    CurrentDamage.ToolTip = null;
                    if (Current.Type == WeaponType.NovaCannon)
                    {
                        CurrentCrit.Text = "";
                        CurrentCrit.ToolTip = "Any damage dice which roll a 10 will inflict a critical hit, and this can cause multiple critical hits";
                        CurrentRange.ToolTip = "Nove Cannons have a minimum range of 6 VUs";
                    }
                    else
                    {
                        CurrentCrit.Text = Current.Crit.ToString();
                        CurrentCrit.Text = Current.Crit == 0 ? "This weapon cannot critically hit" : null;
                        CurrentRange.ToolTip = null;
                    }
                    CurrentRange.Text = Current.DisplayRange;
                }
                CurrentQuality.Text = Current.Quality + " Quality";
                CurrentSpecial.Text = Current.Description;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogResult ?? false)
                Starship.Weapons[Index] = Current;
        }

        private void Quality_Click(object sender, RoutedEventArgs e)
        {
            WeaponQualityChooser dialog = new WeaponQualityChooser(Current.Type, Current.Quality, Current.WeaponQuality);
            Tuple<Quality, WeaponQuality> output = dialog.ShowDialog();
            ChangeQuality(output.Item1, output.Item2);
        }
    }
}
