var _Fragments = variable_struct_get(global.__dialogue_flow_data.willow_lowcrey, "Fragments")
var _Scripts = variable_struct_get(global.__dialogue_flow_data.willow_lowcrey, "Scripts")
var _Specs = variable_struct_get(global.__dialogue_flow_data.willow_lowcrey, "Specs")

_Fragments.HUB_Cnd_6E9D167C_negative = "condition_CND_curseScrollIntro"
_Fragments.condition_CND_curseScrollIntro = ["Cnd_curseScrollIntro_positive", "Cnd_curseScrollIntro_negative"]
_Fragments.Cnd_curseScrollIntro_negative = "greeting_HASH1"
_Fragments.Cnd_curseScrollIntro_positive = "cursescroll_ready_to_intro"
_Fragments.cursescroll_ready_to_intro = "cursescroll_ready_to_sell"
_Fragments.cursescroll_ready_to_sell = ["cursescroll_ready_to_sell_pc", "cursescroll_ready_to_sell_refuse_pc"]
_Fragments.cursescroll_ready_to_sell_pc = "instruction_INS_readyToSell"
_Fragments.cursescroll_ready_to_sell_refuse_pc = "@dialogue_end"
_Fragments.instruction_INS_readyToSell = "@dialogue_end"

_Specs.Cnd_curseScrollIntro_positive = { hub: true }
_Specs.Cnd_curseScrollIntro_negative = { hub: true }
_Specs.instruction_INS_readyToSell = { action: true }

_Scripts.condition_CND_curseScrollIntro = function()
{
    with (owner)
    {
        var _timestamp = scr_npc_get_global_info("make_curse_scroll_timestamp")
        var _daysPassed = scr_timeGetPassed(_timestamp, 3)
        return scr_npc_get_global_info("num_of_cursed_item") >= 3
                && _daysPassed >= 1 && !scr_dialogue_complete("cursescroll_ready_to_sell")
    }
}

_Scripts.instruction_INS_readyToSell = "scr_curseScroll_lowcreyUpdateInventory"
