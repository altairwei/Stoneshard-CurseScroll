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
            new MslEvent("gml_Object_o_skill_absorb_curse_Alarm_0.gml", EventType.Alarm, 0),
            new MslEvent("gml_Object_o_skill_absorb_curse_Other_20.gml", EventType.Other, 10),
            new MslEvent("gml_Object_o_skill_absorb_curse_Other_11.gml", EventType.Other, 1)
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
            new MslEvent("gml_Object_o_skill_transfer_curse_Other_20.gml", EventType.Other, 10),
            new MslEvent("gml_Object_o_skill_transfer_curse_Other_11.gml", EventType.Other, 1)
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
            new MslEvent("gml_Object_o_inv_scroll_curse_Create_0.gml", EventType.Create, 0)
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
            .InsertAbove("ds_list_add(selling_loot_object, o_inv_scroll_curse, 25)")
            .Save();

        Localization.ItemsPatching();

        // Delete Me!
        Msl.LoadGML("gml_Object_o_player_KeyPress_115") // F4
            .MatchAll()
            .InsertBelow(ModFiles, "generate_cursed_item.gml")
            .Save();
    }
}
