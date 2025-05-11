using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PlayerActivity
{
    public static class ActivityLoggerUtil
    {
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
            return GetContainerData(inventory.GetAllItems());
        }

        public static string GetContainerData(IEnumerable<ItemDrop.ItemData> itemsList)
        {
            _builder.Clear();
            foreach (var item in itemsList)
            {
                _builder.AppendLine(item.ToPresentableString());
            }
            return _builder.ToString().TrimEnd();
        }

        public static string GetDamageLog(HitData hit)
        {
            _builder.Clear();
            var damage = hit.m_damage;
            if (damage.m_damage > 0) AppendDamageToBuilder("damage", damage.m_damage, _builder);
            if (damage.m_blunt > 0) AppendDamageToBuilder("blunt", damage.m_blunt, _builder);
            if (damage.m_slash > 0) AppendDamageToBuilder("slash", damage.m_slash, _builder);
            if (damage.m_pierce > 0) AppendDamageToBuilder("pierce", damage.m_pierce, _builder);
            if (damage.m_fire > 0) AppendDamageToBuilder("fire", damage.m_fire, _builder);
            if (damage.m_frost > 0) AppendDamageToBuilder("frost", damage.m_frost, _builder);
            if (damage.m_lightning > 0) AppendDamageToBuilder("light", damage.m_lightning, _builder);
            if (damage.m_poison > 0) AppendDamageToBuilder("poison", damage.m_poison, _builder);
            if (damage.m_spirit > 0) AppendDamageToBuilder("spirit", damage.m_spirit, _builder);
            if (damage.m_chop > 0) AppendDamageToBuilder("chop", damage.m_chop, _builder);
            if (damage.m_pickaxe > 0) AppendDamageToBuilder("pickaxe", damage.m_pickaxe, _builder);
            AppendDamageToBuilder("total", damage.GetTotalDamage(), _builder);
            return _builder.ToString();
        }

        private static void AppendDamageToBuilder(string name, float damage, StringBuilder builder)
        {
            builder.AppendFormat("{0}:{1:0.0} ", name, damage);
        }

        public static string FormatParameters(string parameters, Vector3 position)
        {
            return string.Format("{0} {1}", position.ToPresentableString(), parameters);
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
