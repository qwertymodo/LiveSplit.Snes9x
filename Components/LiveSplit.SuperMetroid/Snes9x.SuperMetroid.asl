state("snes9x", "1.55")
{
    bool    IN_ROOM_TRANSITION  :   0x35C000, 0x0797;
    ushort  ROOM_ID             :   0x35C000, 0x079B;
    ushort  REGION_ID           :   0x35C000, 0x079F;
    byte255 MAP_CURRENT         :   0x35C000, 0x07F7;
    ushort  TIMER_STATE         :   0x35C000, 0x0943;
    ushort  GAME_STATE          :   0x35C000, 0x0998;
    ushort  MAX_ENERGY          :   0x35C000, 0x09C4;
    ushort  MAX_MISSILES        :   0x35C000, 0x09C8;
    ushort  MAX_SUPER_MISSILES  :   0x35C000, 0x09CC;
    ushort  MAX_POWER_BOMBS     :   0x35C000, 0x09D0;
    ushort  MAX_RESERVE         :   0x35C000, 0x09D4;
    ushort  IGT_FRAMES          :   0x35C000, 0x09DA;
    ushort  IGT_SECONDS         :   0x35C000, 0x09DC;
    ushort  IGT_MINUTES         :   0x35C000, 0x09DE;
    ushort  IGT_HOURS           :   0x35C000, 0x09E0;
    ushort  POSE                :   0x35C000, 0x0A1C;
    bool    HYPER_BEAM          :   0x35C000, 0x0A76;
    byte255 MAP_CRATERIA        :   0x35C000, 0xCD52;
    byte255 MAP_BRINSTAR        :   0x35C000, 0xCE52;
    byte255 MAP_NORFAIR         :   0x35C000, 0xCF52;
    byte255 MAP_WRECKED_SHIP    :   0x35C000, 0xD052;
    byte255 MAP_MARIDIA         :   0x35C000, 0xD152;
    byte255 MAP_TOURIAN         :   0x35C000, 0xD252;
    byte255 MAP_CERES           :   0x35C000, 0xD352;
    ushort  ENEMY_HEALTH        :   0x35C000, 0x0F8C;
    byte7   BOSSES_DEFEATED     :   0x35C000, 0xD828;
    byte20  PICKUPS             :   0x35C000, 0xD870;
}

state("snes9x", "1.54.1")
{
    bool    IN_ROOM_TRANSITION  :   0x348000, 0x0797;
    ushort  ROOM_ID             :   0x348000, 0x079B;
    ushort  REGION_ID           :   0x348000, 0x079F;
    byte255 MAP_CURRENT         :   0x348000, 0x07F7;
    ushort  TIMER_STATE         :   0x348000, 0x0943;
    ushort  GAME_STATE          :   0x348000, 0x0998;
    ushort  MAX_ENERGY          :   0x348000, 0x09C4;
    ushort  MAX_MISSILES        :   0x348000, 0x09C8;
    ushort  MAX_SUPER_MISSILES  :   0x348000, 0x09CC;
    ushort  MAX_POWER_BOMBS     :   0x348000, 0x09D0;
    ushort  MAX_RESERVE         :   0x348000, 0x09D4;
    ushort  IGT_FRAMES          :   0x348000, 0x09DA;
    ushort  IGT_SECONDS         :   0x348000, 0x09DC;
    ushort  IGT_MINUTES         :   0x348000, 0x09DE;
    ushort  IGT_HOURS           :   0x348000, 0x09E0;
    ushort  POSE                :   0x348000, 0x0A1C;
    bool    HYPER_BEAM          :   0x348000, 0x0A76;
    ushort  ENEMY_HEALTH        :   0x348000, 0x0F8C;
    byte255 MAP_CRATERIA        :   0x348000, 0xCD52;
    byte255 MAP_BRINSTAR        :   0x348000, 0xCE52;
    byte255 MAP_NORFAIR         :   0x348000, 0xCF52;
    byte255 MAP_WRECKED_SHIP    :   0x348000, 0xD052;
    byte255 MAP_MARIDIA         :   0x348000, 0xD152;
    byte255 MAP_TOURIAN         :   0x348000, 0xD252;
    byte255 MAP_CERES           :   0x348000, 0xD352;
    byte7   BOSSES_DEFEATED     :   0x348000, 0xD828;
    byte20  PICKUPS             :   0x348000, 0xD870;
}

state("snes9x", "1.53")
{
    bool    IN_ROOM_TRANSITION  :   0x2F0010, 0x0797;
    ushort  ROOM_ID             :   0x2F0010, 0x079B;
    ushort  REGION_ID           :   0x2F0010, 0x079F;
    byte255 MAP_CURRENT         :   0x2F0010, 0x07F7;
    ushort  TIMER_STATE         :   0x2F0010, 0x0943;
    ushort  GAME_STATE          :   0x2F0010, 0x0998;
    ushort  MAX_ENERGY          :   0x2F0010, 0x09C4;
    ushort  MAX_MISSILES        :   0x2F0010, 0x09C8;
    ushort  MAX_SUPER_MISSILES  :   0x2F0010, 0x09CC;
    ushort  MAX_POWER_BOMBS     :   0x2F0010, 0x09D0;
    ushort  MAX_RESERVE         :   0x2F0010, 0x09D4;
    ushort  IGT_FRAMES          :   0x2F0010, 0x09DA;
    ushort  IGT_SECONDS         :   0x2F0010, 0x09DC;
    ushort  IGT_MINUTES         :   0x2F0010, 0x09DE;
    ushort  IGT_HOURS           :   0x2F0010, 0x09E0;
    ushort  POSE                :   0x2F0010, 0x0A1C;
    bool    HYPER_BEAM          :   0x2F0010, 0x0A76;
    ushort  ENEMY_HEALTH        :   0x2F0010, 0x0F8C;
    byte255 MAP_CRATERIA        :   0x2F0010, 0xCD52;
    byte255 MAP_BRINSTAR        :   0x2F0010, 0xCE52;
    byte255 MAP_NORFAIR         :   0x2F0010, 0xCF52;
    byte255 MAP_WRECKED_SHIP    :   0x2F0010, 0xD052;
    byte255 MAP_MARIDIA         :   0x2F0010, 0xD152;
    byte255 MAP_TOURIAN         :   0x2F0010, 0xD252;
    byte255 MAP_CERES           :   0x2F0010, 0xD352;
    byte7   BOSSES_DEFEATED     :   0x2F0010, 0xD828;
    byte20  PICKUPS             :   0x2F0010, 0xD870;
}

init
{
    version = modules.First().FileVersionInfo.FileVersion;
}

startup
{
    vars.splitCount = 0;
    vars.checkpoints = new List<string>();
    vars.listIDs = new List<string>();
    vars.checkpointLists = new Dictionary <string, List<string>>();

    settings.Add("checkpointList", true, "****ONLY SELECT ONE LIST****");

    var AddCheckpointList = (Action<string, string, bool, List<string>>)((id, title, selected, checkpoints) =>
    {
        System.Globalization.TextInfo TII = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
        vars.listIDs.Add(id);
        vars.checkpointLists.Add(id, checkpoints);

        settings.Add(id + "_LIST", selected, title, "checkpointList");
        foreach (string c in checkpoints)
            settings.Add(id + "_" + c, true, TII.ToTitleCase(c.Replace("_", " ").ToLower()), id + "_LIST");
    });

    // Available checkpoint strings, since proper Enums don't work here
    // COLLECT_FIRST_ENERGY_TANK
    // COLLECT_ALL_ENERGY_TANKS
    // COLLECT_RIDLEY_ENERGY_TANK
    // COLLECT_FIRST_RESERVE_TANK
    // COLLECT_ALL_RESERVE_TANKS
    // COLLECT_FIRST_MISSILES
    // COLLECT_ALL_MISSILES
    // COLLECT_FIRST_SUPER_MISSILES
    // COLLECT_ALL_SUPER_MISSILES
    // COLLECT_FIRST_POWER_BOMBS
    // COLLECT_ALL_POWER_BOMBS
    // COLLECT_GRAPPLE_BEAM
    // COLLECT_XRAY
    // COLLECT_CHARGE_BEAM
    // COLLECT_ICE_BEAM
    // COLLECT_WAVE_BEAM
    // COLLECT_SPAZER
    // COLLECT_PLASMA_BEAM
    // COLLECT_HYPER_BEAM
    // COLLECT_VARIA_SUIT
    // COLLECT_GRAVITY_SUIT
    // COLLECT_MORPH_BALL
    // COLLECT_BOMBS
    // COLLECT_SPRING_BALL
    // COLLECT_SCREW_ATTACK
    // COLLECT_HIGH_JUMP_BOOTS
    // COLLECT_SPACE_JUMP
    // COLLECT_SPEED_BOOSTER
    // DEFEAT_TORIZO
    // DEFEAT_SPORE_SPAWN
    // DEFEAT_KRAID
    // DEFEAT_CROCOMIRE
    // DEFEAT_PHANTOON
    // DEFEAT_BOTWOON
    // DEFEAT_DRAYGON
    // DEFEAT_GOLDEN_TORIZO
    // DEFEAT_RIDLEY
    // DEFEAT_MOTHER_BRAIN_1
    // DEFEAT_MOTHER_BRAIN_2
    // DEFEAT_MOTHER_BRAIN_3
    // ENTER_RIDLEY_MOUTH
    // ENTER_GOLDEN_FOUR
    // SAVE_POINT_RIDLEY
    // SAVE_POINT_TOURIAN
    // ESCAPE_CERES
    // ESCAPE_ZEBES
    // COMPLETE_MAP_CERES
    // COMPLETE_MAP_CRATERIA
    // COMPLETE_MAP_BRINSTAR
    // COMPLETE_MAP_NORFAIR
    // COMPLETE_MAP_WRECKED_SHIP
    // COMPLETE_MAP_MARIDIA
    // COMPLETE_MAP_TOURIAN

//===== ADD NEW CHECKPOINT LISTS HERE =====
    // My Splits
    AddCheckpointList("MY", "====My List====", true, new List<string>
    {
        "COLLECT_BOMBS",
        "COLLECT_FIRST_SUPER_MISSILES",
        "COLLECT_VARIA_SUIT",
        "COLLECT_SPEED_BOOSTER",
        "COLLECT_GRAPPLE_BEAM",
        "DEFEAT_PHANTOON",
        "COLLECT_GRAVITY_SUIT",
        "COLLECT_SPACE_JUMP",
        "COLLECT_PLASMA_BEAM",
        "COLLECT_SCREW_ATTACK",
        "SAVE_POINT_RIDLEY",
        "ENTER_GOLDEN_FOUR",
        "ESCAPE_ZEBES"
    });

    // Low% Ice
    AddCheckpointList("LOW_ICE", "====14% Ice====", false, new List<string>
    {
        "COLLECT_MORPH_BALL",
        "COLLECT_BOMBS",
        "COLLECT_FIRST_SUPER_MISSILES",
        "COLLECT_CHARGE_BEAM",
        "COLLECT_FIRST_POWER_BOMBS",
        "DEFEAT_PHANTOON",
        "COLLECT_GRAVITY_SUIT",
        "COLLECT_VARIA_SUIT",
        "COLLECT_ICE_BEAM",
        "ENTER_RIDLEY_MOUTH",
        "COLLECT_RIDLEY_ENERGY_TANK",
        "DEFEAT_DRAYGON",
        "ENTER_GOLDEN_FOUR",
        "ESCAPE_ZEBES"
    });

    // Any% KPDR

    // Any% PRKD

    // 100%
    AddCheckpointList("ALL_ITEMS", "====100% Item Collection====", false, new List<string>
    {
        "COLLECT_MORPH_BALL",
        "COLLECT_BOMBS",
        "COLLECT_FIRST_SUPER_MISSILES",
        "COLLECT_CHARGE_BEAM",
        "COLLECT_SPAZER",
        "COLLECT_VARIA_SUIT",
        "COLLECT_HIGH_JUMP_BOOTS",
        "COLLECT_SPEED_BOOSTER",
        "COLLECT_WAVE_BEAM",
        "COLLECT_FIRST_POWER_BOMBS",
        "COLLECT_GRAPPLE_BEAM",
        "DEFEAT_PHANTOON",
        "COLLECT_GRAVITY_SUIT",
        "COLLECT_SPACE_JUMP",
        "COLLECT_SPRING_BALL",
        "COLLECT_PLASMA_BEAM",
        "COLLECT_ICE_BEAM",
        "COLLECT_SCREW_ATTACK",
        "COLLECT_XRAY",
        "COLLECT_ALL_RESERVE_TANKS",
        "COLLECT_ALL_POWER_BOMBS",
        "COLLECT_ALL_ENERGY_TANKS",
        "COLLECT_ALL_SUPER_MISSILES",
        "COLLECT_ALL_MISSILES",
        "ENTER_GOLDEN_FOUR",
        "ESCAPE_ZEBES"
    });

    // 100% Map Completion Splits
    AddCheckpointList("ALL_MAP", "====100% Map Completion====", false, new List<string>
    {
        "COMPLETE_MAP_CERES",
        "COMPLETE_MAP_BRINSTAR",
        "COMPLETE_MAP_NORFAIR",
        "COMPLETE_MAP_MARIDIA",
        "COMPLETE_MAP_WRECKED_SHIP",
        "COMPLETE_MAP_TOURIAN",
        "COMPLETE_MAP_CRATERIA",
        "ESCAPE_ZEBES"
    });

    // RBO
    AddCheckpointList("RBO", "====Reverse Boss Order====", false, new List<string>
    {
        "COLLECT_MORPH_BALL",
        "COLLECT_BOMBS",
        "COLLECT_FIRST_SUPER_MISSILES",
        "COLLECT_CHARGE_BEAM",
        "COLLECT_HIGH_JUMP_BOOTS",
        "COLLECT_SPEED_BOOSTER",
        "COLLECT_WAVE_BEAM",
        "DEFEAT_CROCOMIRE",
        "COLLECT_GRAPPLE_BEAM",
        "ENTER_RIDLEY_MOUTH",
        "COLLECT_SCREW_ATTACK",
        "DEFEAT_GOLDEN_TORIZO",
        "COLLECT_RIDLEY_ENERGY_TANK",
        "COLLECT_ICE_BEAM",
        "DEFEAT_BOTWOON",
        "COLLECT_SPACE_JUMP",
        "COLLECT_PLASMA_BEAM",
        "DEFEAT_PHANTOON",
        "DEFEAT_KRAID",
        "ENTER_GOLDEN_FOUR",
        "ESCAPE_ZEBES"
    });
}

start
{
    vars.splitCount = 0;
    vars.checkpoints.Clear();

    if (old.ROOM_ID == 0x0000 && current.ROOM_ID == 0xDF45)
    {
        foreach (string id in vars.listIDs)
        {
            if (settings[id + "_LIST"])
            {
                foreach (string c in vars.checkpointLists[id])
                {
                    if (settings[id + "_" + c])
                        vars.checkpoints.Add(c);
                }
                break;
            }
        }

        //foreach(string c in vars.checkpoints)
            //print(c);

        return true;
    }

    return false;
}

reset
{
    return current.GAME_STATE == 0;
}

update
{

}

gameTime
{
    return new TimeSpan(0, (int)current.IGT_HOURS, (int)current.IGT_MINUTES, (int)current.IGT_SECONDS, (int)current.IGT_FRAMES * 1000 / 60);
}

split
{
    var Checkpoint = (Func<bool, bool>)((inProgress) =>
    {
        if (!inProgress) ++vars.splitCount;
        return !inProgress;
    });

    //print("Split count: " + vars.splitCount);
    print("Room ID: " + current.ROOM_ID);
    switch ((string)vars.checkpoints[(int)vars.splitCount])
    {
        case "COLLECT_FIRST_ENERGY_TANK":
            return Checkpoint(current.MAX_ENERGY < 199);
        
        case "COLLECT_ALL_ENERGY_TANKS":
            return Checkpoint(current.MAX_ENERGY < 1499);
        
        case "COLLECT_RIDLEY_ENERGY_TANK":
            print("Not Implemented");
            break;
        
        case "COLLECT_FIRST_RESERVE_TANK":
            return Checkpoint(current.MAX_RESERVE < 100);
        
        case "COLLECT_ALL_RESERVE_TANKS":
            return Checkpoint(current.MAX_RESERVE < 400);
        
        case "COLLECT_FIRST_MISSILES":
            return Checkpoint(current.MAX_MISSILES < 5);
        
        case "COLLECT_ALL_MISSILES":
            return Checkpoint(current.MAX_MISSILES < 230);
        
        case "COLLECT_FIRST_SUPER_MISSILES":
            //return Checkpoint((current.PICKUPS[2] & 0x01) == 0);
            return Checkpoint(current.MAX_SUPER_MISSILES < 5);
        
        case "COLLECT_ALL_SUPER_MISSILES":
            return Checkpoint(current.MAX_SUPER_MISSILES < 50);
        
        case "COLLECT_FIRST_POWER_BOMBS":
            return Checkpoint(current.MAX_POWER_BOMBS < 5);
        
        case "COLLECT_ALL_POWER_BOMBS":
            return Checkpoint(current.MAX_POWER_BOMBS < 50);
        
        case "COLLECT_GRAPPLE_BEAM":
            return Checkpoint((current.PICKUPS[7] & 0x10) == 0);
        
        case "COLLECT_XRAY":
            return Checkpoint((current.PICKUPS[4] & 0x40) == 0);
        
        case "COLLECT_CHARGE_BEAM":
            return Checkpoint((current.PICKUPS[2] & 0x80) == 0);
        
        case "COLLECT_ICE_BEAM":
            return Checkpoint((current.PICKUPS[6] & 0x04) == 0);
        
        case "COLLECT_WAVE_BEAM":
            return Checkpoint((current.PICKUPS[8] & 0x10) == 0);
        
        case "COLLECT_SPAZER":
            return Checkpoint((current.PICKUPS[5] & 0x04) == 0);
        
        case "COLLECT_PLASMA_BEAM":
            return Checkpoint((current.PICKUPS[17] & 0x80) == 0);
        
        case "COLLECT_HYPER_BEAM":
            return Checkpoint(!current.HYPER_BEAM);
        
        case "COLLECT_VARIA_SUIT":
            return Checkpoint((current.PICKUPS[6] & 0x01) == 0);
        
        case "COLLECT_GRAVITY_SUIT":
            return Checkpoint((current.PICKUPS[16] & 0x80) == 0);

        case "COLLECT_MORPH_BALL":
            return Checkpoint((current.PICKUPS[3] & 0x04) == 0);
        
        case "COLLECT_BOMBS":
            return Checkpoint((current.PICKUPS[0] & 0x80) == 0);
        
        case "COLLECT_SPRING_BALL":
            return Checkpoint((current.PICKUPS[18] & 0x40) == 0);
        
        case "COLLECT_SCREW_ATTACK":
            return Checkpoint((current.PICKUPS[9] & 0x80) == 0);
        
        case "COLLECT_HIGH_JUMP_BOOTS":
            return Checkpoint((current.PICKUPS[6] & 0x20) == 0);
        
        case "COLLECT_SPACE_JUMP":
            return Checkpoint((current.PICKUPS[19] & 0x04) == 0);
        
        case "COLLECT_SPEED_BOOSTER":
            return Checkpoint((current.PICKUPS[8] & 0x04) == 0);
        
        case "DEFEAT_TORIZO":
            // return Checkpoint((current.BOSSES_DEFEATED[0] & 0x04) == 0);
            return Checkpoint((current.ROOM_ID != 0x9804) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_SPORE_SPAWN":
            // return Checkpoint((current.BOSSES_DEFEATED[1] & 0x02) == 0);
            return Checkpoint((current.ROOM_ID != 0x9DC7) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_KRAID":
            // return Checkpoint((current.BOSSES_DEFEATED[1] & 0x01) == 0);
            return Checkpoint((current.ROOM_ID != 0xA59F) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_CROCOMIRE":
            // return Checkpoint((current.BOSSES_DEFEATED[2] & 0x02) == 0);
            return Checkpoint((current.ROOM_ID != 0xA98D) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_PHANTOON":
            // return Checkpoint((current.BOSSES_DEFEATED[3] & 0x01) == 0);
            return Checkpoint((current.ROOM_ID != 0xCD13) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_BOTWOON":
            // return Checkpoint((current.BOSSES_DEFEATED[4] & 0x02) == 0);
            return Checkpoint((current.ROOM_ID != 0xD95E) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_DRAYGON":
            // return Checkpoint((current.BOSSES_DEFEATED[4] & 0x01) == 0);
            return Checkpoint((current.ROOM_ID != 0xDA60) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_GOLDEN_TORIZO":
            // return Checkpoint((current.BOSSES_DEFEATED[2] & 0x04) == 0);
            return Checkpoint((current.ROOM_ID != 0xB283) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_RIDLEY":
            // return Checkpoint((current.BOSSES_DEFEATED[2] & 0x01) == 0);
            return Checkpoint((current.ROOM_ID != 0xB32E) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.ENEMY_HEALTH != 0));
        
        case "DEFEAT_MOTHER_BRAIN_1":
            print("Not Implemented");
            break;
        
        case "DEFEAT_MOTHER_BRAIN_2":
            print("Not Implemented");
            break;
        
        case "DEFEAT_MOTHER_BRAIN_3":
            // return Checkpoint((current.BOSSES_DEFEATED[5] & 0x02) == 0);
            print("Not Implemented");
            break;

        case "ENTER_RIDLEY_MOUTH":
            return Checkpoint((current.ROOM_ID != 0xAF3Fe) ||
                              (current.IN_ROOM_TRANSITION));
        
        case "ENTER_GOLDEN_FOUR":
            return Checkpoint((current.ROOM_ID != 0xA66A) ||
                              (current.IN_ROOM_TRANSITION));
        
        case "SAVE_POINT_RIDLEY":
            return Checkpoint((current.ROOM_ID != 0xB741) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (old.POSE != 0x0000 && old.POSE != 0x009B) ||
                              (current.POSE == 0x0000 || current.POSE == 0x009B));
        
        case "SAVE_POINT_TOURIAN":
            return Checkpoint((current.ROOM_ID != 0xDE23) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (old.POSE != 0x0000 && old.POSE != 0x009B) ||
                              (current.POSE == 0x0000 || current.POSE == 0x009B));
        
        case "ESCAPE_CERES":
            return Checkpoint((current.ROOM_ID != DF45) ||
                              (old.TIMER_STATE != 0x06) ||
                              (current.TIMER_STATE != 0x00));
        
        case "ESCAPE_ZEBES":
            return Checkpoint(((current.BOSSES_DEFEATED[5] & 0x02) == 0) ||
                              (current.ROOM_ID != 0x91F8) ||
                              (current.IN_ROOM_TRANSITION) ||
                              (current.POSE != 0x0000 &&
                               current.POSE != 0x009B));
            
        case "COMPLETE_MAP_CERES":
            {
                ulong total = 0;

                if (current.REGION_ID != 0x06)
                    return false;

                foreach (ushort x in current.MAP_CURRENT)
                    total += x;

                return Checkpoint(total < 0x0110);
            }
        
        case "COMPLETE_MAP_CRATERIA":
            {
                ulong total = 0;

                if (current.REGION_ID == 0x00)
                {
                    foreach (ushort x in current.MAP_CURRENT)
                        total += x;
                }

                else
                {
                    foreach (ushort x in current.MAP_CRATERIA)
                        total += x;
                }

                return Checkpoint(total < 0x1BA5);
            }
        
        case "COMPLETE_MAP_BRINSTAR":
            {
                ulong total = 0;

                if (current.REGION_ID == 0x01)
                {
                    foreach (ushort x in current.MAP_CURRENT)
                        total += x;
                }

                else
                {
                    foreach (ushort x in current.MAP_BRINSTAR)
                        total += x;
                }

                return Checkpoint(total < 0x21F6);
            }
        
        case "COMPLETE_MAP_NORFAIR":
            {
                ulong total = 0;

                if (current.REGION_ID == 0x02)
                {
                    foreach (ushort x in current.MAP_CURRENT)
                        total += x;
                }

                else
                {
                    foreach (ushort x in current.MAP_NORFAIR)
                        total += x;
                }

                return Checkpoint(total < 0x2A8C);
            }
        
        case "COMPLETE_MAP_WRECKED_SHIP":
            {
                ulong total = 0;

                if (current.REGION_ID == 0x03)
                {
                    foreach (ushort x in current.MAP_CURRENT)
                        total += x;
                }

                else
                {
                    foreach (ushort x in current.MAP_WRECKED_SHIP)
                        total += x;
                }

                return Checkpoint(total < 0x085E);
            }
        
        case "COMPLETE_MAP_MARIDIA":
            {
                ulong total = 0;

                if (current.REGION_ID == 0x04)
                {
                    foreach (ushort x in current.MAP_CURRENT)
                        total += x;
                }

                else
                {
                    foreach (ushort x in current.MAP_MARIDIA)
                        total += x;
                }

                return Checkpoint(total < 0x2110);
            }
        
        case "COMPLETE_MAP_TOURIAN":
            {
                ulong total = 0;

                if (current.REGION_ID == 0x05)
                {
                    foreach (ushort x in current.MAP_CURRENT)
                        total += x;
                }

                else
                {
                    foreach (ushort x in current.MAP_TOURIAN)
                        total += x;
                }

                return Checkpoint(total < 0x08B5);
            }
        

        default:
            print("Invalid Checkpoint");
            break;;
    }

    return false;
}
