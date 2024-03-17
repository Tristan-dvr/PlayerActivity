using HarmonyLib;
using static Terminal;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class TerminalPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ConsoleCommand), nameof(ConsoleCommand.RunAction))]
        private static void ConsoleCommand_RunAction(ConsoleCommand __instance, ConsoleEventArgs args)
        {
            ActivityLog.AddLogWithPlayerPosition($"Command {args.FullLine}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZNet), nameof(ZNet.RemoteCommand))]
        private static void ZNet_RemoteCommand(string command)
        {
            ActivityLog.AddLogWithPlayerPosition($"Command remote {command}");
        }
    }
}
