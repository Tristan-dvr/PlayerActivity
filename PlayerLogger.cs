using UnityEngine;

namespace PlayerActivity
{
    class PlayerLogger : MonoBehaviour
    {
        private Player _player;

        private void Start()
        {
            if (ZNet.instance.IsDedicated()) return;

            _player = GetComponent<Player>();

            InvokeRepeating(nameof(AddPeriodicallyLogs), 60, 60);
            InvokeRepeating(nameof(AddInventoryLog), 1, 600);

            Log.Info("Player logger created");
        }

        private void AddPeriodicallyLogs()
        {
            if (_player.IsDead()) return;

            ZNet.instance.GetNetStats(out _, out _, out var ping, out _, out _);
            ActivityLog.AddLogWithPosition(ActivityEvents.Ping, ping.ToString(), _player);
        }

        private void AddInventoryLog()
        {
            if (_player.IsDead()) return;

            ActivityLog.AddLogWithPosition(ActivityEvents.Inventory, $"\n{ActivityLoggerUtil.GetContainerData(_player.GetInventory())}", _player);
        }
    }
}
