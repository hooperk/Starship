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
        public string Name { get; private set; }
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
                        return Math.Max(_sp - 1, 1);
                    case Quality.Good:
                    case Quality.Slim:
                    case Quality.Efficient:
                        return _sp + 1;
                    case Quality.Best:
                        return _sp + 2;
                    default:
                        return _sp;
                }
            }
            private set { _sp = value; }
        }
        private int _sp;
        /// <summary>
        /// Power Used or supplied by component
        /// </summary>
        public virtual int Power
        {
            get
            {
                if (_power == 0)
                    return 0;//TODO: check if there was any case where this is what happens
                switch (this.Quality)
                {
                    case Quality.Poor:
                        return _power + 1;
                    case Quality.Good:
                    case Quality.Efficient:
                    case Quality.Best:
                        return Math.Max(_power - 1, 1);
                    default:
                        return _power;
                }
            }
            private set { _power = value; }
        }//add quality modifier later on get
        protected int _power;
        /// <summary>
        /// Space taken up by component
        /// </summary>
        public virtual int Space
        {
            get
            {
                if (_space == 0)
                    return 0;
                switch (this.Quality)
                {
                    case Quality.Poor:
                        return _space + 1;
                    case Quality.Slim:
                    case Quality.Best:
                        return Math.Max(_space - 1, 1);
                    default:
                        return _space;
                }
            }
            set
            {
                _space = value;
            }
        }
        protected int _space;
        /// <summary>
        /// Any special effects of the component
        /// </summary>
        public virtual string Special
        {
            get
            {
                if (_special == null)
                    return "";
                return _special + ";";
            }
            set { _special = value; }
        }
        private string _special;
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
        public ComponentOrigin ComponentOrigin { get; private set; }

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
        public Component(string name, int sp, int power, int space, string special, RuleBook origin, byte page, HullType types, Quality quality = Quality.Common, ComponentOrigin comp = ComponentOrigin.Standard)
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
                StringBuilder output = new StringBuilder();
                if (!String.IsNullOrWhiteSpace(Special))
                    output.Append(Special);
                return output.ToString();
            }
        }

        /// <summary>
        /// Serialises the component
        /// </summary>
        /// <returns>JSON object as string</returns>
        public abstract string ToJSON();
    }
}
