using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Warp drive essential component
    /// </summary>
    public class WarpDrive : Component
    {
        /// <summary>
        /// Create a new Warp Drive
        /// </summary>
        /// <param name="types">Types which can use this drive</param>
        /// <param name="power">power used by this drive</param>
        /// <param name="space">space used by this drive</param>
        /// <param name="origin">rulebook containing this drive</param>
        /// <param name="page">page to find this drive on</param>
        /// <param name="sp">cost of this drive</param>
        /// <param name="special">special rules of this drive</param>
        /// <param name="quality">quality of this drive</param>
        public WarpDrive(HullType types, int power, int space, RuleBook origin, byte page, int sp = 0, string special = null, 
            Quality quality = Quality.Common) : base(sp, power, space, special, origin, page, types, quality) { }
    }
}
