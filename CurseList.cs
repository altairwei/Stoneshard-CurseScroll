using ModShardLauncher;
using ModShardLauncher.Mods;
using ModShardLauncher.Resources.Codes;

using UndertaleModLib;
using UndertaleModLib.Decompiler;
using UndertaleModLib.Models;
using UndertaleModLib.Scripting;

namespace CurseScroll;

public class CurseList
{
    public static void CurseFunctionPatching()
    {
        Msl.AddFunction(
            name: "gml_GlobalScript_Curse_of_Nihility",
            codeAsString: @"function Curse_of_Nihility()
{
    if (argument_count == 0)
    {
        scr_curse_add_value(""Cooldown_Reduction"", -10)
        scr_curse_add_value(""Health_Restoration"", -10)
    }
}");

        Msl.AddFunction(
            name: "gml_GlobalScript_Curse_of_Craze",
            codeAsString: @"function Curse_of_Craze()
{
    if (argument_count == 0)
    {
        scr_curse_add_value(""Miracle_Chance"", 10)
        scr_curse_add_value(""Spells_Energy_Cost"", 15)
    }
}");

        Msl.AddFunction(
            name: "gml_GlobalScript_Curse_of_Scabies",
            codeAsString: @"function Curse_of_Scabies()
{
    if (argument_count == 0)
    {
        scr_curse_add_value(""Physical_Resistance"", 10)
        scr_curse_add_value(""Skills_Energy_Cost"", 20)
    }
}");

        Msl.AddFunction(
            name: "gml_GlobalScript_Curse_of_Blindness",
            codeAsString: @"function Curse_of_Blindness()
{
    if (argument_count == 0)
    {
        scr_curse_add_value(""CTA"", 15)
        scr_curse_add_value(""VSN"", -1)
    }
}");
        
        Msl.AddFunction(
            name: "gml_GlobalScript_Curse_of_Thirsty",
            codeAsString: @"function Curse_of_Thirsty()
{
    if (argument_count == 0)
    {
        scr_curse_add_value(""Manasteal"", 20)
        scr_curse_add_value(""Thirst_Change"", 0.04)
    }
}");

        Msl.AddFunction(
            name: "gml_GlobalScript_Curse_of_Wrath",
            codeAsString: @"function Curse_of_Wrath()
{
    if (argument_count == 0)
    {
        scr_curse_add_value(""CRT"", 10)
        scr_curse_add_value(""EVS"", -10)
    }
}");

        Msl.AddFunction(
            name: "gml_GlobalScript_Curse_of_Evermind",
            codeAsString: @"function Curse_of_Evermind()
{
    if (argument_count == 0)
    {
        scr_curse_add_value(""CTA"", 15)
        scr_curse_add_value(""FMB"", 10)
    }
}");

        AddCurseTags();
    }

    public static void AddScript(string name)
    {
        string codeName = $"gml_GlobalScript_{name}";
        UndertaleCode code = DataLoader.data.Code.ByName(codeName);

        UndertaleScript scr = new UndertaleScript();
        scr.Name = DataLoader.data.Strings.MakeString(name);
        scr.Code = code;
        DataLoader.data.Scripts.Add(scr);
    }

    private static void AddCurseTags()
    {
        // 过载诅咒：+10%准度-10%精力恢复速度
        AddScript("Curse_of_Nihility");
        AddScript("Curse_of_Craze");
        AddScript("Curse_of_Scabies");
        AddScript("Curse_of_Blindness");
        AddScript("Curse_of_Thirsty");
        AddScript("Curse_of_Wrath");
        AddScript("Curse_of_Evermind");

        string txtCurseTags = @"
if (global.add_new_curses)
{
    asset_add_tags(Curse_of_Nihility, [""curse_eff"", ""Weapon"", ""Jewelry"", ""Armor""], asset_script)
    asset_add_tags(Curse_of_Craze, [""curse_eff"", ""Weapon"", ""Jewelry"", ""Armor""], asset_script)
    asset_add_tags(Curse_of_Scabies, [""curse_eff"", ""Armor""], asset_script)
    asset_add_tags(Curse_of_Blindness, [""curse_eff"", ""Armor""], asset_script)
    asset_add_tags(Curse_of_Thirsty, [""curse_eff"", ""Weapon"", ""Jewelry"", ""Armor""], asset_script)
    asset_add_tags(Curse_of_Wrath, [""curse_eff"", ""Weapon"", ""Jewelry"", ""Armor""], asset_script)
    asset_add_tags(Curse_of_Evermind, [""curse_eff"", ""Weapon"", ""Jewelry"", ""Armor""], asset_script)
}
else
{
    asset_clear_tags(Curse_of_Nihility, asset_script)
    asset_clear_tags(Curse_of_Craze, asset_script)
    asset_clear_tags(Curse_of_Scabies, asset_script)
    asset_clear_tags(Curse_of_Blindness, asset_script)
    asset_clear_tags(Curse_of_Thirsty, asset_script)
    asset_clear_tags(Curse_of_Wrath, asset_script)
    asset_clear_tags(Curse_of_Evermind, asset_script)
}";

        UndertaleGameObject ob = Msl.AddObject("o_curse_scrull_initializer", isPersistent: true);
        Msl.AddNewEvent(ob, txtCurseTags, EventType.Other, 5);
        // initializer in START room
        UndertaleRoom room = Msl.GetRoom("START");
        room.AddGameObject("Instances", ob);
    }

}