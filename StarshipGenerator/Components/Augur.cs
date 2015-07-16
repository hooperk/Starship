using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    /// <summary>
    /// Augur array essential component
    /// </summary>
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
        /// Ballistic Skill modifier this array grants
        /// </summary>
        public int BSModifier { get; private set; }
        /// <summary>
        /// Any special effects of the array
        /// </summary>
        public override string Special
        {
            get
            {
                if (!base.Special.Contains("External"))
                    return "External" + (String.IsNullOrEmpty(base.Special) ? "" : "; " + base.Special);
                return base.Special;
            }
        }

        /// <summary>
        /// Create new augur array
        /// </summary>
        /// <param name="name">name of augur arrray</param>
        /// <param name="power">power used by this array</param>
        /// <param name="origin">rulebook containign this array</param>
        /// <param name="page">page this array can be found on</param>
        /// <param name="det">detection rating modifier of this array</param>
        /// <param name="special">special rules of this array</param>
        /// <param name="quality">quality of this array</param>
        /// <param name="sp">cost of this array</param>
        /// <param name="man">manoeuvrability modifier of this array</param>
        /// <param name="bs">ballistic skill modifier of this array</param>
        public Augur(string name, int power, RuleBook origin, byte page, int det = 0, String special = null,
            Quality quality = Quality.Common, int sp = 0, int man = 0, int bs = 0, 
            ComponentOrigin comp = ComponentOrigin.Standard)
            : base(name, sp, power, 0, special, origin, page, HullType.All, quality,comp)
        {
            this.Manoeuvrability = man;
            this.DetectionRating = det;
            this.BSModifier = bs;
        }

        /// <summary>
        /// Serialises the Augur Array
        /// </summary>
        /// <returns>JSON object as string</returns>
        public override string ToJSON()
        {
            /*
             * { 
             * "Augur" : { 
             *  "Name" : name, 
             *  "Power" : power, 
             *  "Origin" : origin, 
             *  "Page" : page, 
             *  "Det" : det, 
             *  "Special" : special, 
             *  "Quality" : quality, 
             *  "SP" : sp, 
             *  "Man": man, 
             *  "BS" : bs,
             *  "Comp" : comp }
             * }
             * */
            return @"{""Augur"":{""Name"":""" + Name.Escape() + @""",""Power"":" + Power + @",""Origin"":" + (byte)Origin + @",""Page"":" + PageNumber
                + @",""Det"":" + DetectionRating + @",""Special"":""" + base.Special.Escape() + @""",""Quality"":" + (byte)Quality + @",""SP"":" + SP
                    + @",""Man"":" + Manoeuvrability + @",""BS"":" + BSModifier + @",""Comp"":" + (byte)ComponentOrigin + @"}}";
        }

        /// <summary>
        /// Description of the Augur Array to display while picking
        /// </summary>
        public override string Description
        {
            get
            {
                StringBuilder output = new StringBuilder();
                //Show DetectionRating specially
                //if (DetectionRating > 0)
                //    output.Append("+" + DetectionRating + " Detection Rating; ");
                //else if (DetectionRating < 0)
                //    output.Append(DetectionRating + " Detection Rating; ");
                if (Manoeuvrability > 0)
                    output.Append("+" + Manoeuvrability + " Manoeuvrability; ");
                else if (Manoeuvrability < 0)
                    output.Append(Manoeuvrability + " Manoeuvrability; ");
                if (BSModifier > 0)
                    output.Append("+" + BSModifier + " Ballistic Skill; ");
                else if (BSModifier < 0)
                    output.Append(BSModifier + " Ballistic Skill; ");
                output.Append(Special);
                return output.ToString();
            }
        }
    }
}
