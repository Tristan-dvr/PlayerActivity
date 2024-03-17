using HarmonyLib;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class InventoryPatches
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
            typeof(string), typeof(int), typeof(int), typeof(int), typeof(long), typeof(string), typeof(bool))]
        private static void Inventory_AddItem_Postfix(ref ItemDrop.ItemData __result)
        {
            if (__result == null || !_isCrafting) return;

            ActivityLog.AddLogWithPlayerPosition($"Craft {__result.ToPresentableString()}");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Inventory), nameof(Inventory.MoveAll))]
        private static void Inventory_MoveAll(Inventory __instance, Inventory fromInventory)
        {
            ActivityLog.AddLogWithPlayerPosition($"MoveAll from:{fromInventory.GetName()} to:{__instance.GetName()}\n{GetContainerData(fromInventory)}");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Inventory), nameof(Inventory.MoveItemToThis), typeof(Inventory), typeof(ItemDrop.ItemData))]
        private static void Inventory_MoveItemToThis(Inventory __instance, Inventory fromInventory, ItemDrop.ItemData item)
        {
            ActivityLog.AddLogWithPlayerPosition($"Move from:{fromInventory.GetName()} to:{__instance.GetName()} {item.ToPresentableString()}");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Inventory), nameof(Inventory.MoveItemToThis), typeof(Inventory), typeof(ItemDrop.ItemData), typeof(int), typeof(int), typeof(int))]
        private static void Inventory_MoveItemToThisWithAmount(Inventory __instance, Inventory fromInventory, ItemDrop.ItemData item, int amount)
        {
            if (__instance == fromInventory && item.m_stack == amount) return;

            ActivityLog.AddLogWithPlayerPosition($"Move from:{fromInventory.GetName()} to:{__instance.GetName()} {item.ToPresentableString(amount)}");
        }

        [HarmonyFinalizer, HarmonyPatch(typeof(Inventory), nameof(Inventory.MoveInventoryToGrave))]
        private static void Inventory_MoveInventoryToGrave(Inventory __instance, Inventory original)
        {
            ActivityLog.AddLogWithPlayerPosition($"Move to grave\n{GetContainerData(__instance)}\nPlayer inventory\n{GetContainerData(original)}");
        }
    }
}
