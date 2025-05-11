using HarmonyLib;
using System.Collections.Generic;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class InventoryPatches
    {
        [HarmonyPrefix, HarmonyPatch(typeof(Inventory), nameof(Inventory.MoveAll))]
        private static void Inventory_MoveAll(Inventory __instance, Inventory fromInventory)
        {
            ActivityLog.AddLogWithPlayerPosition(ActivityEvents.MoveAll, $"from:{fromInventory.GetName()} to:{__instance.GetName()}\n{GetContainerData(fromInventory)}");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Inventory), nameof(Inventory.MoveItemToThis), typeof(Inventory), typeof(ItemDrop.ItemData))]
        private static void Inventory_MoveItemToThis(Inventory __instance, Inventory fromInventory, ItemDrop.ItemData item)
        {
            ActivityLog.AddLogWithPlayerPosition(ActivityEvents.Move, $"from:{fromInventory.GetName()} to:{__instance.GetName()} {item.ToPresentableString()}");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(Inventory), nameof(Inventory.MoveItemToThis), typeof(Inventory), typeof(ItemDrop.ItemData), typeof(int), typeof(int), typeof(int))]
        private static void Inventory_MoveItemToThisWithAmount(Inventory __instance, Inventory fromInventory, ItemDrop.ItemData item, int amount)
        {
            if (__instance == fromInventory && item.m_stack == amount) return;

            ActivityLog.AddLogWithPlayerPosition(ActivityEvents.Move, $"from:{fromInventory.GetName()} to:{__instance.GetName()} {item.ToPresentableString(amount)}");
        }

        [HarmonyPatch]
        private class StackAllPatch
        {
            private static Inventory _stackFromInventory;
            private static readonly HashSet<ItemDrop.ItemData> _movedItems = new HashSet<ItemDrop.ItemData>();

            [HarmonyPrefix, HarmonyPatch(typeof(Inventory), nameof(Inventory.StackAll))]
            private static void Inventory_StackAll_Prefix(Inventory __instance, Inventory fromInventory)
            {
                _movedItems.Clear();
                _stackFromInventory = fromInventory;
            }

            [HarmonyPostfix, HarmonyPatch(typeof(Inventory), nameof(Inventory.StackAll))]
            private static void Inventory_StackAll_Postfix(Inventory __instance, Inventory fromInventory, ref int __result)
            {
                _stackFromInventory = null;
                if (__instance == fromInventory || __result == 0) return;

                ActivityLog.AddLogWithPlayerPosition(ActivityEvents.StackAll, $"from:{fromInventory.GetName()} to:{__instance.GetName()}\n{GetContainerData(_movedItems)}");
            }

            [HarmonyPostfix, HarmonyPatch(typeof(Inventory), nameof(Inventory.RemoveItem),
                typeof(ItemDrop.ItemData))]
            private static void Inventory_RemoveItem(Inventory __instance, ItemDrop.ItemData item)
            {
                if (__instance != _stackFromInventory) return;

                _movedItems.Add(item);
            }
        }
    }
}
