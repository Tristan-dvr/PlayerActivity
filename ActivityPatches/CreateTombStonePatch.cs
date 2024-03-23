using HarmonyLib;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class CreateTombStonePatch
    {
        [HarmonyPatch(typeof(TombStone), nameof(TombStone.Setup))]
        private static void TombStone_Setup(TombStone __instance)
        {
            var player = Player.m_localPlayer;
            var container = __instance.GetComponent<Container>();
            ActivityLog.AddLogWithPosition($"Grave items:\n{GetContainerData(container.GetInventory())}\nplayer:\n{GetContainerData(player.GetInventory())}", __instance);
        }
    }
}
