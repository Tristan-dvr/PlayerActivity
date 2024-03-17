using HarmonyLib;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class RepairBuildingPatches
    {
        private static bool _repairing;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WearNTear), nameof(WearNTear.Repair))]
        private static void InventoryGui_CanRepair(WearNTear __instance, ref bool __result)
        {
            if (!_repairing || !__result) return;

            ActivityLog.AddLogWithPosition($"Repair building {Utils.GetPrefabName(__instance.gameObject)}", __instance);
        }

        [HarmonyPatch(typeof(Player), nameof(Player.Repair))]
        private class PlayerRepairPatches
        {
            private static void Prefix() => _repairing = true;
            private static void Postfix() => _repairing = false;
        }
    }
}
