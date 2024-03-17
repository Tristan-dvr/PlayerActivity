using HarmonyLib;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class DisconnectPatch
    {
        [HarmonyPriority(Priority.First)]
        [HarmonyPrefix, HarmonyPatch(typeof(Game), nameof(Game.Shutdown))]
        private static void Game_Shutdown()
        {
            ActivityLog.AddLogWithPlayerPosition("Disconnect");
        }
    }
}
