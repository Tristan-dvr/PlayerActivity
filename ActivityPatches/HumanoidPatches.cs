using HarmonyLib;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch(typeof(Humanoid))]
    class HumanoidPatches
    {
        [HarmonyFinalizer]
        [HarmonyPatch(nameof(Humanoid.EquipItem))]
        private static void Humanoid_EquipItem(Humanoid __instance, ItemDrop.ItemData item, ref bool __result)
        {
            if (item == null || !__result || !CheckIsLocalPlayer(__instance) || !item.IsEquipable()) return;

            ActivityLog.AddLogWithPosition($"Equip {item.ToPresentableString()}", __instance);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Humanoid.UnequipItem))]
        private static void Humanoid_UnequipItem(Humanoid __instance, ItemDrop.ItemData item)
        {
            if (item == null || !CheckIsLocalPlayer(__instance) || !item.IsEquipable() || !item.m_equipped) return;

            ActivityLog.AddLogWithPosition($"Unequip {item.ToPresentableString()}", __instance);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Humanoid.DropItem))]
        private static void Humanoid_DropItem(Humanoid __instance, Inventory inventory, ItemDrop.ItemData item, int amount, ref bool __result)
        {
            if (!__result || !CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLogWithPosition($"Drop from:{inventory.GetName()} {item.ToPresentableString(amount)}", __instance);
        }
    }
}
