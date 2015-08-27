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
    /// Interaction logic for WeaponQualityChooser.xaml
    /// </summary>
    public partial class WeaponQualityChooser : Window
    {
        Quality CurrentQuality;
        Quality OriginalQuality;
        WeaponQuality CurrentWQ;
        WeaponQuality OriginalWQ;
        WeaponType Type;
        public WeaponQualityChooser(WeaponType type, Quality last, WeaponQuality lastwq)
        {
            this.Type = type;
            this.CurrentQuality = this.OriginalQuality = last;
            this.CurrentWQ = this.OriginalWQ = lastwq;
            InitializeComponent();
            QualityChoice.ItemsSource = new List<Quality>() { Quality.Poor, Quality.Common, Quality.Good, Quality.Best };
            QualityChoice.SelectedItem = CurrentQuality;
        }

        private void QualityChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QualityChoice.SelectedItem != null)
                CurrentQuality = (Quality)QualityChoice.SelectedItem;
            //QualityChoice.Text = CurrentQuality + " Quality";
            if (CurrentQuality == Quality.Common)//If moving to Common (nothing set)
                SpaceCheck.IsChecked = StrengthCheck.IsChecked = DamageCheck.IsChecked = RangeCheck.IsChecked = CritCheck.IsChecked = false;//uncheck all for common
            else if (CurrentQuality == Quality.Good)//If Good(Less can be set than poor or best)
                StrengthCheck.IsChecked = CritCheck.IsChecked = false;//uncheck disallowed options for good quality
            if ((CurrentQuality == Quality.Poor || CurrentQuality == Quality.Best) && (Type == WeaponType.LandingBay || Type == WeaponType.TorpedoTube))
            {
                SpaceCheck.IsChecked = StrengthCheck.IsChecked = true;//Only two options available so auto check them
                SpaceCheck.IsEnabled = StrengthCheck.IsEnabled = DamageCheck.IsEnabled = RangeCheck.IsEnabled = CritCheck.IsEnabled = false;//disable all sinc there is only one valid permutation
                CurrentWQ = WeaponQuality.Space | WeaponQuality.Strength;
                SetValues();
            }
            else
                CheckEnables();
        }

        private void CheckChanged(object sender, RoutedEventArgs e)
        {
            CheckEnables();
        }

        private void CheckEnables()
        {
            int count = 0;
            CurrentWQ = WeaponQuality.None;
            if (SpaceCheck.IsChecked ?? false)
            {
                count++;
                CurrentWQ |= WeaponQuality.Space;
            }
            if (StrengthCheck.IsChecked ?? false) { 
                count++;
            CurrentWQ |= WeaponQuality.Strength;
            }
            if (DamageCheck.IsChecked ?? false)
            {
                count++;
                CurrentWQ |= WeaponQuality.Damage;
            }
            if (RangeCheck.IsChecked ?? false)
            {
                count++;
                CurrentWQ |= WeaponQuality.Range;
            }
            if (CritCheck.IsChecked ?? false)
            {
                count++;
                CurrentWQ |= WeaponQuality.Crit;
            }
            int allowed;
            if (CurrentQuality == Quality.Good)
                allowed = 1;
            else if (CurrentQuality == Quality.Common)
                allowed = 0;
            else
                allowed = 2;
            SpaceCheck.IsEnabled = ((SpaceCheck.IsChecked ?? true) || count < allowed);
            StrengthCheck.IsEnabled = (CurrentQuality != Quality.Good && ((StrengthCheck.IsChecked ?? true) || count < allowed));
            DamageCheck.IsEnabled = (Type != WeaponType.LandingBay && Type != WeaponType.TorpedoTube && ((DamageCheck.IsChecked ?? true) || count < allowed));//Landing bays and Torpedoes don't have Damage
            RangeCheck.IsEnabled = (Type != WeaponType.LandingBay && Type != WeaponType.TorpedoTube && ((RangeCheck.IsChecked ?? true) || count < allowed));//Landing bays and torpedoes don't have range
            CritCheck.IsEnabled = (CurrentQuality != Quality.Good && (Type == WeaponType.Macrobattery || Type == WeaponType.Lance) && ((CritCheck.IsChecked ?? true) || count < allowed));//Only Macrobatteries and Lances have crit
            SetValues();
        }

        private void SetValues()
        {
            switch (CurrentQuality)
            {
                case Quality.Poor:
                    SPValue.Text = "-1";
                    if (SpaceCheck.IsChecked ?? false)
                        SpaceValue.Text = "+1";
                    else
                        SpaceValue.Text = "+0";
                    if (StrengthCheck.IsChecked ?? false)
                        StrengthValue.Text = "-1";
                    else
                        StrengthValue.Text = "+0";
                    if (DamageCheck.IsChecked ?? false)
                        DamageValue.Text = "-1";
                    else
                        DamageValue.Text = "+0";
                    if (RangeCheck.IsChecked ?? false)
                        RangeValue.Text = "-1";
                    else
                        RangeValue.Text = "+0";
                    if (CritCheck.IsChecked ?? false)
                        CritValue.Text = "+1";
                    else
                        CritValue.Text = "+0";
                    break;
                case Quality.Common:
                    SPValue.Text = "+0";
                    SpaceValue.Text = "+0";
                    StrengthValue.Text = "+0";
                    DamageValue.Text = "+0";
                    RangeValue.Text = "+0";
                    CritValue.Text = "+0";
                    break;
                case Quality.Good:
                    SPValue.Text = "+1";
                    if (SpaceCheck.IsChecked ?? false)
                        SpaceValue.Text = "-1";
                    else
                        SpaceValue.Text = "+0";
                    StrengthValue.Text = "+0";
                    if (DamageCheck.IsChecked ?? false)
                        DamageValue.Text = "+1";
                    else
                        DamageValue.Text = "+0";
                    if (RangeCheck.IsChecked ?? false)
                        RangeValue.Text = "+1";
                    else
                        RangeValue.Text = "+0";
                    CritValue.Text = "+0";
                    break;
                case Quality.Best:
                    SPValue.Text = (2).PrintInt();
                    if (SpaceCheck.IsChecked ?? false)
                        SpaceValue.Text = "-1";
                    else
                        SpaceValue.Text = "+0";
                    if (StrengthCheck.IsChecked ?? false)
                        StrengthValue.Text = "+1";
                    else
                        StrengthValue.Text = "+0";
                    if (DamageCheck.IsChecked ?? false)
                        DamageValue.Text = "+1";
                    else
                        DamageValue.Text = "+0";
                    if (RangeCheck.IsChecked ?? false)
                        RangeValue.Text = "+1";
                    else
                        RangeValue.Text = "+0";
                    if (CritCheck.IsChecked ?? false)
                        CritValue.Text = "-1";
                    else
                        CritValue.Text = "+0";
                    break;
            }
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        public new Tuple<Quality,WeaponQuality> ShowDialog()
        {
            if (base.ShowDialog() ?? false)
                return new Tuple<Quality, WeaponQuality>(CurrentQuality, CurrentWQ);
            else return new Tuple<Quality, WeaponQuality>(OriginalQuality, OriginalWQ);
        }
    }
}
