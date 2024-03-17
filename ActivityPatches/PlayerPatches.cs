using HarmonyLib;
using UnityEngine;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch(typeof(Player))]
    public class PlayerPatches
    {
        private static bool _inDodge = false;

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.OnSpawned))]
        private static void Player_OnSpawned(Player __instance)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLogWithPosition($"Spawned {__instance.GetPlayerData()}", __instance);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.PlacePiece))]
        private static void Player_PlacePiece(Player __instance, Piece piece, ref bool __result)
        {
            if (!__result || !CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLog($"Place {Utils.GetPrefabName(piece.gameObject)}", __instance.m_placementGhost.transform.position);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.TeleportTo))]
        private static void Player_TeleportTo(Player __instance, Vector3 pos, bool distantTeleport, ref bool __result)
        {
            if (!__result || !CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLogWithPosition($"Teleport distant:{distantTeleport} to:{pos.ToPresentableString()}", __instance);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.UpdateDodge))]
        private static void Player_UpdateDodge_Prefix(Player __instance)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            _inDodge = __instance.InDodge();
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.UpdateDodge))]
        private static void Player_UpdateDodge_Postfix(Player __instance)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            if (!_inDodge && __instance.InDodge())
            {
                ActivityLog.AddLogWithPosition("Dodge", __instance);
            }
            _inDodge = __instance.InDodge();
        }
    }
}
