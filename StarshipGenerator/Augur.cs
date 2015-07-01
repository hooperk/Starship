using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    public class Augur : Component
    {
        /// <summary>
        /// Manoeuvrability modifier this array grants
        /// </summary>
        public int Manoeuvrability { get; private set; }
        /// <summary>
        /// Detection rating modifier this array grants
        /// </summary>
        public int DetectionRating { get; private set; }
        /// <summary>
        /// Any special effects of the array
        /// </summary>
        public override string Special
        {
            get
            {
                return "External" + (String.IsNullOrEmpty(base.Special) ? "" : ", " + base.Special);
            }
        }

        /// <summary>
        /// Create new augur array
        /// </summary>
        /// <param name="power">power used by this array</param>
        /// <param name="origin">rulebook containign this array</param>
        /// <param name="page">page this array can be found on</param>
        /// <param name="det">detection rating modifier of this array</param>
        /// <param name="special">special rules of this array</param>
        /// <param name="quality">quality of this array</param>
        /// <param name="sp">cost of this array</param>
        /// <param name="man">manoeuvrability modifier of this array</param>
        public Augur(int power, RuleBook origin, byte page, int det = 0, String special = null,
            Quality quality = Quality.Common, int sp = 0, int man = 0)
            : base(sp, power, 0, special, origin, page, HullType.All, quality) { }
    }
}
