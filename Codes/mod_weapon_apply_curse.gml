function mod_weapon_apply_curse()
{
    var _eff = argument0

    var name = ds_map_find_value_ext(data, "idName", "N/A")
    var dur = ds_map_find_value_ext(data, "Duration", noone)
    var max_dur = ds_map_find_value_ext(data, "MaxDuration", noone)
    var _is_quest = ds_map_find_value_ext(data, "is_quest_item", noone)
    var _has_owner = ds_map_find_value_ext(data, "HasOwner", noone)
    var _town = ds_map_find_value_ext(data, "Town", "N/A")
    var _stolen_days = ds_map_find_value_ext(data, "Stolen_Days", noone)
    var _stolen_days_stamp = ds_map_find_value_ext(data, "Stolen_Days_Timestamp", noone)
    __dsDebuggerMapDestroy(data)

    data = noone
    data = __dsDebuggerMapCreate()
    ds_map_add(data, "arrayPosition", scr_inventory_get_weapon_number(name))
    is_new = true
    main_parametr = __dsDebuggerListCreate()
    ds_map_add_list(data, "second_damage", __dsDebuggerListCreate())
    ds_map_add_list(data, "general_resists", __dsDebuggerListCreate())
    ds_map_add_list(data, "physical_resists", __dsDebuggerListCreate())
    ds_map_add_list(data, "nature_resists", __dsDebuggerListCreate())
    ds_map_add_list(data, "magic_resists", __dsDebuggerListCreate())
    ds_map_add_list(data, "chance_and_resist", __dsDebuggerListCreate())
    ds_map_add_list(data, "combat", __dsDebuggerListCreate())
    ds_map_add_list(data, "magic", __dsDebuggerListCreate())
    ds_map_add_list(data, "restoration", __dsDebuggerListCreate())
    ds_map_add_list(data, "stealth", __dsDebuggerListCreate())

    empty = false
    scr_inventory_weapon_get_params()
    var num = ds_map_find_value(data, "arrayPosition")
    slotmap = __dsDebuggerMapCreate()
    common = __dsDebuggerMapCreate()
    rare = __dsDebuggerMapCreate()
    effect = __dsDebuggerMapCreate()
    scr_weapon_prefix_generation("Lifesteal", "Weapon", 2, 5);
    scr_weapon_prefix_generation("Manasteal", "Weapon", 2, 5);
    scr_weapon_prefix_generation("Bleeding_Chance", "Weapon", 2, 5);
    scr_weapon_prefix_generation("Daze_Chance", "Weapon", 2, 5);
    scr_weapon_prefix_generation("Stun_Chance", "Weapon", 2, 5);
    scr_weapon_prefix_generation("Knockback_Chance", "Weapon", 2, 5);
    scr_weapon_prefix_generation("Fire_Damage", "Weapon", 2, 2);
    scr_weapon_prefix_generation("Frost_Damage", "Weapon", 2, 1);
    scr_weapon_prefix_generation("Poison_Damage", "Weapon", 2, 1);
    scr_weapon_prefix_generation("Shock_Damage", "Weapon", 2, 1);
    scr_weapon_prefix_generation("Caustic_Damage", "Weapon", 2, 1);
    scr_weapon_prefix_generation("HP", "Armor", 2, 3);
    scr_weapon_prefix_generation("MP", "Armor", 2, 3);
    scr_weapon_prefix_generation("Health_Restoration", "Armor", 2, 4);
    scr_weapon_prefix_generation("MP_Restoration", "Armor", 2, 2);
    scr_weapon_prefix_generation("Weapon_Damage", "Weapon", 2, 4);
    scr_weapon_prefix_generation("Armor_Damage", "Weapon", 2, 10);
    scr_weapon_prefix_generation("Bodypart_Damage", "Weapon", 2, 10);
    scr_weapon_prefix_generation("Magic_Power", "Weapon", 2, 4);
    scr_weapon_prefix_generation("Skills_Energy_Cost", "Weapon", 2, -4);
    scr_weapon_prefix_generation("Spells_Energy_Cost", "Weapon", 2, -4);
    scr_weapon_prefix_generation("Cooldown_Reduction", "Weapon", 2, -4);
    scr_weapon_prefix_generation("PRR", "Weapon", 1, 3);
    scr_weapon_prefix_generation("Block_Power", "all", 1, 3);
    scr_weapon_prefix_generation("EVS", "Armor", 1, 2);
    scr_weapon_prefix_generation("CTA", "Weapon", 1, 3);
    scr_weapon_prefix_generation("Hit_Chance", "Weapon", 1, 2);
    scr_weapon_prefix_generation("CRT", "Weapon", 1, 3);
    scr_weapon_prefix_generation("CRTD", "Weapon", 1, 8);
    scr_weapon_prefix_generation("FMB", "Weapon", 1, -2);
    scr_weapon_prefix_generation("Armor_Piercing", "Weapon", 1, 4);
    scr_weapon_prefix_generation("Fortitude", "Armor", 1, 5);
    scr_weapon_prefix_generation("Healing_Received", "Armor", 1, 10);
    scr_weapon_prefix_generation("Slashing_Resistance", "Armor", 1, 8);
    scr_weapon_prefix_generation("Piercing_Resistance", "Armor", 1, 8);
    scr_weapon_prefix_generation("Blunt_Resistance", "Armor", 1, 8);
    scr_weapon_prefix_generation("Rending_Resistance", "Armor", 1, 8);
    scr_weapon_prefix_generation("Unholy_Resistance", "Armor", 1, 8);
    scr_weapon_prefix_generation("Stun_Resistance", "Armor", 1, 8);
    scr_weapon_prefix_generation("Knockback_Resistance", "Armor", 1, 8);
    scr_weapon_prefix_generation("Bleeding_Resistance", "Armor", 1, 8);
    scr_weapon_prefix_generation("Physical_Resistance", "Armor", 1, 3);
    scr_weapon_prefix_generation("Nature_Resistance", "Armor", 1, 5);
    scr_weapon_prefix_generation("Magic_Resistance", "Armor", 1, 5);
    scr_weapon_prefix_generation("Pain_Resistance", "Armor", 1, 5);
    Common = (1 << 0)
    Uncommon = (2 << 0)
    Rare = (3 << 0)
    Epic = (4 << 0)
    Curse = (5 << 0)
    Unique = (6 << 0)
    Treasure = (7 << 0)
    var tech = __dsDebuggerMapCreate()
    ds_map_add(tech, string(Common), 16777215)
    ds_map_add(tech, string(Uncommon), make_colour_rgb(89, 219, 76))
    ds_map_add(tech, string(Rare), make_colour_rgb(76, 127, 255))
    ds_map_add(tech, string(Epic), make_colour_rgb(255, 183, 43))
    ds_map_add(tech, string(Curse), make_colour_rgb(130, 72, 88))
    ds_map_add(tech, string(Unique), make_colour_rgb(130, 72, 188))
    ds_map_add(tech, string(Treasure), make_colour_rgb(229, 193, 85))

    quality = Curse

    var common_chars = 0
    var rare_chars = 0
    identified = true
    key = ""
    var char = 0
    switch quality
    {
        case Common:
            identified = true
            break
        case Uncommon:
            if scr_chance_value(50)
                common_chars = 1
            else
                rare_chars = 1
            identified = false
            break
        case Rare:
            repeat (2)
            {
                if scr_chance_value(50)
                    common_chars += 1
                else
                    rare_chars += 1
            }
            identified = false
            break
        case Curse:
        case Epic:
            repeat (2)
            {
                if scr_chance_value(50)
                    common_chars = 1
                else
                    rare_chars = 1
            }
            identified = 0
            break
    }

    n = 0
    scr_weapon_generation_prefix_search(common_chars, common)
    scr_weapon_generation_prefix_search(rare_chars, rare)
    if (n == 0)
        ds_map_add(data, "key", key)
    ds_map_add(data, "Suffix", (string(quality) + " " + type))
    ds_map_add(data, "Colour", ds_map_find_value(tech, string(quality)))
    ds_map_add(data, "LVL", LVL)
    ds_map_add(data, "Num", num)
    curse_list = __dsDebuggerListCreate()
    ds_map_add(data, "cursedQuality", noone)

    // Apply the curse
    var _key = script_get_name(_eff)
    ds_list_add(curse_list, _key)
    ds_list_add(curse_list, _eff)
    if instance_exists(o_player)
    {
        script_execute(_eff)
        scr_atr_calc(o_player)
    }

    ds_map_add(data, ds_list_find_value(curse_list, 2), ds_list_find_value(curse_list, 3))
    ds_map_replace(data, "is_cursed", true)
    if ((common_chars + rare_chars) == 1)
        ds_map_replace(data, "cursedQuality", Uncommon)
    else
        ds_map_replace(data, "cursedQuality", Rare)

    ds_map_add_list(data, "Curse", curse_list)
    ds_map_add(data, "identified", identified)
    ds_map_add(data, "quality", quality)

    // Release memory
    __dsDebuggerMapDestroy(effect)
    effect = noone
    __dsDebuggerMapDestroy(common)
    common = noone
    __dsDebuggerMapDestroy(slotmap)
    slotmap = noone
    __dsDebuggerMapDestroy(rare)
    rare = noone
    __dsDebuggerMapDestroy(tech)
    tech = noone

    // Refresh the sprite?
    event_user(1)

    // Recovery related information
    if (_is_quest != noone)
        ds_map_replace(data, "is_quest_item", _is_quest)
    if (_has_owner != noone)
        scr_item_crime_transfer(_has_owner, _town, _stolen_days, _stolen_days_stamp)

    // Refresh the text panel of the weapon
    event_user(7)

    // Identify the weapon
    identified = true
    ds_map_replace(data, "identified", identified)
    sh_diss = 200

    // Randomly reduce the duration
    ds_map_replace(data, "MaxDuration", max_dur)
    ds_map_replace(data, "Duration", max(1, (dur - max_dur * (random_range(0.15, 0.5)))))
}