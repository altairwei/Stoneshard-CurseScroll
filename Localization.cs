
using ModShardLauncher;
using ModShardLauncher.Mods;

namespace CurseScroll;

public class Localization
{
    public static void ItemsPatching()
    {
        Msl.InjectTableItemLocalization(
            "scroll_curse",
            new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Curse Scroll"},
                {ModLanguage.Chinese, "诅咒卷轴"}
            },
            new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "A scroll that ~lg~transfers~/~ a ~cursed~curse~/~ from one item to another."},
                {ModLanguage.Chinese, "一种可以将~cursed~诅咒~/~从一个物品~lg~转移~/~到另一个物品的卷轴。"}
            },
            new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "This scroll can absorb the power from an existing cursed item and transfer both the power and the curse to another item. However, the original object will inevitably be reduced to dust."},
                {ModLanguage.Chinese, "这种卷轴能够从已有的受诅咒物品中吸取力量，并将这种力量连同诅咒一起转移到另一个物品上。不过，原来的物品不可避免地会完全化为齑粉。"}
            }
        );
    }

    public static void DialogLinesPatching()
    {
        Msl.InjectTableDialogLocalization(
            new LocalizationSentence(
                "cursescroll_ready_to_intro",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Ah, you're here! Those cursed items you sold me—I've studied them thoroughly, and I've made some significant progress recently. #I've crafted a scroll that can absorb the power from an existing cursed item and transfer both the power and the curse to another item. #However, the original object will inevitably be reduced to dust."},
                    {ModLanguage.Chinese, "你来了！你卖给我的那些受诅咒物品，我仔细研究了一番，最近有了重大进展。#我制作了一种卷轴，能够从已有的受诅咒物品中吸取力量，并将这种力量连同诅咒一起转移到另一个物品上。#不过，原来的物品不可避免地会完全化为齑粉。"}
                }
            ),
            new LocalizationSentence(
                "cursescroll_ready_to_sell",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "If you're interested in such a scroll, I could supply a limited amount, though the price will be steep. The materials required for its creation are exceedingly rare, you see."},
                    {ModLanguage.Chinese, "如果你对这种卷轴感兴趣的话，我可以少量供应给你。不过价格会很高，毕竟制作这卷轴所需的原材料极其稀少。"}
                }
            ),
            new LocalizationSentence(
                "cursescroll_ready_to_sell_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "OK! I'll take as much as you have!"},
                    {ModLanguage.Chinese, "行！你有多少我要多少！"}
                }
            ),
            new LocalizationSentence(
                "cursescroll_ready_to_sell_refuse_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I don't need this stuff."},
                    {ModLanguage.Chinese, "我不需要这些玩意儿。"}
                }
            )
        );
    }

    public static void CurseTextPatching()
    {
        List<string> idlist = new List<string>();

        string id = "Curse_of_Nihility";
        string text_en = "Curse of Nihility";
        string text_zh = "虚无诅咒";
        idlist.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 11)));

        id = "Curse_of_Craze";
        text_en = "Curse of Craze";
        text_zh = "狂热诅咒";
        idlist.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 11)));

        id = "Curse_of_Scabies";
        text_en = "Curse of Scabies";
        text_zh = "癞皮诅咒";
        idlist.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 11)));

        id = "Curse_of_Blindness";
        text_en = "Curse of Blindness";
        text_zh = "目盲诅咒";
        idlist.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 11)));

        id = "Curse_of_Thirsty";
        text_en = "Curse of Thirsty";
        text_zh = "干渴诅咒";
        idlist.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 11)));

        id = "Curse_of_Wrath";
        text_en = "Curse of Wrath";
        text_zh = "暴怒诅咒";
        idlist.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 11)));

        id = "Curse_of_Evermind";
        text_en = "Curse of Evermind";
        text_zh = "迷醉诅咒";
        idlist.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 11)));

        id = "Curse_of_Inversion";
        text_en = "Curse of Inversion";
        text_zh = "相噬诅咒";
        idlist.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 11)));

        string curse_end = ";" + string.Concat(Enumerable.Repeat("curse_name_end;", 14));

        List<string> curse_table = Msl.ThrowIfNull(ModLoader.GetTable("gml_GlobalScript_table_curses"));
        curse_table.InsertRange(curse_table.IndexOf(curse_end), idlist);
        ModLoader.SetTable(curse_table, "gml_GlobalScript_table_curses");
    }
}