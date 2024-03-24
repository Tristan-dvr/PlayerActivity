using HarmonyLib;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch(typeof(Player))]
    class DodgePatches
    {
        private static bool _inDodge = false;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.UpdateDodge))]
        private static void Player_UpdateDodge_Prefix(Player __instance)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            _inDodge = __instance.InDodge();
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.UpdateDodge))]
        private static void Player_UpdateDodge_Postfix(Player __instance)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            if (!_inDodge && __instance.InDodge())
            {
                ActivityLog.AddLogWithPosition("Dodge", __instance);
            }
            _inDodge = __instance.InDodge();
        }
    }
}
