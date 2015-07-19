using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Ammo
{
    /// <summary>
    /// Squadron class represnting squadrons of small craft
    /// </summary>
    public class Squadron
    {
        /// <summary>
        /// Name of the Squadron
        /// </summary>
        public string Name {get; private set; }
        /// <summary>
        /// Race Squadron belongs to
        /// </summary>
        public Race Race { get; private set; }
        /// <summary>
        /// Craft Rating of the Squadron
        /// </summary>
        public int CraftRating { get; private set; }
        /// <summary>
        /// VUs squadron may move per turn
        /// </summary>
        public int Speed { get; private set; }
        /// <summary>
        /// Maximum size of squadron
        /// </summary>
        public int MaxSize { get; private set; }
        /// <summary>
        /// Current Size of the Squadron
        /// </summary>
        public int CurrentSize;
        /// <summary>
        /// Squadron strength as abstract
        /// </summary>
        public Strength Strength
        {
            get
            {
                if (CurrentSize == MaxSize)
                    return Strength.Full;
                if (CurrentSize >= Math.Floor((float)MaxSize / 2.0))
                    return Strength.Half;
                return Strength.Destroyed;
            }
            set
            {
                switch (value)
                {
                    case Strength.Full:
                        CurrentSize = MaxSize;
                        break;
                    case Strength.Half:
                        CurrentSize = MaxSize / 2;
                        break;
                    case Strength.Destroyed:
                        CurrentSize = 0;
                        break;
                }
            }
        }
        /// <summary>
        /// Special rules for the squadron
        /// </summary>
        public String Special { get; private set; }
        /// <summary>
        /// Rulebook containing rules for this squadron
        /// </summary>
        public RuleBook Origin { get; private set; }
        /// <summary>
        /// Page of rulebook that rules for this squadron are on
        /// </summary>
        public byte Page { get; private set; }

        /// <summary>
        /// A new Squadron at full strength
        /// </summary>
        /// <param name="rating">Craft Rating of Squad</param>
        /// <param name="speed">Speed of Squadron</param>
        /// <param name="size">Maximum Size of Squadron</param>
        public Squadron(String name, Race race, int rating, int speed, int size, string special = null, RuleBook origin = RuleBook.BattlefleetKoronus, byte page = 14)
        {
            this.Name = name;
            this.Race = race;
            this.CraftRating = rating;
            this.Speed = speed;
            this.CurrentSize = this.MaxSize = size;
            this.Special = special;
            this.Origin = origin;
            this.Page = page;
        }

        /// <summary>
        /// Serialises the Squadron
        /// </summary>
        /// <returns>Json object as a string</returns>
        public string ToJSON()
        {
            /*{
             * "Squadron" : {
             *  "Name" : name,
             *  "Race" : race,
             *  "Rating" : rating,
             *  "Speed" : speed,
             *  "Max" : max,
             *  "Size" : size }
             *}
             */
            return @"{""Squadron"":{""Name"":""" + Name.Escape() + @""",""Race"":" + (byte)Race + @",""Rating"":" + CraftRating 
                + @",""Speed"":" + Speed + @",""Max"":" + MaxSize + @",""Size"":" + CurrentSize + @",""Special"":""" + Special.Escape() 
                + @""",""Origin"":" + (byte)Origin + @",""Page"":" + Page + @"}}";
        }
    }
}
