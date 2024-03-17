using System.Collections.Generic;
using System.Text;

namespace PlayerActivity
{
    public static class ItemDataUtil
    {
        const string ItemFormat = "{0} x{1} qualily:{2} crafter:{3}({4}) data:{5}";
        const string CustomDataValueFormat = "[{0}]:[{1}]";

        private static StringBuilder _builder = new StringBuilder();

        public static string ToPresentableString(this ItemDrop.ItemData item)
        {
            return ToPresentableString(item, item.m_stack);
        }

        public static string ToPresentableString(this ItemDrop.ItemData item, int count)
        {
            var itemName = item.m_dropPrefab?.name ?? item.m_shared.m_name;
            return string.Format(ItemFormat,
                itemName, count, item.m_quality,
                item.m_crafterName, item.m_crafterID,
                ToPresentableFormat(item.m_customData));
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
