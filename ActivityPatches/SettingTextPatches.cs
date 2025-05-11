using HarmonyLib;
using UnityEngine;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    public class SettingTextPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Tameable), nameof(Tameable.SetText))]
        private static void Tameable_SetText(Tameable __instance, string text)
        {
            if (!__instance.m_nview.IsValid()) return;
            
            AddSetTextLog(__instance, text);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Sign), nameof(Sign.SetText))]
        private static void Sign_SetText(Sign __instance, string text)
        {
            if (!__instance.m_nview.IsValid() || !string.Equals(__instance.GetText(), text)) return;

            AddSetTextLog(__instance, text);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TeleportWorld), nameof(TeleportWorld.SetText))]
        private static void TeleportWorld_SetText(TeleportWorld __instance, string text)
        {
            if (!__instance.m_nview.IsValid()) return;
            
            AddSetTextLog(__instance, text);
        }

        private static void AddSetTextLog(Component obj, string text)
        {
            ActivityLog.AddLogWithPosition(ActivityEvents.Text, $"target:{Utils.GetPrefabName(obj.gameObject)} text:{text}", obj);
        }
    }
}
