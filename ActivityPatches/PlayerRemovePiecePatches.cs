using HarmonyLib;
using UnityEngine;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch(typeof(Player))]
    public class PlayerRemovePiecePatches
    {
        private static bool _removingPiece;
        private static RemovedPieceData _piece;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.RemovePiece))]
        private static void Player_RemovePiece_Prefix(Player __instance)
        {
            _removingPiece = CheckIsLocalPlayer(__instance);
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.CheckCanRemovePiece))]
        private static void Player_RemovePiece_Prefix(Player __instance, Piece piece, ref bool __result)
        {
            if (!__result || !_removingPiece || !CheckIsLocalPlayer(__instance)) return;

            _piece = new RemovedPieceData()
            {
                name = Utils.GetPrefabName(piece.gameObject),
                positon = piece.transform.position,
            };
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.RemovePiece))]
        private static void Player_RemovePiece_Postfix(Player __instance, ref bool __result)
        {
            if (!__result || _piece == null || !CheckIsLocalPlayer(__instance)) return;

            ActivityLog.AddLogWithPosition(ActivityEvents.Remove, _piece.name, _piece.positon);

            _piece = null;
        }

        private class RemovedPieceData
        {
            public string name;
            public Vector3 positon;
        }
    }
}
