using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Components for a ship
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// Name of the component
        /// </summary>
        public virtual string Name { get; private set; }
        /// <summary>
        /// Bitarray dictating if a component can be used with a particular hull
        /// </summary>
        public HullType HullTypes { get; private set; }
        /// <summary>
        /// Cost to add this Component
        /// </summary>
        public virtual int SP
        {
            get
            {
                switch (this.Quality)
                {
                    case Quality.Poor:
                        return Math.Max(RawSP - 1, 1);
                    case Quality.Good:
                    case Quality.Slim:
                    case Quality.Efficient:
                        return RawSP + 1;
                    case Quality.Best:
                        return RawSP + 2;
                    default:
                        return RawSP;
                }
            }
            private set { RawSP = value; }
        }
        public int RawSP{get; private set;}
        /// <summary>
        /// Power Used or supplied by component
        /// </summary>
        public virtual int Power
        {
            get
            {
                if (RawPower == 0)
                    return 0;//TODO: check if there was any case where this is what happens
                switch (this.Quality)
                {
                    case Quality.Poor:
                        return RawPower + 1;
                    case Quality.Good:
                    case Quality.Efficient:
                    case Quality.Best:
                        return Math.Max(RawPower - 1, 1);
                    default:
                        return RawPower;
                }
            }
            private set { RawPower = value; }
        }
        public int RawPower { get; protected set; }
        /// <summary>
        /// Space taken up by component
        /// </summary>
        public virtual int Space
        {
            get
            {
                if (RawSpace == 0)
                    return 0;
                switch (this.Quality)
                {
                    case Quality.Poor:
                        return RawSpace + 1;
                    case Quality.Good:
                    case Quality.Slim:
                    case Quality.Best:
                        return Math.Max(RawSpace - 1, 1);
                    default:
                        return RawSpace;
                }
            }
            set
            {
                RawSpace = value;
            }
        }
        public int RawSpace { get; protected set; }
        /// <summary>
        /// Any special effects of the component
        /// </summary>
        public virtual string Special
        {
            get
            {
                if (RawSpecial == null)
                    return "";
                return RawSpecial + ";";
            }
            set { RawSpecial = value; }
        }
        public string RawSpecial;
        /// <summary>
        /// Final Special to print, minus anything that doesn't need to go on the printing page like replacign other components
        /// </summary>
        public virtual string Print { get { return Special; } }
        /// <summary>
        /// Rulebook this component can be found in
        /// </summary>
        public RuleBook Origin { get; private set; }
        /// <summary>
        /// Page number of the rulebook to locate this component
        /// </summary>
        public byte PageNumber { get; private set; }
        /// <summary>
        /// Quality of the component
        /// </summary>
        public Quality Quality { get; set; }
        /// <summary>
        /// Origin of the component
        /// </summary>
        public virtual ComponentOrigin ComponentOrigin { get; private set; }
        /// <summary>
        /// Name of the component including the component's quality
        /// </summary>
        public String QualityName
        {
            get
            {
                switch (Quality)
                {
                    case Quality.Poor:
                        return "Poor Quality " + Name;
                    case Quality.Good:
                        return "Good Quality " + Name;
                    case Quality.Slim:
                        return "Good Quality Slim " + Name;
                    case Quality.Efficient:
                        return "Good Quality Efficient " + Name;
                    case Quality.Best:
                        return "Best Quality " + Name;
                    default:
                        return Name;
                }
            }
        }
        /// <summary>
        /// Current condition of the Component
        /// </summary>
        public Condition Condition;

        /// <summary>
        /// Create a new Component object
        /// </summary>
        /// <param name="name">name of component</param>
        /// <param name="sp">cost of the component</param>
        /// <param name="power">power used or provided by the component</param>
        /// <param name="space">space taken or provided by the component</param>
        /// <param name="special">special rules for the component</param>
        /// <param name="origin">rulebook containing this component</param>
        /// <param name="page">page of the rulebook that the component may be found on</param>
        /// <param name="types">hulls which may use this component</param>
        /// <param name="quality">Quality of this component</param>
        public Component(string name, int sp, int power, int space, string special, RuleBook origin, byte page, HullType types, Quality quality = Quality.Common, ComponentOrigin comp = ComponentOrigin.Standard, Condition cond = Condition.Intact)
        {
            this.Name = name;
            this.SP = sp;
            this.Power = power;
            this.Space = space;
            this.Special = special;
            this.Origin = origin;
            this.PageNumber = page;
            this.HullTypes = types;
            this.Quality = quality;
            this.ComponentOrigin = comp;
            this.Condition = cond;
        }

        /// <summary>
        /// Return the biggest ship this can be used by to determine priority for ships which can have components from multiple classes
        /// </summary>
        public int Priority
        {
            get
            {
                if ((HullTypes & HullType.BattleShip) != 0)
                    return 8;
                if ((HullTypes & HullType.GrandCruiser) != 0)
                    return 7;
                if ((HullTypes & HullType.BattleCruiser) != 0)
                    return 6;
                if ((HullTypes & HullType.Cruiser) != 0)
                    return 5;
                if ((HullTypes & HullType.LightCruiser) != 0)
                    return 4;
                if ((HullTypes & HullType.Frigate) != 0)
                    return 3;
                if ((HullTypes & HullType.Raider) != 0)
                    return 2;
                if ((HullTypes & HullType.Transport) != 0)
                    return 1;
                return 0;
            }
        }

        /// <summary>
        /// Description of the component to display while picking
        /// </summary>
        public virtual string Description
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Special))
                    return Special;
                return "";
            }
        }

        /// <summary>
        /// Serialises the component
        /// </summary>
        /// <returns>JSON object as string</returns>
        public abstract string ToJSON();

        /// <summary>
        /// Checks if the two Components are equal
        /// </summary>
        /// <param name="obj">Object to check equality against</param>
        /// <returns>false if other object is null, is not a component or does not match</returns>
        /// <remarks>If a component has the same name, origin and quality it is the same.
        /// Currently inequalities could match between two custom components</remarks>
        public override bool Equals(object obj)
        {
            Component other = obj as Component;
            if (other == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            return (this.QualityName.Equals(other.QualityName) && this.HullTypes == other.HullTypes && this.Origin == other.Origin);
        }

        /// <summary>
        /// Retrieve the hash code of the Component
        /// </summary>
        /// <returns>Hash Code of this Component</returns>
        public override int GetHashCode()
        {
            return this.QualityName.GetHashCode() + (7 * this.HullTypes.GetHashCode()) + (13 * this.Origin.GetHashCode());//checking equality off having the same name and the same quality
        }
    }

    public static class ComponentExtension
    {
        /// <summary>
        /// External method for making a component json which should properly handle nulls 
        /// </summary>
        /// <param name="self">Compoent to get JSON from</param>
        /// <returns>'null' if component is null, Component.ToJSON() otherwise</returns>
        public static String JSON(this Component self)
        {
            if (self == null)
                return @"null";
            return self.ToJSON();
        }

        /// <summary>
        /// External method for getting name of a component if it is not null and returning nothing otherwise
        /// </summary>
        /// <param name="self">Component to get the name of</param>
        /// <returns>The raw name of the component</returns>
        public static String GetName(this Component self)
        {
            if (self == null)
                return "";
            if (self is PlasmaDrive)
                return ((PlasmaDrive)self).RawName;//as plasmadrive is the only class that renames itself, handle rawname in it
            return self.Name;
        }

        public static IEnumerable<Component> Highest(this IEnumerable<Component> self)
        {
            var groups = from component in self group component by component.GetName();
            List<Component> components = new List<Component>();
            foreach (var group in groups)
            {
                yield return group.OrderBy(x => x.HullTypes).First();
            }
        }
    }
}
