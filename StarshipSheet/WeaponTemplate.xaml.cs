﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StarshipGenerator.Components;
using StarshipGenerator.Utils;

namespace StarshipSheet
{
    /// <summary>
    /// Interaction logic for Weapon.xaml
    /// </summary>
    public partial class WeaponTemplate : UserControl, IWeaponSlot
    {
        public Weapon Weapon
        {
            get
            {
                return _weapon;
            }
            set
            {
                _weapon = value;
                UpdateWeapon();
            }
        }
        private Weapon _weapon;
        public WeaponSlot WeaponFacing { get; set; }

        public WeaponTemplate(WeaponSlot facing, Weapon weapon = null)//Also pass in method for button - do after making said method
        {
            InitializeComponent();
            this.WeaponFacing = facing;
            Facing.Content = WeaponFacing.ToString();
            this.Weapon = weapon;
        }

        public void UpdateWeapon()
        {
            if (Weapon == null)
            {
                WeaponChoice.Content = "Weapon";
                WeaponType.Text = "";
                WeaponStrength.Text = "";
                WeaponDamage.Text = "";
                WeaponRange.Text = "";
                WeaponCrit.Text = "";
            }
            else
            {
                WeaponChoice.Content = Weapon.QualityName;
                WeaponType.Text = Weapon.Type.Name();
                WeaponStrength.Text = Weapon.Strength.ToString();
                WeaponDamage.Text = Weapon.Damage.ToString();
                WeaponRange.Text = Weapon.Range.ToString();
                WeaponCrit.Text = Weapon.Crit.ToString();
            }
        }
    }
}