using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    /// <summary>
    /// Ship bridge essential component
    /// </summary>
    public class Bridge : Component
    {
        /// <summary>
        /// Manoeuvrability modifier this bridge grants
        /// </summary>
        public int Manoeuvrability { get; private set; }

        /// <summary>
        /// Create a new bridge
        /// </summary>
        /// <param name="types">ships which can use this bridge</param>
        /// <param name="power">power used by this bridge</param>
        /// <param name="space">space used by this bridge</param>
        /// <param name="origin">rulebook containing this bridge</param>
        /// <param name="page">page this bridge can be found on</param>
        /// <param name="special">special rules for this bridge</param>
        /// <param name="sp">cost of this bridge</param>
        /// <param name="quality">quality of this bridge</param>
        /// <param name="man">manoeuvrability modifier of this bridge</param>
        public Bridge(HullType types, int power, int space, RuleBook origin, byte page, string special = null,
            int sp = 0, Quality quality = Quality.Common, int man = 0)
            : base(sp, power, space, special, origin, page, types, quality) 
        {
            this.Manoeuvrability = man;
        }
    }
}
