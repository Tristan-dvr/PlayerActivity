using UnityEngine;

namespace PlayerActivity
{
    public static class ActivityLog
    {
        private static ActivityLogger _logger => ActivityLogger.Instance;

        public static void AddLog(string log)
        {
            _logger.AddLog(log);
        }

        public static void AddLog(string log, Vector3 position)
        {
            _logger.AddLog(string.Format("{0} {1}", position.ToPresentableString(), log));
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
