using System.Text;
using UnityEngine;

namespace PlayerActivity
{
    public static class ActivityLog
    {
        private static StringBuilder _builder = new StringBuilder();

        private static ActivityLogger _logger => ActivityLogger.Instance;

        public static void AddLog(string log)
        {
            _logger.AddLog(log);
        }

        public static void AddLog(string log, Vector3 position)
        {
            _builder.Clear();
            _builder.Append(position.ToPresentableString());
            _builder.Append(' ');
            _builder.Append(log);
            _logger.AddLog(_builder.ToString());
        }

        public static void AddLogWithPosition(string log, Component component)
        {
            _builder.Clear();
            _builder.Append(component.transform.position.ToPresentableString());
            _builder.Append(' ');
            _builder.Append(log);
            _logger.AddLog(_builder.ToString());
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
