using BepInEx;
using HarmonyLib;
using System.Reflection;

namespace PlayerActivity
{
    [BepInPlugin(Guid, Name, Version)]
    internal class Plugin : BaseUnityPlugin
    {
        private const string Guid = "org.tristan.playeractivity";
        public const string Name = "Player Activity";
        public const string Version = "1.2.1";

        private void Awake()
        {
            Log.CreateInstance(Logger);
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), Guid);
        }

        [HarmonyPatch]
        private class InitializationPatch
        {
            [HarmonyPostfix, HarmonyPatch(typeof(Game), nameof(Game.Start))]
            private static void Game_Start(Game __instance)
            {
                if (!ZNet.instance.IsServer()) return;

                __instance.gameObject.AddComponent<ActivityStorageService>();
            }

            [HarmonyPostfix, HarmonyPatch(typeof(Player), nameof(Player.SetLocalPlayer))]
            private static void Player_SetLocalPlayer(Player __instance)
            {
                __instance.gameObject.AddComponent<PlayerLogger>();
            }
        }
    }
}
