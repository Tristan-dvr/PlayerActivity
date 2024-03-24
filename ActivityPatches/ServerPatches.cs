using HarmonyLib;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch]
    class ServerPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(ZNet), nameof(ZNet.SendPeerInfo))]
        private static void ZNet_SendPeerInfo(ZNet __instance, ZRpc rpc)
        {
            if (!__instance.IsServer()) return;

            var steamId = rpc.m_socket.GetHostName();
            AppendServerLog(steamId, "Connected");
        }

        [HarmonyPrefix, HarmonyPatch(typeof(ZNet), nameof(ZNet.Disconnect))]
        private static void ZNet_Disconnect(ZNet __instance, ZNetPeer peer)
        {
            if (!__instance.IsServer()) return;

            var steamId = peer.m_rpc.m_socket.GetHostName();
            if (peer.m_characterID != default)
            {
                AppendServerLog(steamId, ActivityLoggerUtil.FormatLog("Disconnected", peer.GetRefPos()));
            }
            else
            {
                AppendServerLog(steamId, "Disconnected");
            }
        }

        private static void AppendServerLog(string steamId, string log)
        {
            ActivityStorageService.Instance.AppendPlayerLogs(steamId,
                ActivityLoggerUtil.AddDateToLog(log));
        }
    }
}
