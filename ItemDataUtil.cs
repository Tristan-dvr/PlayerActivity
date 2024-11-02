using System.Collections.Generic;
using System.Text;

namespace PlayerActivity
{
    public static class ItemDataUtil
    {
        const string CustomDataValueFormat = "[{0}]:[{1}]";

        private static StringBuilder _builder = new StringBuilder();
        private static StringBuilder _itemDataBuilder = new StringBuilder();

        public static string ToPresentableString(this ItemDrop.ItemData item)
        {
            return ToPresentableString(item, item.m_stack);
        }

        public static string ToPresentableString(this ItemDrop.ItemData item, int count)
        {
            var itemName = item.m_dropPrefab?.name ?? item.m_shared.m_name;
            _itemDataBuilder.Clear();
            _itemDataBuilder.AppendFormat("{0} x{1} quality:{2}", itemName, count, item.m_quality);
            if (item.m_crafterID != 0)
            {
                _itemDataBuilder.AppendFormat(" crafter:{0}({1})", item.m_crafterName, item.m_crafterID);
            }
            if (item.m_customData.Count > 0)
            {
                _itemDataBuilder.AppendFormat(" data:{0}", ToPresentableFormat(item.m_customData));
            }
            return _itemDataBuilder.ToString();
        }

        private static string ToPresentableFormat(Dictionary<string, string> customData)
        {
            _builder.Clear();
            var appendWhiteSpace = false;
            foreach (var pair in customData)
            {
                if (appendWhiteSpace) _builder.Append(", ");

                _builder.AppendFormat(CustomDataValueFormat, pair.Key, pair.Value);

                appendWhiteSpace = true;
            }
            return _builder.ToString();
        }
    }
}
