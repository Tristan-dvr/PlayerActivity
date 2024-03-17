using HarmonyLib;
using UnityEngine;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    public class PlayerUseItemPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), nameof(Player.ConsumeItem))]
        private static void Humanoid_UseItem(Player __instance, Inventory inventory, ItemDrop.ItemData item)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLogWithPosition($"Consume {item.ToPresentableString()} inventory:{inventory?.GetName()}", __instance);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CookingStation), nameof(CookingStation.UseItem))]
        private static void CookingStation_UseItem(CookingStation __instance, Humanoid user, ItemDrop.ItemData item, ref bool __result)
        {
            TryLogUsingItem(__instance, user, item, __result);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.UseItem))]
        private static void Fireplace_UseItem(Fireplace __instance, Humanoid user, ItemDrop.ItemData item, ref bool __result)
        {
            TryLogUsingItem(__instance, user, item, __result);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Fermenter), nameof(Fermenter.UseItem))]
        private static void Fermenter_UseItem(Fermenter __instance, Humanoid user, ItemDrop.ItemData item, ref bool __result)
        {
            TryLogUsingItem(__instance, user, item, __result);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ItemStand), nameof(ItemStand.UseItem))]
        private static void ItemStand_UseItem(ItemStand __instance, Humanoid user, ItemDrop.ItemData item, ref bool __result)
        {
            TryLogUsingItem(__instance, user, item, __result);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(OfferingBowl), nameof(OfferingBowl.UseItem))]
        private static void OfferingBowl_UseItem(OfferingBowl __instance, Humanoid user, ItemDrop.ItemData item, ref bool __result)
        {
            TryLogUsingItem(__instance, user, item, __result);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Switch), nameof(Switch.UseItem))]
        private static void Switch_UseItem(Switch __instance, Humanoid user, ItemDrop.ItemData item, ref bool __result)
        {
            TryLogUsingItem(__instance, user, item, __result);
        }

        [HarmonyFinalizer]
        [HarmonyPatch(typeof(PrivateArea), nameof(PrivateArea.UseItem))]
        private static void PrivateArea_UseItem(PrivateArea __instance, Humanoid user, ItemDrop.ItemData item, ref bool __result)
        {
            TryLogUsingItem(__instance, user, item, __result);
        }

        private static void TryLogUsingItem(Component obj, Humanoid humanoid, ItemDrop.ItemData item, bool result)
        {
            if (!result || !CheckIsLocalPlayer(humanoid)) return;

            ActivityLog.AddLogWithPosition($"Use target:{Utils.GetPrefabName(obj.gameObject)} item:{item.ToPresentableString()}", obj);
        }
    }
}
