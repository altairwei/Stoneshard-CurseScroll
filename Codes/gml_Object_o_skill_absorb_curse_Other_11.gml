if (interact_id == noone)
    interact_id = instance_position(global.guiMouseX, global.guiMouseY, o_inv_slot)
var _success = false
var _curse_eff = noone
with (interact_id)
{
    if (image_alpha == 1)
    {
        // Absorb the curse then destroy the item
        var _curse_list = ds_map_find_value(data, "Curse")
        _curse_eff = ds_list_find_value(_curse_list, 1)
        total_destroy = true
        instance_destroy()

        if inmouse
        {
            scr_guiInteractiveEventPerform(id, 1)
            scr_guiInteractiveEventPerform(id, 0)
        }
        _success = true
        
    }
}
if (_success && _curse_eff != noone)
{
    with (parent)
    {
        scr_actionsLog("useItem", [scr_id_get_name(o_player), log_text, ds_map_find_value(data, "Name")])
        sh_diss = 200

        curse_eff = _curse_eff
        skill = o_skill_transfer_curse
    }
    event_user(0)
}
interact_id = noone
