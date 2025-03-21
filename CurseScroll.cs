// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using System;
using System.IO;

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace CurseScroll;
public class CurseScroll : Mod
{
    public override string Author => "Altair Wei";
    public override string Name => "Curse Scroll";
    public override string Description => "l'Owcrey at the Rotten Willow now sells a scroll that transfers curses.";
    public override string Version => "1.1.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        Msl.AddFunction(ModFiles.GetCode("mod_weapon_apply_curse.gml"), "mod_weapon_apply_curse");

        Msl.AddMenu(
            "Curse Scroll",
            new UIComponent(
                name: "Enable new curses", associatedGlobal: "add_new_curses",
                UIComponentType.CheckBox, 0, true)
        );

        // Transfer curse to new item

        UndertaleGameObject o_skill_transfer_curse = Msl.AddObject(
            name: "o_skill_transfer_curse",
            spriteName: "sprite1",
            parentName: "o_weapon_skills",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_transfer_curse.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_skill_transfer_curse_Alarm_0.gml", EventType.Alarm, 0),
            new MslEvent("gml_Object_o_skill_transfer_curse_Other_11.gml", EventType.Other, 11),
            new MslEvent("gml_Object_o_skill_transfer_curse_Other_20.gml", EventType.Other, 20)
        );

        // Absorb curse from an existed item
        UndertaleGameObject o_skill_absorb_curse = Msl.AddObject(
            name: "o_skill_absorb_curse",
            spriteName: "sprite1",
            parentName: "o_weapon_skills",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_absorb_curse.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_skill_absorb_curse_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_skill_absorb_curse_Alarm_0.gml", EventType.Alarm, 0),
            new MslEvent("gml_Object_o_skill_absorb_curse_Other_11.gml", EventType.Other, 11),
            new MslEvent("gml_Object_o_skill_absorb_curse_Other_20.gml", EventType.Other, 20)
        );

        // Create Curse Scroll

        UndertaleGameObject o_inv_scroll_curse = Msl.AddObject(
            name: "o_inv_scroll_curse",
            spriteName: "s_inv_scroll",
            parentName: "o_inv_scroll_parent",
            isVisible: true,
            isPersistent: true,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_inv_scroll_curse.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_inv_scroll_curse_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_inv_scroll_curse_Alarm_0.gml", EventType.Alarm, 0),
            new MslEvent("gml_Object_o_inv_scroll_curse_Other_17.gml", EventType.Other, 17)
        );

        UndertaleGameObject o_loot_scroll_curse = Msl.AddObject(
            name: "o_loot_scroll_curse",
            spriteName: "s_loot_scroll",
            parentName: "o_loot_scroll_parent",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Box
        );

        o_loot_scroll_curse.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_loot_scroll_curse_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_loot_scroll_curse_Other_12.gml", EventType.Other, 12)
        );

        // Let l'Owcrey sell these scroll

        Msl.LoadGML("gml_Object_o_npc_lowcrey_Other_19")
            .MatchAll()
            .InsertBelow(@"
if (scr_dialogue_complete(""cursescroll_ready_to_sell""))
    ds_list_add(selling_loot_object, o_inv_scroll_curse, irandom_range(1, 3))")
            .Save();

        Msl.LoadGML("gml_Object_o_trade_inventory_Create_0")
            .MatchAll()
            .InsertBelow(ModFiles, "gml_Object_o_trade_inventory_Create_0.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_trade_inventory_Other_11")
            .MatchAll()
            .InsertBelow(ModFiles, "gml_Object_o_trade_inventory_Other_11.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_trade_inventory_Destroy_0")
            .MatchAll()
            .InsertAbove(ModFiles, "gml_Object_o_trade_inventory_Destroy_0.gml")
            .Save();

        Msl.AddFunction(
            name: "scr_curseScroll_lowcreyUpdateInventory",
            codeAsString: @"function scr_curseScroll_lowcreyUpdateInventory()
{
    with (o_npc_lowcrey)
    {
        scr_npc_restock(true);
        var _timestamp = scr_timeGetTimestamp();
        scr_npc_set_global_info(""restock_timestamp"", _timestamp);
    }

    scr_dialogue_complete(""cursescroll_ready_to_sell"", true)
    scr_trade_open()
}");

        Msl.InjectTableItemStats(
            id: "scroll_curse",
            Price: 500,
            Cat: Msl.ItemStatsCategory.scroll,
            Material: Msl.ItemStatsMaterial.paper,
            Weight: Msl.ItemStatsWeight.Light,
            tags: Msl.ItemStatsTags.special
            
        );

        // Add Several Curse Scroll to Witch's Container

        Msl.LoadGML("gml_Object_o_whitchousecontainer02_Other_10")
            .MatchFrom("scr_inventory_add_item(scr_get_scroll")
            .InsertBelow(@"
        repeat random_range(1, 3)
            scr_inventory_add_item(o_inv_scroll_curse)")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_loot_cabinetCatacombs")
            .MatchFrom("scr_inventory_add_item(choose(o_inv_scroll_enchant, o_inv_scroll_identification))")
            .ReplaceBy(@"{
                if scr_chance_value(10)
                    scr_inventory_add_item(o_inv_scroll_curse)
                else
                    scr_inventory_add_item(choose(o_inv_scroll_enchant, o_inv_scroll_identification))
            }")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_loot_chestRemoteCatacombs")
            .MatchFrom("scr_inventory_add_item(choose(o_inv_bottle, o_inv_scroll_enchant))")
            .InsertBelow(@"    if scr_chance_value(10)
        scr_inventory_add_item(o_inv_scroll_curse)")
            .Save();

        // Insert Localization and Curse List

        Localization.ItemsPatching();
        Localization.DialogLinesPatching();
        Localization.CurseTextPatching();
        CurseList.CurseFunctionPatching();

        // Patch dialogue
        Msl.AddNewEvent("o_curse_scrull_initializer", "", EventType.Create, 0);
        Msl.LoadGML(Msl.EventName("o_curse_scrull_initializer", EventType.Create, 0))
            .MatchAll()
            .InsertBelow(ModFiles, "lowcrey_dialog_update.gml")
            .Save();

        // Path text color
        Msl.LoadGML("gml_GlobalScript_scr_colorTextColorsMap")
            .MatchFrom(@"        ds_map_add(global.colorTextMap, ""~c~"", make_colour_rgb(115, 192, 222))")
            .InsertBelow(@"        ds_map_add(global.colorTextMap, ""~cursed~"", make_colour_rgb(130, 72, 88))")
            .Save();

        // Delete Me!
        // Msl.LoadGML("gml_Object_o_player_KeyPress_115") // F4
        //     .MatchAll()
        //     .InsertBelow(ModFiles, "generate_cursed_item.gml")
        //     .Save();
    }
}
