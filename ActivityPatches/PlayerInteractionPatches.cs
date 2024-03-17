using HarmonyLib;
using UnityEngine;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    public class PlayerInteractionPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Bed), nameof(Bed.Interact))]
        private static void Bed_Interact(Bed __instance, ref bool __result, Humanoid human)
        {
            TryLogInteraction(__instance, __result, human, __instance.GetOwnerName());
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Beehive), nameof(Beehive.Interact))]
        private static void Beehive_Interact(Beehive __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Chair), nameof(Chair.Interact))]
        private static void Chair_Interact(Chair __instance, ref bool __result, Humanoid human)
        {
            TryLogInteraction(__instance, __result, human);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Container), nameof(Container.Interact))]
        private static void Container_Interact(Container __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CookingStation), nameof(CookingStation.Interact))]
        private static void CookingStation_Interact(CookingStation __instance, ref bool __result, Humanoid user)
        {
            TryLogInteraction(__instance, __result, user);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CraftingStation), nameof(CraftingStation.Interact))]
        private static void CraftingStation_Interact(CraftingStation __instance, ref bool __result, Humanoid user)
        {
            TryLogInteraction(__instance, __result, user);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Door), nameof(Door.Interact))]
        private static void Door_Interact(Door __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Fermenter), nameof(Fermenter.Interact))]
        private static void Fermenter_Interact(Fermenter __instance, ref bool __result, Humanoid user)
        {
            TryLogInteraction(__instance, __result, user);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.Interact))]
        private static void Fireplace_Interact(Fireplace __instance, ref bool __result, Humanoid user)
        {
            TryLogInteraction(__instance, __result, user);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ItemStand), nameof(ItemStand.Interact))]
        private static void ItemStand_Interact(ItemStand __instance, ref bool __result, Humanoid user)
        {
            TryLogInteraction(__instance, __result, user, __instance.GetAttachedItem());
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Ladder), nameof(Ladder.Interact))]
        private static void Ladder_Interact(Ladder __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(OfferingBowl), nameof(OfferingBowl.Interact))]
        private static void OfferingBowl_Interact(OfferingBowl __instance, ref bool __result, Humanoid user)
        {
            TryLogInteraction(__instance, __result, user);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Pickable), nameof(Pickable.Interact))]
        private static void Pickable_Interact(Pickable __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PickableItem), nameof(PickableItem.Interact))]
        private static void PickableItem_Interact(PickableItem __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PrivateArea), nameof(PrivateArea.Interact))]
        private static void PrivateArea_Interact(PrivateArea __instance, ref bool __result, Humanoid human)
        {
            TryLogInteraction(__instance, __result, human, $"{__instance.GetCreatorName()} active:{__instance.IsEnabled()}");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Raven), nameof(Raven.Interact))]
        private static void Raven_Interact(Raven __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(RuneStone), nameof(RuneStone.Interact))]
        private static void RuneStone_Interact(RuneStone __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ShipControlls), nameof(ShipControlls.Interact))]
        private static void ShipControlls_Interact(ShipControlls __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Sign), nameof(Sign.Interact))]
        private static void Sign_Interact(Sign __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Switch), nameof(Switch.Interact))]
        private static void Switch_Interact(Switch __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Tameable), nameof(Tameable.Interact))]
        private static void Tameable_Interact(Tameable __instance, ref bool __result, Humanoid user)
        {
            TryLogInteraction(__instance, __result, user);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Teleport), nameof(Teleport.Interact))]
        private static void Teleport_Interact(Teleport __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TeleportWorld), nameof(TeleportWorld.Interact))]
        private static void TeleportWorld_Interact(TeleportWorld __instance, ref bool __result, Humanoid human)
        {
            TryLogInteraction(__instance, __result, human);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ToggleSwitch), nameof(ToggleSwitch.Interact))]
        private static void ToggleSwitch_Interact(ToggleSwitch __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TombStone), nameof(TombStone.Interact))]
        private static void TombStone_Interact(TombStone __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character, __instance.GetOwnerName());
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Trader), nameof(Trader.Interact))]
        private static void Trader_Interact(Trader __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Vagon), nameof(Vagon.Interact))]
        private static void Vagon_Interact(Vagon __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Vegvisir), nameof(Vegvisir.Interact))]
        private static void Vegvisir_Interact(Vegvisir __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WayStone), nameof(WayStone.Interact))]
        private static void WayStone_Interact(WayStone __instance, ref bool __result, Humanoid character)
        {
            TryLogInteraction(__instance, __result, character);
        }

        private static void TryLogInteraction(Component obj, bool result, Humanoid human, string info = null)
        {
            if (!CheckIsLocalPlayer(human)) return;

            ActivityLog.AddLogWithPosition($"Interact {Utils.GetPrefabName(obj.gameObject)} result:{result} info:{info}", obj);
        }
    }
}
