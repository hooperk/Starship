using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    /// <summary>
    /// Gellar field essential component
    /// </summary>
    public class GellarField : Component
    {
        /// <summary>
        /// Create a new gellar field
        /// </summary>
        /// <param name="types">hull classes which may use this field</param>
        /// <param name="power">power used by this field</param>
        /// <param name="special">special rules of this field</param>
        /// <param name="origin">rulebook which contains this field</param>
        /// <param name="page">page number to find this field on</param>
        /// <param name="sp">cost of this field</param>
        public GellarField(HullType types, int power, string special, RuleBook origin, byte page, int sp = 0)
            : base(sp, power, 0, special, origin, page, types) { }
    }
}
