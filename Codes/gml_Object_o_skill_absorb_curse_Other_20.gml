event_inherited()
with (o_inv_slot)
{
    if (owner.object_index != o_trade_inventory && is_weapon && ds_map_find_value_ext(data, "identified", false) && ds_map_find_value_ext(data, "is_cursed", false) && ds_map_find_value_ext(data, "quality", noone) == (5 << 0))
        image_alpha = 1
    else
        image_alpha = 0.25
    can_pick = false
}
