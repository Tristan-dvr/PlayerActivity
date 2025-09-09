using HarmonyLib;
using UnityEngine;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    public class PlayerInteractionPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PrivateArea), nameof(PrivateArea.Interact))]
        private static void PrivateArea_Interact(PrivateArea __instance, ref bool __result, Humanoid human)
        {
            TryLogInteraction(__instance, __result, human, $"{__instance.GetCreatorName()} active:{__instance.IsEnabled()}");
        }

        // character parameter
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WayStone), nameof(WayStone.Interact))]
        [HarmonyPatch(typeof(Vegvisir), nameof(Vegvisir.Interact))]
        [HarmonyPatch(typeof(Vagon), nameof(Vagon.Interact))]
        [HarmonyPatch(typeof(Trader), nameof(Trader.Interact))]
        [HarmonyPatch(typeof(TombStone), nameof(TombStone.Interact))]
        [HarmonyPatch(typeof(ToggleSwitch), nameof(ToggleSwitch.Interact))]
        [HarmonyPatch(typeof(Teleport), nameof(Teleport.Interact))]
        [HarmonyPatch(typeof(Switch), nameof(Switch.Interact))]
        [HarmonyPatch(typeof(Sign), nameof(Sign.Interact))]
        [HarmonyPatch(typeof(ShipControlls), nameof(ShipControlls.Interact))]
        [HarmonyPatch(typeof(RuneStone), nameof(RuneStone.Interact))]
        [HarmonyPatch(typeof(Raven), nameof(Raven.Interact))]
        [HarmonyPatch(typeof(PickableItem), nameof(PickableItem.Interact))]
        [HarmonyPatch(typeof(Pickable), nameof(Pickable.Interact))]
        [HarmonyPatch(typeof(Ladder), nameof(Ladder.Interact))]
        [HarmonyPatch(typeof(Beehive), nameof(Beehive.Interact))]
        [HarmonyPatch(typeof(Container), nameof(Container.Interact))]
        [HarmonyPatch(typeof(Door), nameof(Door.Interact))]
        [HarmonyPatch(typeof(Fish), nameof(Fish.Interact))]
        [HarmonyPatch(typeof(RopeAttachment), nameof(RopeAttachment.Interact))]
        [HarmonyPatch(typeof(SapCollector), nameof(SapCollector.Interact))]
        [HarmonyPatch(typeof(Trap), nameof(Trap.Interact))]
        [HarmonyPatch(typeof(Turret), nameof(Turret.Interact))]
        [HarmonyPatch(typeof(ItemDrop), nameof(ItemDrop.Interact))]
        [HarmonyPatch(typeof(Sadle), nameof(Sadle.Interact))]
        private static void InteractableWithCharacter_Interact(Component __instance, Humanoid character, ref bool __result)
        {
            TryLogInteraction(__instance, __result, character);
        }

        // human parameter
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Feast), nameof(Feast.Interact))]
        [HarmonyPatch(typeof(TeleportWorld), nameof(TeleportWorld.Interact))]
        [HarmonyPatch(typeof(Bed), nameof(Bed.Interact))]
        [HarmonyPatch(typeof(Chair), nameof(Chair.Interact))]
        [HarmonyPatch(typeof(Barber), nameof(Barber.Interact))]
        private static void InteractableWithHuman_Interact(Component __instance, Humanoid human, ref bool __result)
        {
            TryLogInteraction(__instance, __result, human);
        }

        // user parameter
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ArcheryTarget), nameof(ArcheryTarget.Interact))]
        [HarmonyPatch(typeof(Tameable), nameof(Tameable.Interact))]
        [HarmonyPatch(typeof(OfferingBowl), nameof(OfferingBowl.Interact))]
        [HarmonyPatch(typeof(CookingStation), nameof(CookingStation.Interact))]
        [HarmonyPatch(typeof(CraftingStation), nameof(CraftingStation.Interact))]
        [HarmonyPatch(typeof(Fermenter), nameof(Fermenter.Interact))]
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.Interact))]
        [HarmonyPatch(typeof(ItemStand), nameof(ItemStand.Interact))]
        [HarmonyPatch(typeof(Pet), nameof(Pet.Interact))]
        [HarmonyPatch(typeof(ShieldGenerator), nameof(ShieldGenerator.Interact))]
        [HarmonyPatch(typeof(Petable), nameof(Petable.Interact))]
        private static void InteractableWithUser_Interact(Component __instance, Humanoid user, ref bool __result)
        {
            TryLogInteraction(__instance, __result, user);
        }

        private static void TryLogInteraction(Component obj, bool result, Humanoid human, string info = null)
        {
            if (!CheckIsLocalPlayer(human)) return;

            var infoText = string.IsNullOrEmpty(info) 
                ? string.Empty 
                : string.Format("info:{0}", info);
            ActivityLog.AddLogWithPosition(ActivityEvents.Interact, $"{Utils.GetPrefabName(obj.gameObject)} result:{result} {infoText}", obj);
        }
    }
}
