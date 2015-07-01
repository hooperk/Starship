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
        public virtual int SP { get; private set; }//add quality modifier later on get
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
    }
}
