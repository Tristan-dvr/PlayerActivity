using HarmonyLib;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class InventoryPatches
    {
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
    }
}
