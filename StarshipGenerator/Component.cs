using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    /// <summary>
    /// Components for a ship
    /// </summary>
    public abstract class Component
    {
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
        public virtual int Power { get; private set; }//add quality modifier later on get
        /// <summary>
        /// Space taken up by component
        /// </summary>
        public virtual int Space { get; set; }//add quality modifier later on get
        /// <summary>
        /// Any special effects of the component
        /// </summary>
        public virtual string Special
        {
            get
            {
                if (_special == null)
                    return "";
                return _special;
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
        /// Create a new Component object
        /// </summary>
        /// <param name="sp">cost of the component</param>
        /// <param name="power">power used or provided by the component</param>
        /// <param name="space">space taken or provided by the component</param>
        /// <param name="special">special rules for the component</param>
        /// <param name="origin">rulebook containing this component</param>
        /// <param name="page">page of the rulebook that the component may be found on</param>
        /// <param name="types">hulls which may use this component</param>
        /// <param name="quality">Quality of this component</param>
        public Component(int sp, int power, int space, string special, RuleBook origin, byte page, HullType types, Quality quality = Quality.Common)
        {
            this.SP = sp;
            this.Power = power;
            this.Space = space;
            this.Special = special;
            this.Origin = origin;
            this.PageNumber = page;
            this.HullTypes = types;
            this.Quality = quality;
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
    }
}
