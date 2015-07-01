﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarshipGenerator
{
    public enum WeaponType : byte { Macrobattery, Lance, TorpedoTubes, LandingBays, NovaCannon }

    [Flags]
    public enum WeaponSlot : byte
    {
        None = 0x00,
        Prow = 0x01,
        Port = 0x02,
        Starboard = 0x04,
        Aft = 0x08,
        Dorsal = 0x10,
        Keel = 0x20,
        Lance = 0x40,
        //Potential for one more special case
        All = 0xFF
    }

    [Flags]
    public enum HullType : byte { 
        None = 0x00,
        Transport = 0x01, 
        Raider = 0x02, 
        Frigate = 0x04, 
        LightCruiser = 0x08, 
        Cruiser = 0x10, 
        BattleCruiser = 0x20, 
        GrandCruiser = 0x40,
        BattleShip = 0x80,//NOT IMPLEMENTED
        All = 0xFF
    }

    //Slim and efficient are for deciding what bonus good quality grants
    public enum Quality : byte { Poor, Common, Good, Slim, Efficient, Best }

    public enum Race : byte { Human, Servitor, Ork, Eldar, Kroot, Chaos, Rakgol }//more to come?

    public enum RuleBook : byte { 
        Custom = 0, 
        CoreRulebook, 
        IntoTheStorm, 
        HostileAcquisition, 
        BattlefleetKoronus, 
        LureoftheExpanse
    }
}