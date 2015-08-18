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
using Machine = StarshipGenerator.Utils.MachineSpirit;
using History = StarshipGenerator.Utils.ShipHistory;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for Complication.xaml
    /// </summary>
    public partial class Complication : Window
    {
        bool MachineOrHistory;//Machine = true, History = false
        Starship Starship;

        public static Complication MachineSpirit(Starship starship)
        {
            return new Complication(starship, true);
        }

        public static Complication ShipHistory(Starship starship)
        {
            return new Complication(starship, false);
        }

        private Complication(Starship starship, bool MorH)
        {
            if (starship == null)
                throw new ArgumentNullException("Cannot create a complication for a null starship");
            this.Starship = starship;
            this.MachineOrHistory = MorH;
            InitializeComponent();
            if (MachineOrHistory)
            {
                TitleName.Content = "Machine Spirit Complication";
                Name1.Text = ((Machine)1).Name();
                Effect1.Text = ((Machine)1).Description();
                Name2.Text = ((Machine)2).Name();
                Effect2.Text = ((Machine)2).Description();
                Name3.Text = ((Machine)3).Name();
                Effect3.Text = ((Machine)3).Description();
                Name4.Text = ((Machine)4).Name();
                Effect4.Text = ((Machine)4).Description();
                Name5.Text = ((Machine)5).Name();
                Effect5.Text = ((Machine)5).Description();
                Name6.Text = ((Machine)6).Name();
                Effect6.Text = ((Machine)6).Description();
                Name7.Text = ((Machine)7).Name();
                Effect7.Text = ((Machine)7).Description();
                Name8.Text = ((Machine)8).Name();
                Effect8.Text = ((Machine)8).Description();
                Name9.Text = ((Machine)9).Name();
                Effect9.Text = ((Machine)9).Description();
                Name10.Text = ((Machine)10).Name();
                Effect10.Text = ((Machine)10).Description();
                Custom.Content = "Custom Machine Spirit";
                if (!String.IsNullOrWhiteSpace(Starship.GMMachineSpirit))
                {
                    String[] parts = Starship.GMMachineSpirit.Split(new char[]{':'},2);
                    CustomName.Text = parts[0].Trim();
                    CustomEffect.Text = parts[1].Trim();
                }
            }
            else
            {
                TitleName.Content = "Ship History Complication";
                Name1.Text = ((History)1).Name();
                Effect1.Text = ((History)1).Description();
                Name2.Text = ((History)2).Name();
                Effect2.Text = ((History)2).Description();
                Name3.Text = ((History)3).Name();
                Effect3.Text = ((History)3).Description();
                Name4.Text = ((History)4).Name();
                Effect4.Text = ((History)4).Description();
                Name5.Text = ((History)5).Name();
                Effect5.Text = ((History)5).Description();
                Name6.Text = ((History)6).Name();
                Effect6.Text = ((History)6).Description();
                Name7.Text = ((History)7).Name();
                Effect7.Text = ((History)7).Description();
                Name8.Text = ((History)8).Name();
                Effect8.Text = ((History)8).Description();
                Name9.Text = ((History)9).Name();
                Effect9.Text = ((History)9).Description();
                Name10.Text = ((History)10).Name();
                Effect10.Text = ((History)10).Description();
                Custom.Content = "Custom Ship History";
                if (!String.IsNullOrWhiteSpace(Starship.GMShipHistory))
                {
                    String[] parts = Starship.GMShipHistory.Split(new char[] { ':' }, 2);
                    CustomName.Text = parts[0].Trim();
                    CustomEffect.Text = parts[1].Trim();
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = Machine.None;
            else
                Starship.ShipHistory = History.None;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)1;
            else
                Starship.ShipHistory = (History)1;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)2;
            else
                Starship.ShipHistory = (History)2;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)3;
            else
                Starship.ShipHistory = (History)3;
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)4;
            else
                Starship.ShipHistory = (History)4;
        }

        private void RadioButton_Checked_5(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)5;
            else
                Starship.ShipHistory = (History)5;
        }

        private void RadioButton_Checked_6(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)6;
            else
                Starship.ShipHistory = (History)6;
        }

        private void RadioButton_Checked_7(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)7;
            else
                Starship.ShipHistory = (History)7;
        }

        private void RadioButton_Checked_8(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)8;
            else
                Starship.ShipHistory = (History)8;
        }

        private void RadioButton_Checked_9(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)9;
            else
                Starship.ShipHistory = (History)9;
        }

        private void RadioButton_Checked_10(object sender, RoutedEventArgs e)
        {
            if (MachineOrHistory)
                Starship.MachineSpirit = (Machine)10;
            else
                Starship.ShipHistory = (History)10;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (CustomChoice.IsChecked.HasValue)
            {
                if (CustomChoice.IsChecked.Value)
                {
                    if (String.IsNullOrWhiteSpace(CustomName.Text) || String.IsNullOrWhiteSpace(CustomEffect.Text))
                    {
                        MessageBox.Show("Custom " + (MachineOrHistory ? "Machine Spirit" : "Ship History") + " needs a name and effect.");
                        return;
                    }
                    if (MachineOrHistory)
                        Starship.GMMachineSpirit = CustomName.Text + ": " + CustomEffect.Text;
                    else
                        Starship.GMShipHistory = CustomName.Text + ": " + CustomEffect.Text;
                }
                else
                {
                    if (MachineOrHistory)
                        Starship.GMMachineSpirit = null;
                    else
                        Starship.GMShipHistory = null;
                }
            }
            this.Close();
        }
    }
}
