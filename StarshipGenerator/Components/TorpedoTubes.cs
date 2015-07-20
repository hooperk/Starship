using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Ammo;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Torpedo Tubes Weapons
    /// </summary>
    public class TorpedoTubes : Weapon
    {
        /// <summary>
        /// List of the ammo added
        /// </summary>
        public List<Torpedo> Ammo;
        /// <summary>
        /// Maximum ammo capacity
        /// </summary>
        public int Capacity { get; private set; }
        /// <summary>
        /// Array of what torpedoes are in the tubes.
        /// </summary>
        public Torpedo[] Tubes;
        /// <summary>
        /// Torpedoes don't do damage but as they're a weapon, 0 is listed
        /// </summary>
        public override DiceRoll Damage
        {
            get
            {
                return _damage;
            }
        }
        /// <summary>
        /// Torpedo tubes don't have crit ratings, torpedoes do
        /// </summary>
        public override int Crit
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Torpedo range is determined by torpedoes not the launchers
        /// </summary>
        public override int Range
        {
            get
            {
                return 0;
            }
        }

        public override string Special
        {
            get
            {
                if (!base.Special.Contains("Volatile"))
                    return "Volatile" + (String.IsNullOrEmpty(base.Special) ? "" : "; " + base.Special);
                return base.Special;
            }
        }

        /// <summary>
        /// Create a new Torpedo Tube
        /// </summary>
        /// <param name="name">name of the torpedo tube</param>
        /// <param name="hulls">class of ship that can mount this weapon</param>
        /// <param name="slots">locatiosn where this weapon can be mounted</param>
        /// <param name="power">power used by this weapon</param>
        /// <param name="space">space used by this method</param>
        /// <param name="sp">cost of this weapon</param>
        /// <param name="str">strength of the weapon</param>
        /// <param name="capacity">total ammo capacity of the torpedo tube</param>
        /// <param name="origin">rulebook containing this weapon</param>
        /// <param name="page">page this weapon can be found on</param>
        /// <param name="quality">quality of this weapon</param>
        /// <param name="wq">enum declaring which qualities to be adjusted</param>
        /// <param name="special">special rules of this weapon</param>
        public TorpedoTubes(string name, HullType hulls, int power, int space, int sp, int str,
            int capacity, RuleBook origin, byte page, Quality quality = Quality.Common, WeaponQuality wq = WeaponQuality.None, 
            string special = null, ComponentOrigin comp = ComponentOrigin.Standard)
            : base(name, WeaponType.TorpedoTube, hulls, WeaponSlot.Prow | WeaponSlot.Keel, power, space, sp, str, default(DiceRoll), 0, 0, origin, page, 
                quality, wq, special, Quality.None, comp) 
        {
            this.Capacity = capacity;
            Ammo = new List<Torpedo>(Capacity);
            Tubes = new Torpedo[Strength];
        }

        /// <summary>
        /// Serialises the Torpedo Tubes
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*{
             * "Torpedo" : {
             *  "Name" : name,
             *  "Types" : types,
             *  "Power" : power,
             *  "Space" : space,
             *  "SP" : sp,
             *  "Size" : size,
             *  "Origin" : origin,
             *  "Page" : page,
             *  "Quality" : quality,
             *  "WeapQual" : wq,
             *  "Special" : special,
             *  "Comp" : comp,
             *  "Ammo" : [Torpedoes],
             *  "Tubes" : [Tubes] }
             *}
             */
            StringBuilder output = new StringBuilder(
                @"{""Torpedo"":{""Name"":""" + Name.Escape() + @""",""Types"":" + (byte)HullTypes
                + @",""Power"":" + Power + @",""Space"":" + Space + @",""SP"":" + SP + @",""Size"":" + Capacity + @",""Origin"":"
                + (byte)Origin + @",""Page"":" + PageNumber + @",""Quality"":" + (byte)Quality + @",""WeapQual"":"
                + (byte)WeaponQuality + @",""Special"":""" + Special.Escape() + @""",""Comp"":" + (byte)ComponentOrigin
                + @",""Ammo"":[");
            if (Ammo != null)
            {
                for (int i = 0; i < Ammo.Count; i++)
                {
                    output.Append(Ammo[i].ToJSON());
                    if (i < Ammo.Count - 1)
                        output.Append(",");
                }
            }
            output.Append(@"],""Tubes"":[");
            if (Tubes != null)
            {
                for(int i = 0; i < Tubes.Length; i++){
                    output.Append((Tubes[i] == null ? "null" : Tubes[i].ToJSON()));
                    if (i < Tubes.Length - 1)
                        output.Append(",");
                }
            }
            output.Append(@"]}}");
            return output.ToString();
        }

        /// <summary>
        /// Description of the Augur Array to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                output.Append("Ammo capacity of " + Capacity + "; ");
                output.Append(base.Description);
                return output.ToString();
            }
        }

        /// <summary>
        /// Display no range for Torpedo Tubes
        /// </summary>
        public override string DisplayRange
        {
            get
            {
                return null;
            }
        }
    }
}
