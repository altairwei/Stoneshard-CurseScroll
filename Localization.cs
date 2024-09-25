
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
                {ModLanguage.English, "A scroll that transfers a curse from one item to another."},
                {ModLanguage.Chinese, "一种可以将~r~诅咒~/~从一个物品~lg~转移~/~到另一个物品的卷轴。"}
            },
            new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "l'Owcrey has acquired several cursed items from you, and after much study he has finally figured out how the curse works. This scroll is able to absorb power from an existing cursed item, and can then transfer that power, along with the curse, into another item, however inevitably the original item will be completely broken into pieces. But it was undeniably a great invention!"},
                {ModLanguage.Chinese, "埃欧科里从你手里收购了好几件诅咒物品，经过潜心研究，他终于搞懂了诅咒运行的原理。这种卷轴能够从已有的受诅咒物品中吸取力量，然后可以将这种力量连同诅咒一起转移到另一个物品中，然而不可避免的是原有物品会完全化为齑粉。但不可否认的是，这是一个伟大的发明！"}
            }
        );
    }
}