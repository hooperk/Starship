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

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for CrewRating.xaml
    /// </summary>
    public partial class CrewRating : Window
    {
        Starship Starship;
        public int RaceCount { get; set; }

        public CrewRating(Starship starship)
        {
            if (starship == null)
                throw new ArgumentNullException("Cannot initialise Crew Rating with a null ship");
            Starship = starship;
            RaceCount = 0;
            InitializeComponent();
            foreach (Race race in Enum.GetValues(typeof(Race)))
            {
                Button button = new Button();
                button.Content = race.Name();
                button.Click += ((s, e) => SetRace(race));
                Grid.SetColumn(button,0);
                Grid.SetRow(button,RaceCount);
                TextBlock textblock = new TextBlock();
                textblock.Text = race.Description();
                textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                textblock.Margin = new Thickness(3, 1, 1, 1);
                Border border = new Border();
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(0, 1, 0, 1);
                border.Child = textblock;
                Grid.SetColumn(border, 1);
                Grid.SetRow(border, RaceCount++);
                Races.Children.Add(button);
                Races.Children.Add(border);
            }
            if (Starship.CrewRace == null)
                SetRace(Race.Human);
            else
                SetRace(Starship.CrewRace);
        }

        private void SetRace(Race race)
        {
            Starship.CrewRace = race;
            if (race == Race.Servitor)
            {
                Competency.ItemsSource = Enum.GetValues(typeof(ServitorQuality));
                if (Enum.IsDefined(typeof(ServitorQuality), Starship.CrewRating))
                    Competency.SelectedItem = (ServitorQuality)Starship.CrewRating;
                else
                    Competency.SelectedItem = ServitorQuality.Common;
            }
            else
            {
                Competency.ItemsSource = Enum.GetValues(typeof(StarshipGenerator.Utils.CrewRating));
                if (Enum.IsDefined(typeof(StarshipGenerator.Utils.CrewRating), Starship.CrewRating))
                    Competency.SelectedItem = (StarshipGenerator.Utils.CrewRating)Starship.CrewRating;
                else
                    Competency.SelectedItem = StarshipGenerator.Utils.CrewRating.Competent;
            }
        }

        private void Competency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Starship.CrewRating = (int)Competency.SelectedItem;
            Value.Text = Starship.CrewRating.ToString();
        }
    }
}
