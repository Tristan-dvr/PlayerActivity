using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace PlayerActivity
{
    class ActivityStorageService : MonoBehaviour
    {
        public const string LogRpc = "RPC_PlayerActivityLog";

        internal static ActivityStorageService Instance { get; private set; }

        private IDisposable _writerThread;
        private ConcurrentDictionary<string, ConcurrentQueue<string>> _logsToWrite = new ConcurrentDictionary<string, ConcurrentQueue<string>>();

        private void Start()
        {
            Instance = this;
            ZRoutedRpc.instance.Register<LogData>(LogRpc, OnPlayerLogsReceived);

            _writerThread = ThreadingUtil.RunPeriodicalInSingleThread(WriteQueuedLogsToDisk, 500);
            Log.Info("Storage initialized");
        }

        private void OnDestroy()
        {
            _writerThread?.Dispose();
        }

        private void OnPlayerLogsReceived(long uid, LogData logData)
        {
            var peer = ZNet.instance.GetPeer(uid);
            var steamId = peer?.m_socket.GetHostName() ?? "local";

            AppendPlayerLogs(steamId, logData);
        }

        internal void AppendPlayerLogs(string steamId, LogData logData)
        {
            var root = Utils.GetSaveDataPath(FileHelpers.FileSource.Local);
            var filePath = Path.Combine(root, 
                Plugin.Name,
                GetCurrentDateFolderPath(),
                $"{steamId}.log");
            if (!_logsToWrite.TryGetValue(filePath, out var queue))
            {
                _logsToWrite.AddOrUpdate(filePath, new ConcurrentQueue<string>(), (path, q2) => _logsToWrite[path]);
                queue = _logsToWrite[filePath];
            }
            var text = FormatLogToDisk(logData);
            queue.Enqueue(text);
        }

        private void WriteQueuedLogsToDisk()
        {
            foreach (var logEntity in _logsToWrite)
            {
                if (logEntity.Value.IsEmpty)
                    continue;

                var directory = Path.GetDirectoryName(logEntity.Key);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (var writer = File.AppendText(logEntity.Key))
                {
                    while (logEntity.Value.TryDequeue(out var log))
                    {
                        writer.WriteLine(log);
                    }
                }
            }
        }

        private string FormatLogToDisk(LogData logData)
        {
            var date = CalculateRelativeDate(ZNet.instance.GetTimeSeconds(), logData.time);
            return $"[{date.ToString("G", CultureInfo.InvariantCulture)}] {logData}";
        }

        private static DateTime CalculateRelativeDate(double currentWorldTime, double lastWorldTime)
        {
            var now = DateTime.UtcNow;
            var relativeSeconds = currentWorldTime - lastWorldTime;
            return now.Subtract(TimeSpan.FromSeconds(relativeSeconds));
        }
        private static string GetCurrentDateFolderPath()
        {
            return DateTime.UtcNow.ToString("yyyy_MM_dd", CultureInfo.InvariantCulture);
        }
    }
}
