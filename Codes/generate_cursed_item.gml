var place_x = scr_round_cell(o_player.x) + 13
var place_y = scr_round_cell(o_player.y) + 13

var types = choose("Copper Ring", "Copper Chain", "Crude Talisman", "Bow Talisman", "Bronze Ring With Amethyst", "Silver Ring", "Golden Ring")
with (scr_loot_drop(place_x, place_y, o_weapon_loot))
{
    ds_map_add(data, "arrayPosition", scr_inventory_get_weapon_number(types))
    sprite_index = scr_weapon_id("s_loot_")
    determined_quality = (5 << 0)
    ds_map_set(data, "identified", true)
    identified = true
    event_user(1)
}