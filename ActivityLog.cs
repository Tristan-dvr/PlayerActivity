using UnityEngine;

namespace PlayerActivity
{
    public static class ActivityLog
    {
        private static ActivityLogger _logger => ActivityLogger.Instance;

        public static void AddLog(string log)
        {
            if (_logger == null) return;

            _logger.AddLog(log);
        }

        public static void AddLog(string log, Vector3 position)
        {
            AddLog(ActivityLoggerUtil.FormatLog(log, position));
        }

        public static void AddLogWithPosition(string log, Component component)
        {
            if (component != null)
            {
                AddLog(log, component.transform.position);
            }
            else
            {
                AddLog(log);
            }
        }

        public static void AddLogWithPlayerPosition(string log)
        {
            if (Player.m_localPlayer != null)
            {
                AddLogWithPosition(log, Player.m_localPlayer);
            }
            else
            {
                AddLog(log);
            }
        }
    }
}
