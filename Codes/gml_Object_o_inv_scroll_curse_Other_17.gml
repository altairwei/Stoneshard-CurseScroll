event_inherited()
// Change the name of curse scroll
if (curse_list != noone)
{
    var _idName = ds_map_find_value(data, "idName")
    var _space = scr_actionsLogGetSpace()
    var _name = ds_map_find_value(global.consum_type, "scroll")

    var _cursedKey = ds_list_find_value(curse_list, 0)
    var _cursedName = ds_map_find_value(global.curse_name, _cursedKey)

    _name = scr_stringTransformFirst(_name, true)
    if (global.language == 5 || global.language == 8 || global.language == 6)
        ds_map_replace(data, "Name", (_name + _space + _cursedName))
    else
        ds_map_replace(data, "Name", (_cursedName + _space + _name))
}
