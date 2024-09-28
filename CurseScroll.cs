// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

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
            new MslEvent("gml_Object_o_inv_scroll_curse_Alarm_0.gml", EventType.Alarm, 0)
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
            new MslEvent("gml_Object_o_loot_scroll_curse_Create_0.gml", EventType.Create, 0)
        );

        // Let l'Owcrey sell these scroll

        Msl.LoadGML("gml_Object_o_npc_enchanter_Create_0")
            .MatchFrom("gold_k = irandom_range(1000, 1500)")
            .InsertAbove(@"
if (scr_dialogue_complete(""cursescroll_ready_to_sell""))
    ds_list_add(selling_loot_object, o_inv_scroll_curse, 25)
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

        ds_list_clear(selling_loot_object)
        ds_list_add(selling_loot_object, o_inv_treatise_geo3, 20, o_inv_treatise_pyro3, 20, o_inv_treatise_electro3, 20, o_inv_treatise_magic3, 20, o_inv_scroll_curse, 20)
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

        Localization.ItemsPatching();
        Localization.DialogLinesPatching();

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
}
