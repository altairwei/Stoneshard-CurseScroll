event_inherited()
curse_list = scr_dsMapFindValue(data, "Curse", noone)
skill = o_skill_absorb_curse
if (curse_list != noone)
{
    scr_actionsLogUpdate("~y~curse_list!=noone~/~")
    skill = o_skill_transfer_curse
}
scr_consum_atr("scroll_curse")
base_index = 3

