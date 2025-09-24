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
            if (CheckIsLocalPlayer(__instance))
            {
                string name;
                if (!hit.HaveAttacker())
                {
                    name = "none";
                }
                else if (hit.GetAttacker() is Player player)
                    name = player.GetPlayerData();
                else
                    name = Utils.GetPrefabName(hit.GetAttacker().gameObject);

                float health = __instance.GetHealth();
                ActivityLog.AddLogWithPosition(ActivityEvents.Damaged, $"by:{name} health:{health:0.0} {ActivityLoggerUtil.GetDamageLog(hit)}", __instance);

                if (!(__instance.GetHealth() > 0f))
                {
                    ActivityLog.AddLogWithPosition(ActivityEvents.Dead, $"by:{name}", __instance);
                }
            }

            if (hit != null && hit.HaveAttacker())
            {
                Character attackerChar2 = hit.GetAttacker();
                Player attackerPlayer2 = (attackerChar2 is Player) ? (Player)attackerChar2 : null;
                if (attackerPlayer2 != null) 
                {
                    if (!(__instance.GetHealth() > 0f))
                    {
                        string killerName = attackerPlayer2.GetPlayerData();
                        string victimName = Utils.GetPrefabName(__instance.gameObject);

                        ActivityLog.AddLogWithPosition(ActivityEvents.Kill, $"killer:{killerName} victim:{victimName}", __instance);
                    }
                }
            }
        }
    }
}
