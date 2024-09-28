event_inherited()
// Count how many cursed items l'Owcrey has
if (!is_calculated && owner.object_index == o_npc_enchanter)
{
    var _cursed_num = 0
    for (var i = 0; i < ds_list_size(loot_list); i++)
    {
        if (owner.object_index == o_npc_enchanter)
        {
            with (ds_list_find_value(loot_list, i))
            {
                if (scr_dsMapFindValue(data, "is_cursed", false))
                    _cursed_num++
            }
        }
    }

    num_of_cursed_item = _cursed_num
    is_calculated = true
}

