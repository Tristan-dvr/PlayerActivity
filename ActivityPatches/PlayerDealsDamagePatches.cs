using HarmonyLib;
using UnityEngine;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class PlayerDealsDamagePatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
        private static void Character_Damage(Character __instance, HitData hit)
        {
            if (!IsLocalPlayerHit(hit)) return;

            var name = __instance is Player player
                ? player.GetPlayerData()
                : $"{Utils.GetPrefabName(__instance.gameObject)} lvl:{__instance.GetLevel()} tamed:{__instance.IsTamed()}";

            ActivityLog.AddLogWithPosition($"Damage {name} {GetDamageLog(hit)}", __instance);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Destructible), nameof(Destructible.Damage))]
        private static void Destructible_Damage(Destructible __instance, HitData hit, bool __runOriginal)
        {
            if (!__runOriginal) return;

            TryLogStructureDamage(__instance, hit);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(HitArea), nameof(HitArea.Damage))]
        private static void HitArea_Damage(HitArea __instance, HitData hit, bool __runOriginal)
        {
            if (!__runOriginal) return;

            TryLogStructureDamage(__instance, hit);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MineRock), nameof(MineRock.Damage))]
        private static void MineRock_Damage(MineRock __instance, HitData hit, bool __runOriginal)
        {
            if (!__runOriginal) return;

            TryLogStructureDamage(__instance, hit);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MineRock5), nameof(MineRock5.Damage))]
        private static void MineRock5_Damage(MineRock5 __instance, HitData hit, bool __runOriginal)
        {
            if (!__runOriginal) return;

            TryLogStructureDamage(__instance, hit);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Raven), nameof(Raven.Damage))]
        private static void Raven_Damage(Raven __instance, HitData hit, bool __runOriginal)
        {
            if (!__runOriginal) return;

            TryLogStructureDamage(__instance, hit);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WearNTear), nameof(WearNTear.Damage))]
        private static void WearNTear_Damage(WearNTear __instance, HitData hit, bool __runOriginal)
        {
            if (!__runOriginal) return;

            TryLogStructureDamage(__instance, hit);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TreeBase), nameof(TreeBase.Damage))]
        private static void TreeBase_Damage(TreeBase __instance, HitData hit, bool __runOriginal)
        {
            if (!__runOriginal) return;

            TryLogStructureDamage(__instance, hit);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TreeLog), nameof(TreeLog.Damage))]
        private static void TreeLog_Damage(TreeLog __instance, HitData hit, bool __runOriginal)
        {
            if (!__runOriginal) return;

            TryLogStructureDamage(__instance, hit);
        }

        private static void TryLogStructureDamage(Component target, HitData hit)
        {
            if (!IsLocalPlayerHit(hit)) return;

            ActivityLog.AddLogWithPosition($"Damage {Utils.GetPrefabName(target.gameObject)} {GetDamageLog(hit)}", target);
        }

        private static bool IsLocalPlayerHit(HitData hit)
        {
            return hit.HaveAttacker() && CheckIsLocalPlayer(hit.GetAttacker());
        }
    }
}
