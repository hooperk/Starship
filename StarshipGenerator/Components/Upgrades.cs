using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarshipGenerator.Utils;

namespace StarshipGenerator.Components
{
    public static class Upgrades
    {
        public static string CherubimAerieDescription(Quality cherubs)
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

        public static string CherubimAerieSpecial(Quality cherubs)
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

        // no special
        public static string CrewImprovements(Quality improvement)
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

        public static string OstentatiousDisplayOfWealthDescription(Quality display)
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

        public static string OstentatiousDisplayOfWealthSpecial(Quality display)
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

        public static string StarchartCollectionDescription(Quality starchart)
        {
            switch (starchart)
            {
                case Quality.Poor:
                    return "Reduce Warp Travel time by 1d5 days min 1, +25 to Exploration Objectives, -10 penalty on warp encounters table";
                case Quality.Common:
                    return "Reduce Warp Travel time by 1d5 days min 1, +25 to Exploration Objectives";
                case Quality.Good:
                    return "Reduce Warp Travel time by 1d5+5 days min 1, +25 to Exploration Objectives";
                case Quality.Best:
                    return "Reduce Warp Travel time by 1d5+5 days min 1, +50 to Exploration Objectives";
                default:
                    return null;
            }
        }

        public static string StarchartCollectionSpecial(Quality starchart)
        {
            switch (starchart)
            {
                case Quality.Poor:
                    return "Reduce Warp Travel time by 1d5 days min 1, -10 penalty on warp encounters table";
                case Quality.Common:
                    return "Reduce Warp Travel time by 1d5 days min 1";
                case Quality.Good:
                    return "Reduce Warp Travel time by 1d5+5 days min 1";
                case Quality.Best:
                    return "Reduce Warp Travel time by 1d5+5 days min 1";
                default:
                    return null;
            }
        }

        //Storm troopers are common, good or best, special is description
        public static string StormTrooperDetachment(Quality troopers)
        {
            switch (troopers)
            {
                case Quality.Common:
                    return "On hit and run, double hull damage, on defending win, add 1d5 to crew loss inflicted";
                case Quality.Good:
                    return "On hit and run, double hull damage, on defending win, add 1d5 to crew loss inflicted, +5 to opposed Command Tests";
                case Quality.Best:
                    return "On hit and run, double hull damage, on defending win, add 1d5 to crew loss inflicted, +10 to opposed Command Tests";
                default:
                    return null;
            }
        }

        //no special
        public static string VaultedCeilings(Quality ceilings)
        {
            switch (ceilings)
            {
                case Quality.Poor:
                    return "Reduce space base on hull size: Raiders 2, Frigates or Transports 3, Lights 4, Cruiser 5, Increase maximum morale by 5";
                case Quality.Common:
                    return "Reduce space base on hull size: Raiders 1, Frigates or Transports 2, Lights 3, Cruiser 4, Increase maximum morale by 5";
                case Quality.Good:
                    return "Reduce space base on hull size: Raiders 1, Frigates or Transports 2, Lights 3, Cruiser 4, Increase maximum morale by 7";
                case Quality.Best:
                    return "Reduce space base on hull size: Raiders 1, Frigates or Transports 2, Lights 3, Cruiser 4, Increase maximum morale by 10";
                default:
                    return null;
            }
        }

        //no special
        public static string ArresterEngines(Quality engines)
        {
            switch (engines)
            {
                case Quality.Poor:
                    return "+10 on pilot tests involving deceleration, failure by 2+ degrees damages engines, -1 speed until fixed";
                case Quality.Common:
                    return "+10 on pilot tests involving deceleration, failure by 3+ degrees damages engines, -1 speed until fixed";
                case Quality.Good:
                    return "+15 on pilot tests involving deceleration, failure by 3+ degrees damages engines, -1 speed until fixed";
                case Quality.Best:
                    return "+15 on pilot tests involving deceleration, failure by 4+ degrees damages engines, -1 speed until fixed";
                default:
                    return null;
            }
        }

        public static string DistributedCargoHoldDescription(Quality cargo)
        {
            switch (cargo)
            {
                case Quality.Poor:
                    return "-2 morale and -15 to repair tests, cargo components can only be detected on focused augury with 4+ degrees and can't be critically hit, +50 to criminal objectives";
                case Quality.Common:
                    return "-2 morale and -10 to repair tests, cargo components can only be detected on focused augury with 4+ degrees and can't be critically hit, +50 to criminal objectives";
                case Quality.Good:
                    return "-2 morale and -5 to repair tests, cargo components can only be detected on focused augury with 4+ degrees and can't be critically hit, +50 to criminal objectives";
                case Quality.Best:
                    return "-2 morale and -5 to repair tests, cargo components can only be detected on focused augury with 4+ degrees and can't be critically hit, +75 to criminal objectives";
                default:
                    return null;
            }
        }

        public static string DistributedCargoHoldSpecial(Quality cargo)
        {
            if (cargo != Quality.None)
                return "cargo components can only be detected on focused augury with 4+ degrees and can't be critically hit";
            return null;
        }

        //no poor quality
        public static string DisciplinariumDescription(Quality discipline)
        {
            switch (discipline)
            {
                case Quality.Common:
                    return "Once per day: Reduce crew by 1 and increase morale by 2";
                case Quality.Good:
                    return "Once per day: Reduce crew by 1 and increase morale by 2, +5 to command tests";
                case Quality.Best:
                    return "Once per day: Reduce crew by 1 and increase morale by 3, +5 to command tests";
                default:
                    return null;
            }
        }

        public static string DisciplinariumSpecial(Quality discipline)
        {
            switch (discipline)
            {
                case Quality.Common:
                case Quality.Good:
                    return "Once per day: Reduce crew by 1 and increase morale by 2";
                case Quality.Best:
                    return "Once per day: Reduce crew by 1 and increase morale by 3";
                default:
                    return null;
            }
        }

        //no special
        public static string MimicDrive(Quality drive)
        {
            switch (drive)
            {
                case Quality.Poor:
                    return "Opposed +20 Tech-Use vs Detection+Scrutiny to appear as a bigger vessel";
                case Quality.Common:
                    return "Opposed +20 Tech-Use vs Detection+Scrutiny to appear as a bigger vessel, -20 to attempt to impersonate a particular ship";
                case Quality.Good:
                    return "Opposed +30 Tech-Use vs Detection+Scrutiny to appear as a bigger vessel, -20 to attempt to impersonate a particular ship";
                case Quality.Best:
                    return "Opposed +30 Tech-Use vs Detection+Scrutiny to appear as a bigger vessel, -20 to attempt to impersonate a particular ship, has two pre-programmed ship signatures which may be impersonated without the -20";
                default:
                    return null;
            }
        }

        //no special
        public static string OverloadShieldCapacitors(Quality shields)
        {
            switch (shields)
            {
                case Quality.Poor:
                    return "Automatically reactivate shields after a salvo once per combat encounter or 24 hours, will reactivate with 1 less strength";
                case Quality.Common:
                    return "Automatically reactivate shields after a salvo once per combat encounter or 24 hours";
                case Quality.Good:
                    return "Automatically reactivate shields after a salvo three times per combat encounter or 24 hours";
                case Quality.Best:
                    return "Automatically reactivate shields after a salvo once per combat encounter or 24 hours, increase void shields strength by 1 at all times";
                default:
                    return null;
            }
        }

        public static string ResolutionArenaDescription(Quality arena)
        {
            switch (arena)
            {
                case Quality.Poor:
                    return "Permanently increase morale by 2";
                case Quality.Common:
                    return "Permanently increase morale by 3";
                case Quality.Good:
                    return "Permanently increase morale by 3, +10 to commerce tests aboard the ship";
                case Quality.Best:
                    return "Permanently increase morale by 5, +10 to commerce tests aboard the ship";
                default:
                    return null;
            }
        }

        public static string ResolutionArenaSpecial(Quality arena)
        {
            switch (arena)
            {
                case Quality.Good:
                case Quality.Best:
                    return "+10 to commerce tests aboard the ship";
                default:
                    return null;
            }
        }

        public static string SecondaryReactorDescription(Quality reactor)
        {
            switch (reactor)
            {
                case Quality.Poor:
                    return "Raiders and Frigates 1 space, transports 2, 3 light, 4 cruiser+, may make a -20 tech use test to boost speed by 2 for 1 turn. Speed is reduced by one on the following turn, or in case of failure immediately, 3+ degrees of failure halves speed until engine is repaired";
                case Quality.Common:
                    return "Raiders and Frigates 1 space, transports 2, 3 light, 4 cruiser+, may make a -10 tech use test to boost speed by 2 for 1 turn. Speed is reduced by one on the following turn, or in case of failure immediately, 3+ degrees of failure halves speed until engine is repaired";
                case Quality.Good:
                    return "Raiders and Frigates 1 space, transports 2, 3 light, 4 cruiser+, may make a -10 tech use test to boost speed by 2 for 2 turns. Speed is reduced by one on the 3rd turn, or in case of failure immediately, 3+ degrees of failure halves speed until engine is repaired";
                case Quality.Best:
                    return "Raiders and Frigates 1 space, transports 2, 3 light, 4 cruiser+, may make a -10 tech use test to boost speed by 3 for 2 turns. Speed is reduced by one on the 3rd turn, or in case of failure immediately, 3+ degrees of failure halves speed until engine is repaired";
                default:
                    return null;
            }
        }

        public static string SecondaryReactorSpecial(Quality reactor)
        {
            switch (reactor)
            {
                case Quality.Poor:
                    return "May make a -20 tech use test to boost speed by 2 for 1 turn. Speed is reduced by one on the following turn, or in case of failure immediately, 3+ degrees of failure halves speed until engine is repaired";
                case Quality.Common:
                    return "May make a -10 tech use test to boost speed by 2 for 1 turn. Speed is reduced by one on the following turn, or in case of failure immediately, 3+ degrees of failure halves speed until engine is repaired";
                case Quality.Good:
                    return "May make a -10 tech use test to boost speed by 2 for 2 turns. Speed is reduced by one on the 3rd turn, or in case of failure immediately, 3+ degrees of failure halves speed until engine is repaired";
                case Quality.Best:
                    return "May make a -10 tech use test to boost speed by 3 for 2 turns. Speed is reduced by one on the 3rd turn, or in case of failure immediately, 3+ degrees of failure halves speed until engine is repaired";
                default:
                    return null;
            }
        }

        //no poor
        public static string SuperiorDamageControl(Quality control)
        {
            switch (control)
            {
                case Quality.Common:
                    return "Depressurized units cause only 1d5 crew damage, firefighting is +0 command test, +10 to isolated repair tests";
                case Quality.Good:
                    return "Depressurized units cause only 1d5 crew damage, damage by fire only 1d5-1, firefighting is +0 command test, +10 to isolated repair tests";
                case Quality.Best:
                    return "Depressurized units cause only 1d5 crew damage, damage by fire only 1d5-1, firefighting is +0 command test, +10 to isolated repair tests, seals and vents burning components to prevent spread";
                default:
                    return null;
            }
        }

        //no special
        public static string TargettingMatrix(Quality matrix)
        {
            switch (matrix)
            {
                case Quality.Poor:
                    return "+5 to hit with one macrobattery or lance";
                case Quality.Common:
                    return "+5 to hit with all macrobatteries or lances";
                default:
                    return null;
            }
        }
    }
}
