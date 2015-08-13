using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
            NUDTextBox.Text = Value.ToString();
        }

        private void NUDButtonUP_Click(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void NUDButtonDown_Click(object sender, RoutedEventArgs e)
        {
            Value--;
        }

        private void NUDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { true });
            }
        }

        private void NUDTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { false });
        }

        private void NUDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox.Text != "")
            {
                if (!int.TryParse(NUDTextBox.Text, out number)) NUDTextBox.Text = Value.ToString();
                else
                {
                    Value = number;
                }
            }
            NUDTextBox.SelectionStart = NUDTextBox.Text.Length;

        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(int), typeof(NumericUpDown), new PropertyMetadata(100));

        public int Value
        {
            get
            {
                return (int)GetValue(ValueProperty);
            }
            set
            {
                if (value > Max)
                    SetValue(ValueProperty, Max);
                else if (value < Min)
                    SetValue(ValueProperty, Min);
                else
                    SetValue(ValueProperty, value);
                NUDTextBox.Text = Value.ToString();
            }
        }

        public int Min
        {
            get
            {
                return (int)GetValue(MinProperty);
            }
            set
            {
                if(value <= Max)//Don't allow min to be set above max
                    SetValue(MinProperty, value);
                if (Value < Min)
                    Value = Min;
            }
        }

        public int Max
        {
            get
            {
                return (int)GetValue(MaxProperty);
            }
            set
            {
                if(value >= Min)//Don't allow max be set below min
                    SetValue(MaxProperty, value);
                if (Value > Max)
                    Value = Max;
            }
        }
    }
}
