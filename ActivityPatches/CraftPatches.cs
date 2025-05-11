using HarmonyLib;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class CraftPatches
    {
        private static bool _isCrafting = false;

        [HarmonyPriority(int.MinValue)]
        [HarmonyPrefix, HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.DoCrafting))]
        private static void InventoryGui_DoCrafting_Prefix(InventoryGui __instance)
        {
            _isCrafting = true;
        }

        [HarmonyPriority(int.MaxValue)]
        [HarmonyPostfix, HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.DoCrafting))]
        private static void InventoryGui_DoCrafting_Postfix(InventoryGui __instance)
        {
            _isCrafting = false;
        }

        [HarmonyPriority(int.MaxValue)]
        [HarmonyFinalizer, HarmonyPatch(typeof(Inventory), nameof(Inventory.AddItem),
            typeof(string), typeof(int), typeof(int), typeof(int), typeof(long), typeof(string), typeof(Vector2i), typeof(bool))]
        private static void Inventory_AddItem_Postfix(ref ItemDrop.ItemData __result)
        {
            if (__result == null || !_isCrafting) return;

            ActivityLog.AddLogWithPlayerPosition(ActivityEvents.Craft, __result.ToPresentableString());
        }
    }
}
