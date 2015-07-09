using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    public class Upgrades
    {
        public string CherubimAerieDescription(Quality cherubs)
        {
            switch (cherubs)
            {
                case Quality.Poor:
                    return "Decrease morale permanently by 2, +1d10+10 to any endeavour, once per objective";
                case Quality.Common:
                    return "Decrease morale permanently by 1, +1d10+10 to any endeavour, once per objective";
                case Quality.Good:
                    return "Decrease morale permanently by 1, +1d10+15 to any endeavour, once per objective";
                case Quality.Best:
                    return "Decrease morale permanently by 1, +1d10+20 to any endeavour, once per objective";
                default:
                    return null;
            }
        }
        
        public string CherubimAerieSpecial(Quality cherubs)
        {
            switch (cherubs)
            {
                case Quality.Poor:
                case Quality.Common:
                    return "+1d10+10 to any endeavour, once per objective";
                case Quality.Good:
                    return "+1d10+15 to any endeavour, once per objective";
                case Quality.Best:
                    return "+1d10+20 to any endeavour, once per objective";
                default:
                    return null;
            }
        }

        public string CrewImprovementsDescription(Quality improvement)
        {
            switch (improvement)
            {
                case Quality.Poor:
                    return "Permanently improve max morale by 1";
                case Quality.Common:
                    return "Permanently improve max morale by 2";
                case Quality.Good:
                    return "Permanently improve max morale by 3";
                case Quality.Best:
                    return "Permanently improve max morale by 5";
                default:
                    return null;
            }
        }

        public string OstentatiousDisplayOfWealthDescription(Quality display)
        {
            switch (display)
            {
                case Quality.Poor:
                    return "-10 to social tests to influence visitors, +25 to Trade or Criminal Objectives";
                case Quality.Common:
                    return "+10 to social tests to influence visitors, +25 to Trade or Criminal Objectives";
                case Quality.Good:
                    return "+15 to social tests to influence visitors, +25 to Trade or Criminal Objectives";
                case Quality.Best:
                    return "+20 to social tests to influence visitors, +25 to Trade or Criminal Objectives";
                default:
                    return null; 
            }
        }

        public string OstentatiousDisplayOfWealthSpecial(Quality display)
        {
            switch (display)
            {
                case Quality.Poor:
                    return "-10 to social tests to influence visitors";
                case Quality.Common:
                    return "+10 to social tests to influence visitors";
                case Quality.Good:
                    return "+15 to social tests to influence visitors";
                case Quality.Best:
                    return "+20 to social tests to influence visitors";
                default:
                    return null;
            }
        }
    }
}
