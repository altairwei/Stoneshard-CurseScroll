event_inherited()
curse_list = ds_map_find_value_ext(data, "Curse", noone)
if (curse_list != noone)
    skill = o_skill_transfer_curse
else
    skill = o_skill_absorb_curse

event_user(7)
