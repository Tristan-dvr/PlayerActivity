using UnityEngine;

namespace PlayerActivity
{
    public static class ActivityLog
    {
        public static void AddLogWithPosition(string eventName, string parameters, Component component)
        {
            AddLogWithPosition(eventName, parameters, component != null ? component.transform.position : default);
        }

        public static void AddLogWithPosition(string eventName, string parameters, GameObject gameObject)
        {
            AddLogWithPosition(eventName, parameters, gameObject != null ? gameObject.transform.position : default);
        }

        public static void AddLogWithPlayerPosition(string eventName, string parameters)
        {
            AddLogWithPosition(eventName, parameters, Player.m_localPlayer);
        }

        public static void AddLogWithPosition(string eventName, string parameters, Vector3 position)
        {
            var log = new LogData
            {
                eventName = eventName,
                time = ZNet.instance.GetTimeSeconds(),
                parameters = parameters,
                position = position,
            };

            Log.Debug(log);
            ZRoutedRpc.instance.InvokeRoutedRPC(ActivityStorageService.LogRpc, log);
        }
    }
}
