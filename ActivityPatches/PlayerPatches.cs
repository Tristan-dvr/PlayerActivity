﻿using HarmonyLib;
using UnityEngine;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch(typeof(Player))]
    public class PlayerPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.OnSpawned))]
        private static void Player_OnSpawned(Player __instance)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLogWithPlayerPosition(ActivityEvents.Spawned, __instance.GetPlayerData());
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.PlacePiece))]
        private static void Player_PlacePiece(Player __instance, Piece piece)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLogWithPosition(ActivityEvents.Place, Utils.GetPrefabName(piece.gameObject), __instance.m_placementGhost);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.TeleportTo))]
        private static void Player_TeleportTo(Player __instance, Vector3 pos, bool distantTeleport, ref bool __result)
        {
            if (!__result || !CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLogWithPosition(ActivityEvents.Teleport, $"distant:{distantTeleport} to:{pos.ToPresentableString()}", __instance);
        }
    }
}
