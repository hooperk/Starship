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
    /// Interaction logic for Essential.xaml
    /// </summary>
    public partial class Essential : Window
    {
        public int ComponentCount { get; set; }
        Component Current;
        readonly Component Original;
        Type ComponentType;
        private static readonly Quality[] ExternalQualities = new Quality[] { Quality.Poor, Quality.Common, Quality.Good, Quality.Best };
        private static readonly Quality[] InternalQualities = new Quality[] { Quality.Poor, Quality.Common, Quality.Slim, Quality.Efficient, Quality.Best };

        public Essential(IEnumerable<Component> components, Type type, Component current = null)
        {
            this.ComponentType = type;
            ComponentCount = 0;
            this.Current = this.Original = current;
            InitializeComponent();
            if (type == typeof(Augur) || type == typeof(GellarField))
            {
                FillExternal(components);
            }
            else if (type == typeof(VoidShield))
            {
                FillShields(components.Cast<VoidShield>());
            }
            else
            {
                if (type == typeof(PlasmaDrive))
                {
                    ModifiedLabel.Visibility = System.Windows.Visibility.Visible;
                    Modified.Visibility = System.Windows.Visibility.Visible;
                }
                FillComponents(components);
            }
            UpdateChosen();
        }

        private void FillComponents(IEnumerable<Component> components)
        {
            ChosenQuality.ItemsSource = InternalQualities;
            Button button;
            TextBox textbox;
            Label label;
            TextBlock textblock;
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => SetChosen(null));
            Grid.SetRow(button, ComponentCount++);
            Grid.SetColumn(button, 0);
            Grid.SetColumnSpan(button, 4);
            Components.Children.Add(button);
            foreach (var group in components.GroupBy(x => x.ComponentOrigin).OrderBy(x => x.Key))
            {
                if (group.Key != ComponentOrigin.Standard)
                {
                    label = new Label();
                    label.Content = group.Key.ToString();
                    Grid.SetRow(label, ComponentCount++);
                    Grid.SetColumn(label, 0);
                    Components.Children.Add(label);
                }
                foreach (Component component in group)
                {
                    button = new Button();
                    button.Content = component.GetName();
                    button.Click += ((s, e) => SetChosen(component));
                    Grid.SetRow(button, ComponentCount);
                    Grid.SetColumn(button, 0);
                    Components.Children.Add(button);
                    textbox = new TextBox();
                    textbox.Text = component.Power.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 1);
                    Components.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = component.Space.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 2);
                    Components.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = component.SP.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 3);
                    Components.Children.Add(textbox);
                    textblock = new TextBlock();
                    textblock.Text = component.HullTypes.AllHulls();
                    textblock.Margin = new Thickness(2, 2, 2, 2);
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, ComponentCount);
                    Grid.SetColumn(textblock, 5);
                    Components.Children.Add(textblock);
                    label = new Label();
                    label.Content = component.Origin.Name();
                    label.ToolTip = component.Origin.LongName() + ", Page: " + component.PageNumber;
                    Grid.SetRow(label, ComponentCount++);
                    Grid.SetColumn(label, 6);
                    Components.Children.Add(label);
                    if (!String.IsNullOrWhiteSpace(component.Description))
                    {
                        textblock = new TextBlock();
                        textblock.Text = component.Description;
                        textblock.Margin = new Thickness(2, 2, 2, 2);
                        textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                        Grid.SetRow(textblock, ComponentCount++);
                        Grid.SetColumnSpan(textblock, 7);
                        Components.Children.Add(textblock);
                    }
                }
            }
        }

        private void FillExternal(IEnumerable<Component> components)
        {
            ChosenQuality.ItemsSource = ExternalQualities;
            TopSpace.Visibility = System.Windows.Visibility.Collapsed;
            BottomSpace.Visibility = System.Windows.Visibility.Collapsed;
            ChosenSpace.Visibility = System.Windows.Visibility.Collapsed;
            SpaceField.Width = new GridLength(0);
            Button button;
            TextBox textbox;
            Label label;
            TextBlock textblock;
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => SetChosen(null));
            Grid.SetRow(button, ComponentCount++);
            Grid.SetColumn(button, 0);
            Grid.SetColumnSpan(button, 4);
            Components.Children.Add(button);
            foreach (var group in components.GroupBy(x => x.ComponentOrigin).OrderBy(x => x.Key))
            {
                if (group.Key != ComponentOrigin.Standard)
                {
                    label = new Label();
                    label.Content = group.Key.ToString();
                    Grid.SetRow(label, ComponentCount++);
                    Grid.SetColumn(label, 0);
                    Components.Children.Add(label);
                }
                foreach (Component component in group)
                {
                    button = new Button();
                    button.Content = component.GetName();
                    button.Click += ((s, e) => SetChosen(component));
                    Grid.SetRow(button, ComponentCount);
                    Grid.SetColumn(button, 0);
                    Components.Children.Add(button);
                    textbox = new TextBox();
                    textbox.Text = component.Power.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 1);
                    Components.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox = new TextBox();
                    textbox.Text = component.SP.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 3);
                    Components.Children.Add(textbox);
                    textblock = new TextBlock();
                    textblock.Text = component.HullTypes.AllHulls();
                    textblock.Margin = new Thickness(2, 2, 2, 2);
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, ComponentCount);
                    Grid.SetColumn(textblock, 5);
                    Components.Children.Add(textblock);
                    label = new Label();
                    label.Content = component.Origin.Name();
                    label.ToolTip = component.Origin.LongName() + ", Page: " + component.PageNumber;
                    Grid.SetRow(label, ComponentCount++);
                    Grid.SetColumn(label, 6);
                    Components.Children.Add(label);
                    if (!String.IsNullOrWhiteSpace(component.Description))
                    {
                        textblock = new TextBlock();
                        textblock.Text = component.Description;
                        textblock.Margin = new Thickness(2, 2, 2, 2);
                        textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                        Grid.SetRow(textblock, ComponentCount++);
                        Grid.SetColumnSpan(textblock, 7);
                        Components.Children.Add(textblock);
                    }
                }
            }
        }

        private void FillShields(IEnumerable<VoidShield> components)
        {
            ChosenQuality.ItemsSource = InternalQualities;
            TopShields.Visibility = System.Windows.Visibility.Visible;
            BottomShields.Visibility = Visibility.Visible;
            ChosenShields.Visibility = System.Windows.Visibility.Visible;
            ShieldField.Width = new GridLength(47);
            Button button;
            TextBox textbox;
            Label label;
            TextBlock textblock;
            button = new Button();
            button.Content = "Clear";
            button.Click += ((s, e) => SetChosen(null));
            Grid.SetRow(button, ComponentCount++);
            Grid.SetColumn(button, 0);
            Grid.SetColumnSpan(button, 4);
            Components.Children.Add(button);
            foreach (var group in components.GroupBy(x => x.ComponentOrigin).OrderBy(x => x.Key))
            {
                if (group.Key != ComponentOrigin.Standard)
                {
                    label = new Label();
                    label.Content = group.Key.ToString();
                    Grid.SetRow(label, ComponentCount++);
                    Grid.SetColumn(label, 0);
                    Components.Children.Add(label);
                }
                foreach (VoidShield component in group)
                {
                    button = new Button();
                    button.Content = component.GetName();
                    button.Click += ((s, e) => SetChosen(component));
                    Grid.SetRow(button, ComponentCount);
                    Grid.SetColumn(button, 0);
                    Components.Children.Add(button);
                    textbox = new TextBox();
                    textbox.Text = component.Power.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 1);
                    Components.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = component.Space.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 2);
                    Components.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = component.SP.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 3);
                    Components.Children.Add(textbox);
                    textbox = new TextBox();
                    textbox.Text = component.Strength.ToString();
                    textbox.TextAlignment = TextAlignment.Center;
                    textbox.IsReadOnly = true;
                    Grid.SetRow(textbox, ComponentCount);
                    Grid.SetColumn(textbox, 4);
                    Components.Children.Add(textbox);
                    textblock = new TextBlock();
                    textblock.Text = component.HullTypes.AllHulls();
                    textblock.Margin = new Thickness(2, 2, 2, 2);
                    textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                    Grid.SetRow(textblock, ComponentCount);
                    Grid.SetColumn(textblock, 5);
                    Components.Children.Add(textblock);
                    label = new Label();
                    label.Content = component.Origin.Name();
                    label.ToolTip = component.Origin.LongName() + ", Page: " + component.PageNumber;
                    Grid.SetRow(label, ComponentCount++);
                    Grid.SetColumn(label, 6);
                    Components.Children.Add(label);
                    if (!String.IsNullOrWhiteSpace(component.Description))
                    {
                        textblock = new TextBlock();
                        textblock.Text = component.Description;
                        textblock.Margin = new Thickness(2, 2, 2, 2);
                        textblock.TextWrapping = TextWrapping.WrapWithOverflow;
                        Grid.SetRow(textblock, ComponentCount++);
                        Grid.SetColumnSpan(textblock, 7);
                        Components.Children.Add(textblock);
                    }
                }
            }
        }

        private void SetChosen(Component chosen)
        {
            Modified.IsChecked = false;
            UpdateCurrent(chosen, chosen.Quality);//always unmodified when you first add it
            UpdateChosen();
        }

        private void UpdateChosen()
        {
            if (Current != null)
            {
                ChosenName.Text = Current.Name;
                ChosenPower.Text = Current.Power.ToString();
                ChosenSpace.Text = Current.Space.ToString();
                ChosenSP.Text = Current.SP.ToString();
                if (ComponentType == typeof(VoidShield))
                    ChosenShields.Text = ((VoidShield)Current).Strength.ToString();
                ChosenQuality.SelectedItem = Current.Quality;
                if (ComponentType == typeof(PlasmaDrive))
                {
                    Modified.IsChecked = ((PlasmaDrive)Current).Modified;
                    bool modifiable = ((PlasmaDrive)Current).RawCompOrigin == ComponentOrigin.Standard;//can't modify an already archeotech or xenotech drive
                    Modified.IsEnabled = modifiable;
                    Modified.ToolTip = (modifiable ? null : "Cannot modify an archeotech or xenotech drive");
                }
                ChosenSpecial.Text = Current.Description;
            }
            else
            {
                ChosenName.Text = "";
                ChosenPower.Text = "";
                ChosenSpace.Text = "";
                ChosenSP.Text = "";
                ChosenShields.Text = "";
                ChosenQuality.SelectedItem = "";
                Modified.IsChecked = false;
                ChosenSpecial.Text = "";
            }

        }

        private void ChosenQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCurrent(Current, (Quality)ChosenQuality.SelectedItem);
        }

        private void UpdateCurrent(Component next, Quality quality)
        {
            if (ComponentType == typeof(PlasmaDrive))
            {
                PlasmaDrive original = next as PlasmaDrive;
                if (original != null)
                    Current = new PlasmaDrive(original.GetName(), original.HullTypes, original.RawPower, original.RawSpace, original.RawSpecial, original.Origin, original.PageNumber, original.RawSP,
                        quality, original.RawSpeed, original.Manoeuvrability, original.RawCompOrigin, Modified.IsChecked ?? false, original.Condition);
            }
            else if (ComponentType == typeof(WarpDrive))
            {
                WarpDrive original = next as WarpDrive;
                if (original != null)
                    Current = new WarpDrive(original.GetName(), original.HullTypes, original.RawPower, original.RawSpace, original.Origin, original.PageNumber, original.RawSP, original.RawSpecial,
                        quality, original.ComponentOrigin, original.Condition);
            }
            else if (ComponentType == typeof(GellarField))
            {
                GellarField original = next as GellarField;
                if (original != null)
                    Current = new GellarField(original.GetName(), original.HullTypes, original.RawPower, original.RawSpecial, original.Origin, original.PageNumber, original.RawSP, original.NavigateWarp,
                        quality, original.ComponentOrigin, original.Condition);
            }
            else if (ComponentType == typeof(VoidShield))
            {
                VoidShield original = next as VoidShield;
                if (original != null)
                    Current = new VoidShield(original.GetName(), original.HullTypes, original.RawPower, original.RawSpace, original.Strength, original.Origin, original.PageNumber, original.RawSpecial,
                        quality, original.RawSP, original.ComponentOrigin, original.Condition);
            }
            else if (ComponentType == typeof(Bridge))
            {
                Bridge original = next as Bridge;
                if (original != null)
                    Current = new Bridge(original.GetName(), original.HullTypes, original.RawPower, original.RawSpace, original.Origin, original.PageNumber, original.RawSpecial, original.RawSP, quality,
                        original.Manoeuvrability, original.BSModifier, original.Command, original.Repair, original.Pilot, original.NavigateWarp, original.ComponentOrigin, original.MiningObjective, original.CreedObjective,
                        original.MilitaryObjective, original.TradeObjective, original.CriminalObjective, original.ExplorationObjective, original.Condition);
            }
            else if (ComponentType == typeof(LifeSustainer))
            {
                LifeSustainer original = next as LifeSustainer;
                if (original != null)
                    Current = new LifeSustainer(original.GetName(), original.HullTypes, original.RawPower, original.RawSpace, ((LifeSustainer)original).Morale, original.Origin, original.PageNumber, original.RawSpecial,
                        quality, original.RawSP, ((LifeSustainer)original).MoraleLoss, ((LifeSustainer)original).CrewLoss, original.ComponentOrigin, original.Condition);
            }
            else if (ComponentType == typeof(CrewQuarters))
            {
                CrewQuarters original = next as CrewQuarters;
                if (original != null)
                    Current = new CrewQuarters(original.GetName(), original.HullTypes, original.RawPower, original.RawSpace, ((CrewQuarters)original).Morale, original.Origin, original.PageNumber, original.RawSpecial,
                        quality, original.RawSP, ((CrewQuarters)original).MoraleLoss, original.ComponentOrigin, original.Condition);
            }
            else if (ComponentType == typeof(Augur))
            {
                Augur original = next as Augur;
                if (original != null)
                    Current = new Augur(original.GetName(), original.RawPower, original.Origin, original.PageNumber, original.DetectionRating, original.RawSpecial, quality, original.RawSP, original.Manoeuvrability,
                        original.BSModifier, original.MiningObjective, original.CreedObjective, original.MilitaryObjective, original.TradeObjective, original.CriminalObjective, original.ExplorationObjective, original.ComponentOrigin, original.Condition);
            }
            UpdateChosen();
        }

        private void Modified_Toggled(object sender, RoutedEventArgs e)
        {
            if (Current != null)
            {
                UpdateCurrent(Current, (Quality)ChosenQuality.SelectedItem);
                UpdateChosen();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        public new Component ShowDialog()
        {
            bool? save = base.ShowDialog();
            if (save ?? false)
                return Current;
            return Original;
        }
    }
}
