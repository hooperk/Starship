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
    /// Interaction logic for HullChooser.xaml
    /// </summary>
    public partial class HullChooser : Window
    {
        Starship Starship;
        public int HullCount { get; set; }

        public HullChooser(Starship starship, IEnumerable<Hull> hulls)
        {
            if(starship == null)
                throw new ArgumentNullException("Cannot initialise Hull Chooser with a null ship");
            HullCount = 1;
            Starship = starship;
            InitializeComponent();
            ClearButton.Click += ((s, e) => SetHull(null));
            foreach (Hull hull in hulls)
            {
                Hulls.Children.Add(MakeName(hull));
                Hulls.Children.Add(MakeColumn(hull.Speed, 1));
                Hulls.Children.Add(MakeColumn(hull.Manoeuvrability, 2));
                Hulls.Children.Add(MakeColumn(hull.DetectionRating, 3));
                Hulls.Children.Add(MakeColumn(hull.HullIntegrity, 4));
                Hulls.Children.Add(MakeColumn(hull.Armour, 5));
                Hulls.Children.Add(MakeColumn(hull.TurretRating, 6));
                Hulls.Children.Add(MakeSlots(hull));
                String description = hull.Description;
                if (!String.IsNullOrWhiteSpace(description))
                    Hulls.Children.Add(MakeDecription(description));
            }
            UpdateHull();
        }

        private Button MakeName(Hull hull)
        {
            Button button;
            button = new Button();
            button.Content = hull.Name;
            button.Click += ((s, e) => SetHull(hull));
            Grid.SetRow(button, HullCount);
            Grid.SetColumn(button, 0);
            return button;
        }

        private TextBox MakeColumn(int value, int column)
        {
            TextBox textbox = new TextBox();
            textbox.Text = value.ToString();
            textbox.TextAlignment = TextAlignment.Center;
            textbox.IsReadOnly = true;
            Grid.SetRow(textbox, HullCount);
            Grid.SetColumn(textbox, column);
            return textbox;
        }

        private TextBox MakeSlots(Hull hull)
        {
            TextBox textbox = new TextBox();
            textbox.Text = hull.ProwSlots + "/" + hull.DorsalSlots + "/" + hull.SideSlots + "/" + hull.KeelSlots + "/" + hull.AftSlots;
            textbox.TextAlignment = TextAlignment.Center;
            textbox.IsReadOnly = true;
            Grid.SetRow(textbox, HullCount++);
            Grid.SetColumn(textbox, 7);
            return textbox;
        }

        private TextBlock MakeDecription(String description)
        {
            TextBlock textblock = new TextBlock();
            textblock.Text = description;
            textblock.TextWrapping = TextWrapping.WrapWithOverflow;
            textblock.Margin = new Thickness(5, 5, 5, 5);
            Grid.SetRow(textblock, HullCount++);
            Grid.SetColumnSpan(textblock, 8);
            return textblock;
        }

        private void SetHull(Hull hull)
        {
            Starship.Hull = hull;
            UpdateHull();
        }

        private void UpdateHull()
        {
            if (Starship.Hull != null)
            {
                HullName.Text = Starship.Hull.Name;
                HullSpeed.Text = Starship.Hull.Speed.ToString();
                HullMan.Text = Starship.Hull.Manoeuvrability.ToString();
                HullDet.Text = Starship.Hull.DetectionRating.ToString();
                HullInt.Text = Starship.Hull.HullIntegrity.ToString();
                HullArmour.Text = Starship.Hull.Armour.ToString();
                HullTurret.Text = Starship.Hull.TurretRating.ToString();
                HullSlots.Text = Starship.Hull.ProwSlots + "/" + Starship.Hull.DorsalSlots + "/" + Starship.Hull.SideSlots + "/" + Starship.Hull.KeelSlots + "/" + Starship.Hull.AftSlots;
                HullDesc.Text = Starship.Hull.Description;
            }
            else
            {
                HullName.Text = "No Hull Chosen";
                HullSpeed.Text = "";
                HullMan.Text = "";
                HullDet.Text = "";
                HullInt.Text = "";
                HullArmour.Text = "";
                HullTurret.Text = "";
                HullSlots.Text = "";
                HullDesc.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
