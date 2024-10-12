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
    public override string Version => "1.0.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        Msl.AddFunction(ModFiles.GetCode("mod_weapon_apply_curse.gml"), "mod_weapon_apply_curse");

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

        Msl.LoadGML("gml_Object_o_npc_enchanter_Create_0")
            .MatchFrom("gold_k = irandom_range(1000, 1500)")
            .InsertAbove(@"
if (scr_dialogue_complete(""cursescroll_ready_to_sell""))
    ds_list_add(selling_loot_object_persistence, o_inv_scroll_curse, 2)
Stock_Refill_Time = 48")
            .Save();

        UndertaleGameObject o_trade_inventory = Msl.GetObject("o_trade_inventory");
        o_trade_inventory.ApplyEvent(ModFiles,
        new MslEvent("gml_Object_o_trade_inventory_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_trade_inventory_Other_11.gml", EventType.Other, 11),
            new MslEvent("gml_Object_o_trade_inventory_Destroy_0.gml", EventType.Destroy, 0)
        );

        Msl.LoadGML("gml_Object_o_npc_enchanter_Other_23")
            .MatchFrom("event_inherited()")
            .InsertAbove(@"
var _timestamp = scr_npc_get_global_info(""make_curse_scroll_timestamp"")
var _daysPassed = scr_timeGetPassed(_timestamp, ""days"")
if (scr_dsMapFindValue(data, ""num_of_cursed_item"", 0) >= 3 && _daysPassed >= 1 && !scr_dialogue_complete(""cursescroll_ready_to_sell""))
{
    ori_dialog_id = dialog_id
    dialog_id = de2_dialog_open(""curse_scroll_lowcrey.de2"")
    topic = ""topicScroll""
    scr_npc_start_dialog()
}
")
            .Save();

        Msl.AddFunction(
            name: "scr_curseScroll_lowcreyUpdateInventory",
            codeAsString: @"function scr_curseScroll_lowcreyUpdateInventory()
{
    with (o_npc_enchanter)
    {
        dialog_id = ori_dialog_id

        is_execute = false
        var _globalData = scr_globaltile_get(id_name, village_xy[0], village_xy[1])
        ds_list_clear(ds_map_find_value(_globalData, ""trade_list""))
        var _timestamp = scr_timeGetTimestamp()
        scr_npc_set_global_info(""timestamp"", _timestamp)

        ds_list_clear(selling_loot_object_persistence)
        ds_list_add(selling_loot_object_persistence, o_inv_scroll_curse, 2)
    }

    scr_dialogue_complete(""cursescroll_ready_to_sell"", true)
    scr_trade_open()
}");

        // FIXME: work around for `self.every_stock_update()`
        Msl.LoadAssemblyAsString("scr_curseScroll_lowcreyUpdateInventory")
            .MatchFrom("call.i ds_list_add(argc=11)")
            .InsertBelow(@"popz.v
call.i @@This@@(argc=0)
push.v builtin.every_stock_update
callv.v 0")
            .Save();

        Msl.InjectTableConsumableParameters(
            metaGroup: Msl.ConsumParamMetaGroup.MAPSSCROLLSBOOKS,
            id: "scroll_curse",
            Cat: Msl.ConsumParamCategory.scroll,
            Material: Msl.ConsumParamMaterial.paper,
            Weight: Msl.ConsumParamWeight.Light,
            tags: Msl.ConsumParamTags.special,
            Price: 500
        );

        // Add Several Curse Scroll to Witch's Container

        Msl.LoadGML("gml_Object_o_whitchousecontainer02_Other_10")
            .MatchFrom("            scr_inventory_add_item(scr_get_scroll(0))")
            .InsertBelow(@"
        repeat random_range(1, 3)
            scr_inventory_add_item(o_inv_scroll_curse)")
            .Save();

        // Let some enemies drop cure scrolls

        // Msl.AddNewEvent("o_skeleton_monk", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 1)", EventType.Destroy, 0);
        // Msl.AddNewEvent("o_skeleton_priest", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 2)", EventType.Destroy, 0);
        // Msl.AddNewEvent("o_ghast", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 3)", EventType.Destroy, 0);
        // Msl.AddNewEvent("o_skeleton_highpriest", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 3)", EventType.Destroy, 0);
        // Msl.AddNewEvent("o_ghast_accursed", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 3)", EventType.Destroy, 0);
        // Msl.AddNewEvent("o_ghast_elder", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 3)", EventType.Destroy, 0);

        // FIXME: don't touch o_necromancer_boss which will be modified by Necromancy
        // Msl.AddNewEvent("o_necromancer_boss", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 2)", EventType.Destroy, 0);
        // Msl.AddNewEvent("o_necromancer_boss_staff", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 2.5)", EventType.Destroy, 0);
        // Msl.AddNewEvent("o_necromancer_ritualist", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 3)", EventType.Destroy, 0);
        // Msl.AddNewEvent("o_necromancer_wraithbinder", "event_inherited()\nscr_loot(o_loot_scroll_curse, x, y, 3)", EventType.Destroy, 0);

        Msl.LoadGML("gml_Object_o_proselyte_adept_Destroy_0")
            .MatchAll()
            .InsertBelow("scr_loot(scr_get_scroll(1), x, y, 1)")
            .Save();

        Msl.LoadGML("gml_Object_o_proselyte_toller_Destroy_0")
            .MatchAll()
            .InsertBelow("scr_loot(scr_get_scroll(1), x, y, 1)")
            .Save();

        Msl.LoadGML("gml_Object_o_proselyte_hierarch_Destroy_0")
            .MatchAll()
            .InsertBelow("scr_loot(o_loot_scroll_curse, x, y, 2)")
            .Save();

        Msl.LoadGML("gml_Object_o_proselyte_apostate_Destroy_0")
            .MatchAll()
            .InsertBelow("scr_loot(o_loot_scroll_curse, x, y, 2.5)")
            .Save();

        Msl.LoadGML("gml_Object_o_proselyte_abomination_Destroy_0")
            .MatchAll()
            .InsertBelow("scr_loot(o_loot_scroll_curse, x, y, 2.5)")
            .Save();

        Msl.LoadGML("gml_Object_o_proselyte_matriarch_Destroy_0")
            .MatchAll()
            .InsertBelow("scr_loot(o_loot_scroll_curse, x, y, 2.5)")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_loot_cabinetCatacombs")
            .MatchFrom("                    scr_inventory_add_item(choose(3087, 3086, 3085))")
            .ReplaceBy(@"                {
                    if scr_chance_value(10)
                        scr_inventory_add_item(o_inv_scroll_curse)
                    else
                        scr_inventory_add_item(choose(o_inv_scroll_disenchant, o_inv_scroll_enchant, o_inv_scroll_identification))
                }")
            .MatchFrom("                    scr_inventory_add_item(choose(3087, 3086, 3085))")
            .ReplaceBy(@"                {
                    if scr_chance_value(10)
                        scr_inventory_add_item(o_inv_scroll_curse)
                    else
                        scr_inventory_add_item(choose(o_inv_scroll_disenchant, o_inv_scroll_enchant, o_inv_scroll_identification))
                }")
            .MatchFrom("                    scr_inventory_add_item(choose(3087, 3086, 3085))")
            .ReplaceBy(@"                {
                    if scr_chance_value(10)
                        scr_inventory_add_item(o_inv_scroll_curse)
                    else
                        scr_inventory_add_item(choose(o_inv_scroll_disenchant, o_inv_scroll_enchant, o_inv_scroll_identification))
                }")
            .MatchFrom("                    scr_inventory_add_item(choose(3087, 3086, 3085))")
            .ReplaceBy(@"                {
                    if scr_chance_value(10)
                        scr_inventory_add_item(o_inv_scroll_curse)
                    else
                        scr_inventory_add_item(choose(o_inv_scroll_disenchant, o_inv_scroll_enchant, o_inv_scroll_identification))
                }")
            .MatchFrom("                    scr_inventory_add_item(choose(3087, 3086, 3085))")
            .ReplaceBy(@"                {
                    if scr_chance_value(10)
                        scr_inventory_add_item(o_inv_scroll_curse)
                    else
                        scr_inventory_add_item(choose(o_inv_scroll_disenchant, o_inv_scroll_enchant, o_inv_scroll_identification))
                }")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_loot_chestRemoteCatacombs")
            .MatchFrom("        scr_inventory_add_item(choose(3050, 3086))")
            .InsertBelow(@"    if scr_chance_value(10)
        scr_inventory_add_item(o_inv_scroll_curse)")
            .Save();

        // Insert Localization and Curse List

        Localization.ItemsPatching();
        Localization.DialogLinesPatching();
        Localization.CurseTextPatching();
        CurseList.CurseFunctionPatching();

        // Path text color
        Msl.LoadGML("gml_GlobalScript_scr_colorTextColorsMap")
            .MatchFrom(@"        ds_map_add(global.colorTextMap, ""~c~"", make_colour_rgb(115, 192, 222))")
            .InsertBelow(@"        ds_map_add(global.colorTextMap, ""~cursed~"", make_colour_rgb(130, 72, 88))")
            .Save();

        // Delete Me!
        Msl.LoadGML("gml_Object_o_player_KeyPress_115") // F4
            .MatchAll()
            .InsertBelow(ModFiles, "generate_cursed_item.gml")
            .Save();
    }

    private static void ExportTable(string table)
    {
        DirectoryInfo dir = new("ModSources/CurseScroll/tmp");
        if (!dir.Exists) dir.Create();
        List<string>? lines = ModLoader.GetTable(table);
        if (lines != null)
        {
            File.WriteAllLines(
                Path.Join(dir.FullName, Path.DirectorySeparatorChar.ToString(), table + ".tsv"),
                lines.Select(x => string.Join('\t', x.Split(';')))
            );
        }
    }
}
