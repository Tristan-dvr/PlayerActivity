using HarmonyLib;

namespace PlayerActivity.ActivityPatches
{
    [HarmonyPatch(typeof(Player), nameof(Player.AddAdrenaline))]
    internal class TrinketActivationPatch
    {
        private static bool _hasEffectBefore;

        public static void Prefix(Player __instance)
        {
            var trinket = GetActiveTrinket(__instance);
            _hasEffectBefore = trinket != null
                && HasStatusEffect(__instance, trinket);
        }

        public static void Postfix(Player __instance)
        {
            if (_hasEffectBefore)
            {
                return;
            }

            var trinket = GetActiveTrinket(__instance);
            var hasStatusEffect = trinket != null
                && HasStatusEffect(__instance, trinket);

            if (!_hasEffectBefore && hasStatusEffect)
            {
                ActivityLog.AddLogWithPosition(ActivityEvents.TrinketActivated, trinket.ToPresentableString(), __instance);
            }
        }

        private static ItemDrop.ItemData GetActiveTrinket(Player player)
        {
            foreach (var item in player.GetInventory().GetAllItems())
            {
                if (item != null 
                    && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Trinket
                    && item.m_shared.m_fullAdrenalineSE != null
                    && item.m_shared.m_maxAdrenaline > 0)
                {
                    return item;
                }
            }

            return null;
        }

        private static bool HasStatusEffect(Player player, ItemDrop.ItemData trinket)
        {
            return player.GetSEMan().HaveStatusEffect(trinket.m_shared.m_fullAdrenalineSE.NameHash());
        }
    }
}
