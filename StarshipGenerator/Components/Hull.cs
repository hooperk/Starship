using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    public class Hull : Component
    {
        /// <summary>
        /// Base speed of the hull
        /// </summary>
        public int Speed { get; private set; }
        /// <summary>
        /// Base manoeuvrability of the hull
        /// </summary>
        public int Manoeuvrability { get; private set; }
        /// <summary>
        /// Base detection rating of the hull
        /// </summary>
        public int DetectionRating { get; private set; }
        /// <summary>
        /// Base integrity, or hit points, of the hull
        /// </summary>
        public int HullIntegrity { get; private set; }
        /// <summary>
        /// Base armour of the hull
        /// </summary>
        public int Armour { get; private set; }
        /// <summary>
        /// Base turret rating of the hull
        /// </summary>
        public int TurretRating { get; private set; }
        /// <summary>
        /// Number of prow(frontal) weapon slots
        /// </summary>
        public int ProwSlots { get; private set; }
        /// <summary>
        /// Number of dorsal(can shoot forward or sideways) weapon slots
        /// </summary>
        public int DorsalSlots { get; private set; }
        /// <summary>
        /// Number of weapon slots in each side
        /// </summary>
        /// <remarks>per side not combined</remarks>
        public int SideSlots { get; private set; }
        /// <summary>
        /// Number of Keel(can shoot everywhere) weapon slots
        /// </summary>
        public int KeelSlots { get; private set; }
        /// <summary>
        /// Number of Aft (rear) weapon slots
        /// </summary>
        public int AftSlots { get; private set; }
        /// <summary>
        /// Total number of weapon slots
        /// </summary>
        public int WeaponSlots
        {
            get
            {
                return ProwSlots + DorsalSlots + (SideSlots * 2) + KeelSlots + AftSlots;
            }
        }
        /// <summary>
        /// Default prow-mounted weapon
        /// </summary>
        public Weapon DefaultProw { get; private set; }
        /// <summary>
        /// Default weapon in each side slot
        /// </summary>
        public Weapon DefaultBroadside { get; private set; }
        /// <summary>
        /// Array of default components
        /// </summary>
        public Supplemental[] DefaultComponents { get; private set; }
        /// <summary>
        /// Modifier to Command skill which this bridge grants
        /// </summary>
        public int Command { get; private set; }
        /// <summary>
        /// Maximum speed, for instance Universe Class's plodding speed 2.
        /// 0 or less indicates no limit.
        /// </summary>
        public int MaxSpeed { get; private set; }
        /// <summary>
        /// Default ship history, mainly for Loki
        /// </summary>
        public ShipHistory History { get; private set; }
        /// <summary>
        /// Modifer to ballistic skill with this ship's weapons
        /// </summary>
        public int BSModifier { get; private set; }
        /// <summary>
        /// Whether the ship may be given armour upgrades
        /// </summary>
        public bool ArmourLocked { get; private set; }
        /// <summary>
        /// Modifiers to navigate warp with this ship
        /// </summary>
        public int NavigateWarp { get; private set; }
        /// <summary>
        /// Type of VoidShields which can be used by this hull
        /// </summary>
        public HullType VoidShields { get; private set; }
        //image or at least image path if gonna show

        /// <summary>
        /// Create a new hull
        /// </summary>
        /// <param name="name">name of the hull</param>
        /// <param name="speed">base speed of the hull</param>
        /// <param name="man">base manoeuvrability of hull</param>
        /// <param name="det">base detection rating of hull</param>
        /// <param name="hullint">base hull integrity of the hull</param>
        /// <param name="armour">base armour value of the hull</param>
        /// <param name="space">base available space in hull</param>
        /// <param name="sp">cost of the hull</param>
        /// <param name="type">which type(s) this hull can mount components for</param>
        /// <param name="special">special rules for this hull</param>
        /// <param name="origin">rulebook this hull can be found in</param>
        /// <param name="page">page number to find the hull on</param>
        /// <param name="turrets">turret rating of the hull</param>
        /// <param name="prow">number of prow weapon slots</param>
        /// <param name="dorsal">number of dorsal weapon slots</param>
        /// <param name="side">number of port weapon slots</param>
        /// <param name="keel">number of keel weapon slots</param>
        /// <param name="aft">number of aft weapon slots</param>
        /// <param name="frontal">Default prow weapon</param>
        /// <param name="broadside">Default broadside weapons</param>
        /// <param name="comps">Default supplemental components</param>
        /// <param name="command">Command modifier of this hull</param>
        /// <param name="maxspeed">maximum speed of ship. &lt;1 = unlimited</param>
        /// <param name="power">Power generated(or used)by hull, not including built in systems(those should be listed in themselves)</param>
        /// <param name="history">History this hull always has</param>
        /// <param name="bs">Modifier to ballistic skill tests with this hull</param>
        /// <param name="locked">If the ship has been locked so it may not add more armour</param>
        /// <param name="navigate">modifier to navigate warp</param>
        /// <param name="shields">hulltypes of the shields which may be added</param>
        public Hull(string name, int speed, int man, int det, int hullint, int armour, int space, int sp, HullType type,
            String special, RuleBook origin, byte page, int turrets = 1, int prow = 0, int dorsal = 0,
            int side = 0, int keel = 0, int aft = 0, Weapon frontal = null, Weapon broadside = null, 
            Supplemental[] comps = null, int command = 0, int maxspeed = 0, int power = 0, ShipHistory history = ShipHistory.None, 
            int bs = 0, bool locked = false, int navigate = 0, HullType shields = HullType.None)
            : base(name, sp, power, space, special, origin, page, type)
        {
            this.Speed = speed;
            this.Manoeuvrability = man;
            this.DetectionRating = det;
            this.HullIntegrity = hullint;
            this.Armour = armour;
            this.TurretRating = turrets;
            this.ProwSlots = prow;
            this.DorsalSlots = dorsal;
            this.SideSlots = side;
            this.KeelSlots = keel;
            this.AftSlots = aft;
            this.DefaultProw = frontal;
            this.DefaultBroadside = broadside;
            this.DefaultComponents = comps;
            this.Command = command;
            this.MaxSpeed = maxspeed;
            this.History = history;
            this.BSModifier = bs;
            this.ArmourLocked = locked;
            this.NavigateWarp = navigate;
            this.VoidShields = (shields == HullType.None ? type : shields);
        }

        /// <summary>
        /// Serialises the component
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*
             * {
             *  "Hull" : {
             *   "Name" : name,
             *   "Speed" : speed,
             *   "Man" : man,
             *   "Det" : det,
             *   "Int" : hullint,
             *   "Armour" : armour,
             *   "Space" : space,
             *   "SP" : sp,
             *   "Types" : types,
             *   "Special" : special,
             *   "Origin" : origin,
             *   "Page" : page,
             *   "Turrets" : turrets,
             *   "Prow" : prow,
             *   "Dorsal" : dorsal,
             *   "Side" : side,
             *   "Keel" : keel,
             *   "Aft" : aft,
             *   "Frontal" : {Weapon : {...}}
             *   "Broadside" : {Weapon : {...}}
             *   "Comps" : [{Supplemental : {...}},{Supplemental : {...}}...]
             *   "Command" : command,
             *   "Max" : maxspeed,
             *   "Power" : power,
             *   "History" : History,
             *   "BS" : bs,
             *   "Locked" : locked,
             *   "Nav" : nav,
             *   "Shields" : shields }
             * }
             */
            StringBuilder output = new StringBuilder(@"{""Hull"":{""Name"":""" + Name.Escape() + @""",""Speed"":" + Speed);
            output.Append(@",""Man"":" + Manoeuvrability + @",""Det"":" + DetectionRating + @",""Int"":" + HullIntegrity);
            output.Append(@",""Armour"":" + Armour + @",""Space"":" + RawSpace + @",""SP"":" + RawSP + @",""Types"":" + (byte)HullTypes);
            output.Append(@",""Special"":""" + RawSpecial.Escape() + @""",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber);
            output.Append(@",""Turrets"":" + TurretRating + @",""Prow"":" + ProwSlots + @",""Dorsal"":" + DorsalSlots);
            output.Append(@",""Side"":" + SideSlots + @",""Keel"":" + KeelSlots + @",""Aft"":" + AftSlots);
            output.Append(@",""Frontal"":" + DefaultProw.JSON());
            output.Append(@",""Broadside"":" + DefaultBroadside.JSON());
            output.Append(@",""Comps"":[");
            if (DefaultComponents != null)
            {
                for (int i = 0; i < DefaultComponents.Length; i++)
                {
                    output.Append(DefaultComponents[i].ToJSON());
                    if (i < DefaultComponents.Length - 1)
                        output.Append(@",");
                }
            }
            output.Append(@"],""Command"":" + Command + @",""Max"":" + MaxSpeed + @",""Power"":" + Power + @",""History"":" + (byte)History 
                + @",""BS"":" + BSModifier + @",""Locked"":" + (ArmourLocked ? 1 : 0) + @",""Nav"":" + NavigateWarp + @",""Shields"":" + (byte)VoidShields + @"}}");
            return output.ToString();
        }

        /// <summary>
        /// Description of the Hull to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                if (Command > 0)
                    output.Append("+" + Command + " to command tests; ");
                else if (Command > 0)
                    output.Append("+" + Command + " to command tests; ");
                if (DefaultProw != null)
                {
                    output.Append("Prow slot already equipped with " + DefaultProw.Name);
                    if (DefaultProw.Type == WeaponType.TorpedoTube)
                        output.Append(" with ammo capacity of " + ((TorpedoTubes)DefaultProw).Capacity);
                    output.Append("; ");
                }
                if (DefaultBroadside != null)
                    output.Append("1 Port and Starboard slot already equipped with " + DefaultBroadside.Name
                        + "; ");
                foreach (String component in DefaultComponents.Select(c => c.Name).Distinct())
                {
                    int count = DefaultComponents.Where(c => c.Name == component).Count();
                    output.Append("Comes with " + count + " built in " + component + (count > 1 ? "s; " : "; "));
                }
                if (History != ShipHistory.None)
                    output.Append("Past History is always " + History.Name() + ";");
                if (BSModifier != 0)
                    output.Append((BSModifier > 0 ? "+" : "") + BSModifier + " to hit with this ship's weapons");
                if (NavigateWarp != 0)
                    output.Append((NavigateWarp > 0 ? "+" : "") + NavigateWarp + " to navigate during warp travel;");
                if (ArmourLocked)
                    output.Append("May not be given armour upgrades;");
                if (VoidShields != HullTypes)
                    output.Append("May attach Void Shields as if " + VoidShields.ToString());
                output.Append(base.Special);
                return output.ToString();
            }
        }
    }
}
