event_inherited()
curse_list = scr_dsMapFindValue(data, "Curse", noone)
if (curse_list != noone)
    skill = o_skill_transfer_curse
else
    skill = o_skill_absorb_curse
