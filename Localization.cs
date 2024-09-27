
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
                {ModLanguage.English, "l'Owcrey has acquired several cursed items from you, and after much study he has invented a scroll capable of transferring the curse."},
                {ModLanguage.Chinese, "埃欧科里从你手里收购了好几件诅咒物品，经过潜心研究，他发明了一种能够转移诅咒的卷轴。"}
            }
        );
    }

    public static void DialogLinesPatching()
    {
        Msl.InjectTableDialogLocalization(
            new LocalizationSentence(
                "cursescroll_ready_to_intro",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Ah, you're here! Those cursed items you sold me—I've studied them thoroughly, and I've made some significant progress recently. #I've crafted a scroll that can siphon the power from an existing cursed item and transfer both the power and the curse to another item. #However, the original object will inevitably be reduced to dust."},
                    {ModLanguage.Chinese, "你来了！你卖给我的那些受诅咒物品，我仔细研究了一番，最近有了重大进展。#我制作了一种卷轴，能够从已有的受诅咒物品中吸取力量，并将这种力量连同诅咒一起转移到另一个物品上。#不过，原来的物品不可避免地会完全化为齑粉。"}
                }
            ),
            new LocalizationSentence(
                "cursescroll_ready_to_intro_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I can't believe you've mastered the mystical arts of witches! It's truly remarkable."},
                    {ModLanguage.Chinese, "你竟然掌握了女巫的神秘技艺！真是了不起。"}
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
            )
        );
    }
}