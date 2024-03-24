using System.Collections.Generic;
using UnityEngine;

namespace PlayerActivity
{
    class ActivityLogger : MonoBehaviour
    {
        private const int LogsPackageCount = 20;

        public static ActivityLogger Instance { get; private set; }

        private Queue<string> _logsQueue = new Queue<string>();
        private ZPackage _package = new ZPackage();

        private void Awake()
        {
            Instance = this;

            InvokeRepeating(nameof(Flush), 30, 30);
            InvokeRepeating(nameof(FlushLogsIfNeeded), 1, 1);
            Log.Info("Activity logger initialized");
        }

        public void AddLog(string log)
        {
            if (ZNet.instance.IsDedicated()) return;

            Log.Debug(log);
            log = ActivityLoggerUtil.AddDateToLog(log);

            _logsQueue.Enqueue(log);
        }

        internal void Flush()
        {
            if (_logsQueue.Count == 0) return;

            _package.Clear();
            _package.Write(_logsQueue.Count);
            foreach (var log in _logsQueue)
            {
                _package.Write(log);
            }
            Log.Debug($"Sent {_logsQueue.Count} user logs. Size: {_package.Size()}");
            _logsQueue.Clear();

            ZRoutedRpc.instance?.InvokeRoutedRPC(ActivityStorageService.LogsRpc, _package);
        }

        private void FlushLogsIfNeeded()
        {
            if (_logsQueue.Count < LogsPackageCount) return;

            Flush();
        }
    }
}
