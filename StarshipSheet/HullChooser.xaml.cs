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
                Hulls.Children.Add(MakeColumn(hull.SP, 1));
                Hulls.Children.Add(MakeColumn(hull.Space, 2));
                Hulls.Children.Add(MakeClass(hull));
                Hulls.Children.Add(MakeColumn(hull.Speed, 4));
                Hulls.Children.Add(MakeColumn(hull.Manoeuvrability, 5));
                Hulls.Children.Add(MakeColumn(hull.DetectionRating, 6));
                Hulls.Children.Add(MakeColumn(hull.HullIntegrity, 7));
                Hulls.Children.Add(MakeColumn(hull.Armour, 8));
                Hulls.Children.Add(MakeColumn(hull.TurretRating, 9));
                Hulls.Children.Add(MakeSlots(hull));
                Hulls.Children.Add(MakeSource(hull));
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

        private TextBox MakeClass(Hull hull)
        {
            TextBox textbox = new TextBox();
            textbox.Text = hull.HullTypes.HighName();
            textbox.TextAlignment = TextAlignment.Center;
            textbox.IsReadOnly = true;
            Grid.SetRow(textbox, HullCount);
            Grid.SetColumn(textbox, 3);
            return textbox;
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
            textbox.ToolTip = "Prow/Dorsal/Side/Keel/Aft";
            textbox.TextAlignment = TextAlignment.Center;
            textbox.IsReadOnly = true;
            Grid.SetRow(textbox, HullCount);
            Grid.SetColumn(textbox, 10);
            return textbox;
        }

        private TextBox MakeSource(Hull hull)
        {
            TextBox textbox = new TextBox();
            textbox.Text = hull.Origin.Name();
            textbox.ToolTip = hull.Origin.LongName() +", Page: " + hull.PageNumber;
            textbox.TextAlignment = TextAlignment.Center;
            textbox.IsReadOnly = true;
            Grid.SetRow(textbox, HullCount++);
            Grid.SetColumn(textbox, 11);
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
                HullSP.Text = Starship.Hull.SP.ToString();
                HullSpace.Text = Starship.Hull.Space.ToString();
                HullClass.Text = Starship.Hull.HullTypes.HighName();
                HullSpeed.Text = Starship.Hull.Speed.ToString();
                HullMan.Text = Starship.Hull.Manoeuvrability.ToString();
                HullDet.Text = Starship.Hull.DetectionRating.ToString();
                HullInt.Text = Starship.Hull.HullIntegrity.ToString();
                HullArmour.Text = Starship.Hull.Armour.ToString();
                HullTurret.Text = Starship.Hull.TurretRating.ToString();
                HullSlots.Text = Starship.Hull.ProwSlots + "/" + Starship.Hull.DorsalSlots + "/" + Starship.Hull.SideSlots + "/" + Starship.Hull.KeelSlots + "/" + Starship.Hull.AftSlots;
                HullSource.Text = Starship.Hull.Origin.Name();
                HullSource.ToolTip = Starship.Hull.Origin.LongName() + ", Page: " + Starship.Hull.PageNumber;
                HullDesc.Text = Starship.Hull.Description;
            }
            else
            {
                HullName.Text = "No Hull Chosen";
                HullSP.Text = "";
                HullSpace.Text = "";
                HullClass.Text = "";
                HullSpeed.Text = "";
                HullMan.Text = "";
                HullDet.Text = "";
                HullInt.Text = "";
                HullArmour.Text = "";
                HullTurret.Text = "";
                HullSlots.Text = "";
                HullSource.Text = "";
                HullSource.ToolTip = null;
                HullDesc.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
