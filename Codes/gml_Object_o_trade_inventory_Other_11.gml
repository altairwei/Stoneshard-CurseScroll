// Count how many cursed items l'Owcrey has
if (!is_calculated && owner.object_index == o_npc_lowcrey)
{
    var _cursed_num = 0
    for (var i = 0; i < ds_list_size(loot_list); i++)
    {
        if (owner.object_index == o_npc_lowcrey)
        {
            with (ds_list_find_value(loot_list, i))
            {
                if (ds_map_find_value_ext(data, "is_cursed", false))
                    _cursed_num++
            }
        }
    }

    num_of_cursed_item = _cursed_num
    is_calculated = true
}

