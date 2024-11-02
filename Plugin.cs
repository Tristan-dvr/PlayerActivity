using BepInEx;
using HarmonyLib;
using System.Reflection;
using static PlayerActivity.ActivityLoggerUtil;

namespace PlayerActivity
{
    [BepInPlugin(Guid, Name, Version)]
    internal class Plugin : BaseUnityPlugin
    {
        private const string Guid = "org.tristan.playeractivity";
        public const string Name = "Player Activity";
        public const string Version = "1.1.4";

        private void Awake()
        {
            Log.CreateInstance(Logger);
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), Guid);
        }

        [HarmonyPatch]
        private class InitializationPatch
        {
            private static ActivityLogger _logger;

            [HarmonyPostfix, HarmonyPatch(typeof(Game), nameof(Game.Start))]
            private static void Game_Start(Game __instance)
            {
                _logger = __instance.gameObject.AddComponent<ActivityLogger>();

                if (!ZNet.instance.IsServer()) return;

                __instance.gameObject.AddComponent<ActivityStorageService>();
            }

            [HarmonyPostfix, HarmonyPatch(typeof(Player), nameof(Player.SetLocalPlayer))]
            private static void Player_SetLocalPlayer(Player __instance)
            {
                __instance.gameObject.AddComponent<PlayerLogger>();
            }

            [HarmonyPriority(Priority.Last)]
            [HarmonyPrefix, HarmonyPatch(typeof(Game), nameof(Game.Shutdown))]
            private static void Game_Shutdown()
            {
                _logger.Flush();
            }

            [HarmonyFinalizer, HarmonyPatch(typeof(Player), nameof(Player.OnDeath))]
            private static void Player_OnDeath(Player __instance)
            {
                if (!CheckIsLocalPlayer(__instance)) return;

                _logger.Flush();
            }
        }
    }
}
