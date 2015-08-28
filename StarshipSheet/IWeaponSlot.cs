using StarshipGenerator.Components;
using StarshipGenerator.Utils;

namespace StarshipSheet
{
    public interface IWeaponSlot
    {
        Weapon Weapon { get; set; }
        WeaponSlot WeaponFacing { get; set; }
        void UpdateWeapon();
    }
}
