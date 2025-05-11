using HarmonyLib;
using UnityEngine;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class PickupPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.Pickup))]
        private static void Humanoid_Pickup(Humanoid __instance, GameObject go, ref bool __result)
        {
            if (!CheckIsLocalPlayer(__instance) || !__result || !go.TryGetComponent<ItemDrop>(out var item)) return;

            ActivityLog.AddLogWithPosition(ActivityEvents.Pickup, item.m_itemData.ToPresentableString(), item);
        }
    }
}
