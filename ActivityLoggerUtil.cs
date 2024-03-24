using System.Globalization;
using System;
using System.Text;
using UnityEngine;

namespace PlayerActivity
{
    public static class ActivityLoggerUtil
    {
        private const string DefaultDateFormat = "yyyy-MM-dd HH:mm:ss";

        private static StringBuilder _builder = new StringBuilder();

        public static bool CheckIsLocalPlayer(Character character)
        {
            return character == Player.m_localPlayer;
        }

        public static string GetContainerData(Container container)
        {
            return GetContainerData(container.GetInventory());
        }

        public static string GetContainerData(Inventory inventory)
        {
            _builder.Clear();
            inventory.GetAllItems().ForEach(item => _builder.AppendLine(item.ToPresentableString()));
            return _builder.ToString();
        }

        public static string GetDamageLog(HitData hit)
        {
            _builder.Clear();
            var damage = hit.m_damage;
            if (damage.m_damage > 0) AppendDamageToBuilder("damage", damage.m_damage);
            if (damage.m_blunt > 0) AppendDamageToBuilder("blunt", damage.m_blunt);
            if (damage.m_slash > 0) AppendDamageToBuilder("slash", damage.m_slash);
            if (damage.m_pierce > 0) AppendDamageToBuilder("pierce", damage.m_pierce);
            if (damage.m_fire > 0) AppendDamageToBuilder("fire", damage.m_fire);
            if (damage.m_frost > 0) AppendDamageToBuilder("frost", damage.m_frost);
            if (damage.m_lightning > 0) AppendDamageToBuilder("light", damage.m_lightning);
            if (damage.m_poison > 0) AppendDamageToBuilder("poison", damage.m_poison);
            if (damage.m_spirit > 0) AppendDamageToBuilder("spirit", damage.m_spirit);
            if (damage.m_chop > 0) AppendDamageToBuilder("chop", damage.m_chop);
            if (damage.m_pickaxe > 0) AppendDamageToBuilder("pickaxe", damage.m_pickaxe);
            AppendDamageToBuilder("total", damage.GetTotalDamage());
            return _builder.ToString();
        }

        private static void AppendDamageToBuilder(string name, float damage)
        {
            _builder.AppendFormat("{0}:{1:0.0} ", name, damage);
        }

        public static string AddDateToLog(string log)
        {
            return string.Format("[{0}] {1}", GetCurrentDateText(), log);
        }

        public static string FormatLog(string log, Vector3 position)
        {
            return string.Format("{0} {1}", position.ToPresentableString(), log);
        }

        public static string GetCurrentDateText(string format = DefaultDateFormat)
        {
            return DateTime.UtcNow.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string GetPlayerData(this Player player)
        {
            if (player == null) return string.Empty;

            return string.Format("{0}({1})", player.GetPlayerName(), player.GetPlayerID());
        }

        public static string ToPresentableString(this Vector3 position)
        {
            return string.Format("({0:0.0} {1:0.0} {2:0.0})", position.x, position.y, position.z);
        }
    }
}
