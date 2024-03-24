using System;
using System.Collections.Concurrent;
using System.IO;
using UnityEngine;

namespace PlayerActivity
{
    class ActivityStorageService : MonoBehaviour
    {
        public const string LogsRpc = "RPC_PlayerActivityLogs";

        internal static ActivityStorageService Instance { get; private set; }

        private IDisposable _writerThread;
        private ConcurrentDictionary<string, ConcurrentQueue<string>> _logsToWrite = new ConcurrentDictionary<string, ConcurrentQueue<string>>();

        private void Start()
        {
            Instance = this;
            ZRoutedRpc.instance.Register<ZPackage>(LogsRpc, OnPlayerLogsReceived);

            _writerThread = ThreadingUtil.RunPeriodicalInSingleThread(WriteQueuedLogsToDisk, 500);
            Log.Info("Storage initialized");
        }

        private void OnDestroy()
        {
            _writerThread?.Dispose();
        }

        private void OnPlayerLogsReceived(long uid, ZPackage package)
        {
            var peer = ZNet.instance.GetPeer(uid);
            var steamId = peer?.m_socket.GetHostName() ?? "local";

            var logsCount = package.ReadInt();
            for (int i = 0; i < logsCount; i++)
                AppendPlayerLogs(steamId, package.ReadString());
        }

        internal void AppendPlayerLogs(string steamId, string message)
        {
            var root = Utils.GetSaveDataPath(FileHelpers.FileSource.Local);
            var filePath = Path.Combine(root, 
                Plugin.Name,
                ActivityLoggerUtil.GetCurrentDateText("yyyy_MM_dd"),
                $"{steamId}.log");
            if (!_logsToWrite.TryGetValue(filePath, out var queue))
            {
                queue = new ConcurrentQueue<string>();
                _logsToWrite.AddOrUpdate(filePath, queue, (path, q2) => _logsToWrite[path]);
            }
            queue.Enqueue(message);
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
                        writer.WriteLine(log);
                }
            }
        }
    }
}
