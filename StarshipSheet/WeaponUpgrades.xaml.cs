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
using StarshipGenerator.Components;
using StarshipGenerator.Utils;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for WeaponUpgrades.xaml
    /// </summary>
    public partial class WeaponUpgrades : Window
    {
        public int WeaponRowCount { get; set; }
        private Starship Starship;
        private List<Weapon> Weapons;
        private List<TextBox> Ranges;
        private List<TextBox> Ballistics;
        private List<CheckBox> Matrices;
        private readonly List<Quality> Qualities = new List<Quality>() { Quality.None, Quality.Poor, Quality.Common, Quality.Good, Quality.Best };

        public WeaponUpgrades(Starship starship)
        {
            if (starship == null)
                throw new ArgumentException("Cannot make weapon ugprades with a null ship");
            this.Starship = starship;
            this.Weapons = new List<Weapon>();
            this.Ranges = new List<TextBox>();
            this.Ballistics = new List<TextBox>();
            this.Matrices = new List<CheckBox>();
            this.WeaponRowCount = 0;
            InitializeComponent();
            AllCheck.IsChecked = (Starship.TargettingMatrix == Quality.Common);
            Label label;
            TextBox textbox;
            CheckBox checkbox;
            int count = 0;
            foreach (Tuple<WeaponSlot, Weapon> WeaponPackage in Starship.WeaponList)
            {
                if (WeaponPackage.Item2 != null)
                {
                    int index = count;//initialise ints for each time around or lambdas will all use final count value of 3 and be out of bounds
                    ComboBox combobox;//declare memory inside loop for same reason as above
                    Weapons.Add(WeaponPackage.Item2);
                    label = new Label();
                    label.Content = WeaponPackage.Item1.ToString();
                    Grid.SetRow(label, WeaponRowCount);
                    Grid.SetColumn(label, 0);
                    WeaponGrid.Children.Add(label);
                    textbox = new TextBox();
                    textbox.Text = WeaponPackage.Item2.QualityName;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 1);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.IsReadOnly = true;
                    textbox.TextAlignment = TextAlignment.Center;
                    Ranges.Add(textbox);
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 2);
                    WeaponGrid.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.IsReadOnly = true;
                    textbox.TextAlignment = TextAlignment.Center;
                    Ballistics.Add(textbox);
                    Grid.SetRow(textbox, WeaponRowCount);
                    Grid.SetColumn(textbox, 3);
                    WeaponGrid.Children.Add(textbox);
                    combobox = new ComboBox();
                    combobox.ItemsSource = Qualities;
                    combobox.SelectedItem = WeaponPackage.Item2.TurboWeapon;
                    if (WeaponPackage.Item2.Type != WeaponType.Macrobattery)
                    {
                        combobox.IsEnabled = false;
                        combobox.ToolTip = "Only Macrobatteries may have turbo-weapon batteries";
                        ToolTipService.SetShowOnDisabled(combobox, true);
                    }
                    combobox.SelectionChanged += ((s, e) => QualityChanged(combobox, index));
                    Grid.SetRow(combobox, WeaponRowCount);
                    Grid.SetColumn(combobox, 4);
                    WeaponGrid.Children.Add(combobox);
                    checkbox = new CheckBox();
                    checkbox.IsChecked = (Starship.TargettingMatrix == Quality.Common || WeaponPackage.Item2.TargettingMatrix == Quality.Poor);
                    checkbox.Click += ((s, e) => SetMatrixIndex(index));
                    checkbox.ToolTip = "Poor Quality";
                    checkbox.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    checkbox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    checkbox.Margin = new Thickness(17, 0, 0, 0);
                    Matrices.Add(checkbox);
                    Grid.SetRow(checkbox, WeaponRowCount++);
                    Grid.SetColumn(checkbox, 5);
                    WeaponGrid.Children.Add(checkbox);
                    UpdateWeapon(count++);
                }
            }
        }

        private void QualityChanged(ComboBox current, int index)
        {
            Weapons[index].TurboWeapon = (Quality)current.SelectedItem;
            UpdateWeapon(index);
        }

        private void SetCommonMatrix(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Matrices.Count; i++)
            {
                Matrices[i].IsChecked = AllCheck.IsChecked;//If all is either checked or unchecked, make all match
                UpdateWeapon(i);
            }
        }

        private void SetMatrixIndex(int index)
        {
            if ((AllCheck.IsChecked ?? false) || (Matrices[index].IsChecked ?? false))//If currently set to all, make only this one set. If checkbox was just checked, uncheck others
            {
                AllCheck.IsChecked = false;
                for (int i = 0; i < Matrices.Count; i++)
                {
                    Matrices[i].IsChecked = (index == i);
                    UpdateWeapon(i);
                }
            }
            else
                UpdateWeapon(index);
        }

        private void UpdateWeapon(int index)
        {
            Ranges[index].Text = Weapons[index].DisplayRange;
            Ballistics[index].Text = "+" + (0 + (Weapons[index].TurboWeapon == Quality.Best ? 5 : 0) + ((Matrices[index].IsChecked ?? false) ? 5: 0));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void onClose(object sender, EventArgs e)
        {
            bool none = true;//if no targetting matrix
            if (AllCheck.IsChecked ?? false)
            {
                Starship.TargettingMatrix = Quality.Common;
                none = false;
            }
            for (int i = 0; i < Matrices.Count; i++)
            {
                if (!(AllCheck.IsChecked ?? false) && (Matrices[i].IsChecked ?? false))//IF not common quality, and is checked then poor quality for this one
                {
                    Weapons[i].TargettingMatrix = Quality.Poor;
                    Starship.TargettingMatrix = Quality.Poor;
                    Starship.Matrix = i;
                    none = false;
                }
                else
                    Weapons[i].TargettingMatrix = Quality.None;//remove matrix from any not ticked or if common
            }
            if(none)
                Starship.TargettingMatrix = Quality.None;
        }
    }
}
