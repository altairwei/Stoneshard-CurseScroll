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

        curse_list = __dsDebuggerListCreate()
        for (var i = 0; i < ds_list_size(_curse_list); i++) {
            var item = ds_list_find_value(_curse_list, i)
            ds_list_add(curse_list, item)
        }

        ds_map_replace_list(data, "Curse", curse_list)
        skill = o_skill_transfer_curse

        ds_map_replace(data, "quality", (5 << 0))
        ds_map_replace(data, "is_cursed", true)
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
