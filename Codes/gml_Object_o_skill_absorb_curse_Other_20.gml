event_inherited()
with (o_inv_slot)
{
    if (owner.object_index != o_trade_inventory && is_weapon && scr_dsMapFindValue(data, "identified", false) && scr_dsMapFindValue(data, "is_cursed", false) && scr_dsMapFindValue(data, "quality", noone) == (5 << 0))
        image_alpha = 1
    else
        image_alpha = 0.25
    can_pick = false
}
