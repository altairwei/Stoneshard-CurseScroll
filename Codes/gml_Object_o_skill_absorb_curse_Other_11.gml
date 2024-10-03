if (interact_id == noone)
    interact_id = instance_position(global.guiMouseX, global.guiMouseY, o_inv_slot)
var _success = false
var _curse_list = noone
with (interact_id)
{
    if (image_alpha == 1)
    {
        // Absorb the curse then destroy the item
        _curse_list = scr_dsMapFindValue(data, "Curse", noone)

        if inmouse
        {
            scr_guiInteractiveEventPerform(id, 1)
            scr_guiInteractiveEventPerform(id, 0)
        }
        _success = true
        
    }
}
if (_success && _curse_list != noone)
{
    with (parent)
    {
        scr_actionsLog("useItem", [scr_id_get_name(o_player), log_text, ds_map_find_value(data, "Name")])
        sh_diss = 200

        // Absorb the curse from the existed item
        curse_list = __dsDebuggerListCreate()
        ds_list_copy(curse_list, _curse_list)
        ds_map_add_list(data, "Curse", curse_list)
        skill = o_skill_transfer_curse

        ds_map_replace(data, "quality", (5 << 0))
        ds_map_replace(data, "is_cursed", true)
        ds_map_replace(data, "Colour", make_colour_rgb(130, 72, 88))

        // Refresh information of the scroll
        event_user(1)
        event_user(7)
    }

    with (interact_id)
    {
        total_destroy = true
        instance_destroy()
    }

    event_user(0)
}
interact_id = noone
