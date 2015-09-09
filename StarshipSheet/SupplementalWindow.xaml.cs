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
    /// Interaction logic for SupplementalWindow.xaml
    /// </summary>
    public partial class SupplementalWindow : Window
    {
        public int ComponentCount { get; set; }
        Starship Starship;

        public SupplementalWindow(Starship starship, Loader loader)
        {
            if (starship == null || starship.Hull == null)
                throw new ArgumentNullException("Cannot choose supplementals without a hull");
            if (loader == null)
                throw new ArgumentNullException("Cannot load supplementals without a loader");
            this.Starship = starship;
            ComponentCount = 0;
            InitializeComponent();
            Label label;
            Button button;
            TextBox textbox;
            TextBlock textblock;
            foreach (var generated in loader.Supplementals.Where(x => (x.HullTypes & Starship.Hull.HullTypes) != 0).GroupBy(x => x.PowerGenerated))
            {
                foreach (var group in generated.GroupBy(x => x.ComponentOrigin))
                {
                    if (group.Count() > 0)
                    {
                        if (group.Key != ComponentOrigin.Standard)
                        {
                            label = new Label();
                            label.Content = group.Key.ToString();
                            Grid.SetRow(label, ComponentCount);
                            Grid.SetColumn(label, 0);
                            ComponentGrid.Children.Add(label);
                        }
                        label = new Label();
                        label.Content = generated.Key ? "Generated" : "Used";
                        label.HorizontalContentAlignment = HorizontalAlignment.Center;
                        Grid.SetRow(label, ComponentCount++);
                        Grid.SetColumn(label, 2);
                        ComponentGrid.Children.Add(label);
                    }
                    foreach (Supplemental baseComponent in group)
                    {
                        List<Supplemental> qualitycomponents = new List<Supplemental>();
                        if (baseComponent.RawSP > 1)//if component can have a lower cost, include poor quality
                            qualitycomponents.Add(new Supplemental(baseComponent.Name, baseComponent.HullTypes, baseComponent.RawPower, baseComponent.RawSpace, baseComponent.RawSP, baseComponent.Origin, baseComponent.PageNumber,
                                baseComponent.RamDamage, baseComponent.RawSpecial, Quality.Poor, baseComponent.Speed, baseComponent.Manoeuvrability, baseComponent.HullIntegrity, baseComponent.Armour, baseComponent.TurretRating,
                                baseComponent.Morale, baseComponent.CrewPopulation, baseComponent.ProwArmour, baseComponent.CrewRating, baseComponent.MiningObjective, baseComponent.CreedObjective, baseComponent.MilitaryObjective,
                                baseComponent.TradeObjective, baseComponent.CriminalObjective, baseComponent.ExplorationObjective, baseComponent.PowerGenerated, baseComponent.DetectionRating, baseComponent.AuxiliaryWeapon,
                                baseComponent.MacrobatteryModifier, baseComponent.BSModifier, baseComponent.NavigateWarp, baseComponent.CrewLoss, baseComponent.MoraleLoss, baseComponent.ComponentOrigin, baseComponent.Replace, baseComponent.Max,
                                StarshipGenerator.Utils.Condition.Intact));
                        //always add common quality
                        qualitycomponents.Add(new Supplemental(baseComponent.Name, baseComponent.HullTypes, baseComponent.RawPower, baseComponent.RawSpace, baseComponent.RawSP, baseComponent.Origin, baseComponent.PageNumber,
                                baseComponent.RamDamage, baseComponent.RawSpecial, Quality.Common, baseComponent.Speed, baseComponent.Manoeuvrability, baseComponent.HullIntegrity, baseComponent.Armour, baseComponent.TurretRating,
                                baseComponent.Morale, baseComponent.CrewPopulation, baseComponent.ProwArmour, baseComponent.CrewRating, baseComponent.MiningObjective, baseComponent.CreedObjective, baseComponent.MilitaryObjective,
                                baseComponent.TradeObjective, baseComponent.CriminalObjective, baseComponent.ExplorationObjective, baseComponent.PowerGenerated, baseComponent.DetectionRating, baseComponent.AuxiliaryWeapon,
                                baseComponent.MacrobatteryModifier, baseComponent.BSModifier, baseComponent.NavigateWarp, baseComponent.CrewLoss, baseComponent.MoraleLoss, baseComponent.ComponentOrigin, baseComponent.Replace, baseComponent.Max,
                                StarshipGenerator.Utils.Condition.Intact));
                        if ((baseComponent.RawPower > 1) ^ (baseComponent.RawSpace > 1))//if only one possible upgrade for good quality, do default good
                        {
                            qualitycomponents.Add(new Supplemental(baseComponent.Name, baseComponent.HullTypes, baseComponent.RawPower, baseComponent.RawSpace, baseComponent.RawSP, baseComponent.Origin, baseComponent.PageNumber,
                                baseComponent.RamDamage, baseComponent.RawSpecial, Quality.Good, baseComponent.Speed, baseComponent.Manoeuvrability, baseComponent.HullIntegrity, baseComponent.Armour, baseComponent.TurretRating,
                                baseComponent.Morale, baseComponent.CrewPopulation, baseComponent.ProwArmour, baseComponent.CrewRating, baseComponent.MiningObjective, baseComponent.CreedObjective, baseComponent.MilitaryObjective,
                                baseComponent.TradeObjective, baseComponent.CriminalObjective, baseComponent.ExplorationObjective, baseComponent.PowerGenerated, baseComponent.DetectionRating, baseComponent.AuxiliaryWeapon,
                                baseComponent.MacrobatteryModifier, baseComponent.BSModifier, baseComponent.NavigateWarp, baseComponent.CrewLoss, baseComponent.MoraleLoss, baseComponent.ComponentOrigin, baseComponent.Replace, baseComponent.Max,
                                StarshipGenerator.Utils.Condition.Intact));
                        }
                        else if (baseComponent.RawPower > 1 && baseComponent.RawSpace > 1)//If both can be added then can have efficient, slim or best(both) quality
                        {
                            qualitycomponents.Add(new Supplemental(baseComponent.Name, baseComponent.HullTypes, baseComponent.RawPower, baseComponent.RawSpace, baseComponent.RawSP, baseComponent.Origin, baseComponent.PageNumber,
                                baseComponent.RamDamage, baseComponent.RawSpecial, Quality.Slim, baseComponent.Speed, baseComponent.Manoeuvrability, baseComponent.HullIntegrity, baseComponent.Armour, baseComponent.TurretRating,
                                baseComponent.Morale, baseComponent.CrewPopulation, baseComponent.ProwArmour, baseComponent.CrewRating, baseComponent.MiningObjective, baseComponent.CreedObjective, baseComponent.MilitaryObjective,
                                baseComponent.TradeObjective, baseComponent.CriminalObjective, baseComponent.ExplorationObjective, baseComponent.PowerGenerated, baseComponent.DetectionRating, baseComponent.AuxiliaryWeapon,
                                baseComponent.MacrobatteryModifier, baseComponent.BSModifier, baseComponent.NavigateWarp, baseComponent.CrewLoss, baseComponent.MoraleLoss, baseComponent.ComponentOrigin, baseComponent.Replace, baseComponent.Max,
                                StarshipGenerator.Utils.Condition.Intact));
                            qualitycomponents.Add(new Supplemental(baseComponent.Name, baseComponent.HullTypes, baseComponent.RawPower, baseComponent.RawSpace, baseComponent.RawSP, baseComponent.Origin, baseComponent.PageNumber,
                                baseComponent.RamDamage, baseComponent.RawSpecial, Quality.Efficient, baseComponent.Speed, baseComponent.Manoeuvrability, baseComponent.HullIntegrity, baseComponent.Armour, baseComponent.TurretRating,
                                baseComponent.Morale, baseComponent.CrewPopulation, baseComponent.ProwArmour, baseComponent.CrewRating, baseComponent.MiningObjective, baseComponent.CreedObjective, baseComponent.MilitaryObjective,
                                baseComponent.TradeObjective, baseComponent.CriminalObjective, baseComponent.ExplorationObjective, baseComponent.PowerGenerated, baseComponent.DetectionRating, baseComponent.AuxiliaryWeapon,
                                baseComponent.MacrobatteryModifier, baseComponent.BSModifier, baseComponent.NavigateWarp, baseComponent.CrewLoss, baseComponent.MoraleLoss, baseComponent.ComponentOrigin, baseComponent.Replace, baseComponent.Max,
                                StarshipGenerator.Utils.Condition.Intact));
                            qualitycomponents.Add(new Supplemental(baseComponent.Name, baseComponent.HullTypes, baseComponent.RawPower, baseComponent.RawSpace, baseComponent.RawSP, baseComponent.Origin, baseComponent.PageNumber,
                                baseComponent.RamDamage, baseComponent.RawSpecial, Quality.Best, baseComponent.Speed, baseComponent.Manoeuvrability, baseComponent.HullIntegrity, baseComponent.Armour, baseComponent.TurretRating,
                                baseComponent.Morale, baseComponent.CrewPopulation, baseComponent.ProwArmour, baseComponent.CrewRating, baseComponent.MiningObjective, baseComponent.CreedObjective, baseComponent.MilitaryObjective,
                                baseComponent.TradeObjective, baseComponent.CriminalObjective, baseComponent.ExplorationObjective, baseComponent.PowerGenerated, baseComponent.DetectionRating, baseComponent.AuxiliaryWeapon,
                                baseComponent.MacrobatteryModifier, baseComponent.BSModifier, baseComponent.NavigateWarp, baseComponent.CrewLoss, baseComponent.MoraleLoss, baseComponent.ComponentOrigin, baseComponent.Replace, baseComponent.Max,
                                StarshipGenerator.Utils.Condition.Intact));
                        }
                        foreach (Supplemental component in qualitycomponents.Where(x => Starship.SupplementalComponents.Count(y => x.QualityName == y.QualityName && x.Origin == y.Origin) == 0 && (x.Max == 0 || Starship.SupplementalComponents.Count(y => x.Name == y.Name) < x.Max)))
                        {
                            label = new Label();
                            String name = component.QualityName;
                            if (component.Max == 1)
                            {
                                name += "†";
                                label.ToolTip = "Maximum of one " + component.Name;
                            }
                            label.Content = name;
                            Grid.SetRow(label, ComponentCount);
                            Grid.SetColumn(label, 0);
                            ComponentGrid.Children.Add(label);
                            Label countLabel = new Label();
                            countLabel.Content = 0;
                            countLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                            Grid.SetRow(countLabel, ComponentCount);
                            Grid.SetColumn(countLabel, 1);
                            ComponentGrid.Children.Add(countLabel);
                            label = new Label();
                            label.Content = component.Power;
                            label.HorizontalContentAlignment = HorizontalAlignment.Center;
                            Grid.SetRow(label, ComponentCount);
                            Grid.SetColumn(label, 2);
                            ComponentGrid.Children.Add(label);
                            label = new Label();
                            label.Content = component.Space;
                            label.HorizontalContentAlignment = HorizontalAlignment.Center;
                            Grid.SetRow(label, ComponentCount);
                            Grid.SetColumn(label, 3);
                            ComponentGrid.Children.Add(label);
                            label = new Label();
                            label.Content = component.SP;
                            label.HorizontalContentAlignment = HorizontalAlignment.Center;
                            Grid.SetRow(label, ComponentCount);
                            Grid.SetColumn(label, 4);
                            ComponentGrid.Children.Add(label);
                            button = new Button();
                            button.Content = "+";
                            button.Click += ((s, e) => AddComponent(countLabel, component));
                            Grid.SetRow(button, ComponentCount);
                            Grid.SetColumn(button, 5);
                            ComponentGrid.Children.Add(button);
                            button = new Button();
                            button.Content = "-";
                            button.Click += ((s, e) => RemoveComponent(countLabel, component));
                            Grid.SetRow(button, ComponentCount);
                            Grid.SetColumn(button, 6);
                            ComponentGrid.Children.Add(button);
                            label = new Label();
                            textbox = new TextBox();
                            textbox.Text = component.Origin.Name();
                            textbox.IsReadOnly = true;
                            textbox.ToolTip = component.Origin.LongName() + ", Page: " + component.PageNumber;
                            Grid.SetRow(textbox, ComponentCount++);
                            Grid.SetColumn(textbox, 7);
                            ComponentGrid.Children.Add(textbox);
                            textblock = new TextBlock();
                            textblock.Text = component.Description;
                            textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                            Grid.SetRow(textblock, ComponentCount++);
                            Grid.SetColumnSpan(textblock, 8);
                            ComponentGrid.Children.Add(textblock);
                        }
                    }
                }
            }
        }

        private void AddComponent(Label countLabel, Supplemental component)
        {
            int count = Starship.SupplementalComponents.Count(x => x.Name.Equals(component.Name) && x.Origin == component.Origin);
            if (component.Max == 0 || count < component.Max)
            {
                Starship.SupplementalComponents.Add(component);
                countLabel.Content = Starship.SupplementalComponents.Count(x => x.QualityName.Equals(component.QualityName) && x.Origin == component.Origin).ToString();
            }
        }

        private void RemoveComponent(Label countLabel, Supplemental component)
        {
            int count = Starship.SupplementalComponents.Count(x => x.Name.Equals(component.Name) && x.Origin == component.Origin);
            Starship.SupplementalComponents.Remove(component);
            countLabel.Content = Starship.SupplementalComponents.Count(x => x.QualityName.Equals(component.QualityName) && x.Origin == component.Origin).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
