using HarmonyLib;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch(typeof(Character))]
    class DamagePatches
    {
        [HarmonyFinalizer]
        [HarmonyPatch(nameof(Character.ApplyDamage))]
        static void Character_ApplyDamage(Character __instance, HitData hit)
        {
            if (!CheckIsLocalPlayer(__instance)) return;

            string name;
            if (!hit.HaveAttacker())
                name = "none";
            else if (hit.GetAttacker() is Player player)
                name = player.GetPlayerData();
            else
                name = Utils.GetPrefabName(hit.GetAttacker().gameObject);

            var health = __instance.GetHealth();
            ActivityLog.AddLogWithPosition(ActivityEvents.Damaged, $"by:{name} health:{health:0.0} {GetDamageLog(hit)}", __instance);

            if (__instance.GetHealth() > 0) return;

            ActivityLog.AddLogWithPosition(ActivityEvents.Dead, $"by:{name}", __instance);
        }
    }
}
