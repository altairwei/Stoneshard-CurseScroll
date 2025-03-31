if (owner.object_index == o_npc_lowcrey)
{
    var _current_num_of_cursed_item = 0
    for (var i = 0; i < ds_list_size(loot_list); i++)
    {
        if (owner.object_index == o_npc_lowcrey)
        {
            with (ds_list_find_value(loot_list, i))
            {
                if (ds_map_find_value_ext(data, "is_cursed", false))
                    _current_num_of_cursed_item++
            }
        }
    }

    var _num_of_cursed_item = num_of_cursed_item
    with (owner)
    {
        if (_current_num_of_cursed_item > _num_of_cursed_item)
        {
            var _num = scr_npc_get_global_info("num_of_cursed_item")
            var _new_num = _num + (_current_num_of_cursed_item - _num_of_cursed_item)
            scr_npc_set_global_info("num_of_cursed_item", _new_num)
            if (_num < 3 && _new_num >= 3)
            {
                var _timestamp = scr_timeGetTimestamp()
                scr_npc_set_global_info("make_curse_scroll_timestamp", _timestamp)
            }
        }
    }
}
