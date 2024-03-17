using HarmonyLib;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class RepairItemPatches
    {
        private static bool _repairing;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.CanRepair))]
        private static void InventoryGui_CanRepair(ItemDrop.ItemData item, ref bool __result)
        {
            if (!__result || !_repairing) return;

            ActivityLog.AddLogWithPlayerPosition($"Repair {item.ToPresentableString()}");
        }

        [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.RepairOneItem))]
        private class RepairOneItemPatches
        {
            private static void Prefix() => _repairing = true;
            private static void Postfix() => _repairing = false;
        }
    }
}
