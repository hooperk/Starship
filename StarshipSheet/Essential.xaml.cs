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
            Current = chosen;
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
                    Modified.IsChecked = ((PlasmaDrive)Current).Modified;
                ChosenSpecial.Text = Current.Description;
                bool modifiable = Current.ComponentOrigin != ComponentOrigin.Archeotech;//can't modify an alreaddy archeotech drive
                Modified.IsEnabled = modifiable;
                Modified.ToolTip = (modifiable ? null : "Cannot modify an archeotech drive");
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
            if (ComponentType == typeof(PlasmaDrive))
            {
                UpdatePlasma();
            }
            else if (ComponentType == typeof(WarpDrive))
            {
                Current = new WarpDrive(Current.GetName(), Current.HullTypes, Current.RawPower, Current.RawSpace, Current.Origin, Current.PageNumber, Current.RawSP, Current.RawSpecial,
                    (Quality)ChosenQuality.SelectedItem, Current.ComponentOrigin, Current.Condition);
            }
            else if (ComponentType == typeof(GellarField))
            {
                Current = new GellarField(Current.GetName(), Current.HullTypes, Current.RawPower, Current.RawSpecial, Current.Origin, Current.PageNumber, Current.RawSP, ((GellarField)Current).NavigateWarp,
                    (Quality)ChosenQuality.SelectedItem, Current.ComponentOrigin, Current.Condition);
            }
            else if (ComponentType == typeof(VoidShield))
            {
                Current = new VoidShield(Current.GetName(), Current.HullTypes, Current.RawPower, Current.RawSpace, ((VoidShield)Current).Strength, Current.Origin, Current.PageNumber, Current.RawSpecial,
                    (Quality)ChosenQuality.SelectedItem, Current.RawSP, Current.ComponentOrigin, Current.Condition);
            }
            else if (ComponentType == typeof(Bridge))
            {
                Bridge past = (Bridge)Current;
                Current = new Bridge(past.GetName(), past.HullTypes, past.RawPower, past.RawSpace, past.Origin, past.PageNumber, past.RawSpecial, past.RawSP, (Quality)ChosenQuality.SelectedItem,
                    past.Manoeuvrability, past.BSModifier, past.Command, past.Repair, past.Pilot, past.NavigateWarp, past.ComponentOrigin, past.MiningObjective, past.CreedObjective,
                    past.MilitaryObjective, past.TradeObjective, past.CriminalObjective, past.ExplorationObjective, past.Condition);
            }
            else if (ComponentType == typeof(LifeSustainer))
            {
                Current = new LifeSustainer(Current.GetName(), Current.HullTypes, Current.RawPower, Current.RawSpace, ((LifeSustainer)Current).Morale, Current.Origin, Current.PageNumber, Current.RawSpecial,
                    (Quality)ChosenQuality.SelectedItem, Current.RawSP, ((LifeSustainer)Current).MoraleLoss, ((LifeSustainer)Current).CrewLoss, Current.ComponentOrigin, Current.Condition);
            }
            else if (ComponentType == typeof(CrewQuarters))
            {
                Current = new CrewQuarters(Current.GetName(), Current.HullTypes, Current.RawPower, Current.RawSpace, ((CrewQuarters)Current).Morale, Current.Origin, Current.PageNumber, Current.RawSpecial,
                    (Quality)ChosenQuality.SelectedItem, Current.RawSP, ((CrewQuarters)Current).MoraleLoss, Current.ComponentOrigin, Current.Condition);
            }
            else if (ComponentType == typeof(Augur))
            {
                Augur past = (Augur)Current;
                Current = new Augur(past.GetName(), past.RawPower, past.Origin, past.PageNumber, past.DetectionRating, past.RawSpecial, (Quality)ChosenQuality.SelectedItem, past.RawSP, past.Manoeuvrability,
                    past.BSModifier, past.MiningObjective, past.CreedObjective, past.MilitaryObjective, past.TradeObjective, past.CriminalObjective, past.ExplorationObjective, past.ComponentOrigin, past.Condition);
            }
            UpdateChosen();
        }

        private void Modified_Toggled(object sender, RoutedEventArgs e)
        {
            if (Current != null)
            {
                UpdatePlasma();
                UpdateChosen();
            }
        }

        private void UpdatePlasma()
        {
            Current = new PlasmaDrive(Current.GetName(), Current.HullTypes, Current.RawPower, Current.RawSpace, Current.RawSpecial, Current.Origin, Current.PageNumber, Current.RawSP,
                (Quality)ChosenQuality.SelectedItem, ((PlasmaDrive)Current).RawSpeed, ((PlasmaDrive)Current).Manoeuvrability, Current.ComponentOrigin, Modified.IsChecked ?? false, Current.Condition);
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
