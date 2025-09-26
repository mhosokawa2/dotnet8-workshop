using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace memory_leak_demo
{
    // メモリリークの例を示すサンプルプログラム
    public static class MemoryLeakExample
    {
        // 静的なリストはアプリケーションのライフタイムを通じて存続する
        private static List<byte[]> _leakyCollection = new List<byte[]>();

        // イベントを発行するシングルトン
        private static EventPublisher _publisher = new EventPublisher();

        public static async Task Main()
        {
            Console.WriteLine("メモリリークのサンプルを実行中...");
            Console.WriteLine("Ctrl+Cで終了してください。");

            // メモリリークの例1: 静的コレクションへの大量データ追加
            Task leakyCollectionTask = RunLeakyCollectionExample();

            // メモリリークの例2: イベントハンドラの未登録解除
            Task leakyEventTask = RunLeakyEventHandlerExample();

            // 両方のタスクが終了するまで待機（実際には無限ループなので終了しない）
            await Task.WhenAll(leakyCollectionTask, leakyEventTask);
        }

        // メモリリークの例1: 静的コレクションに大量のデータを追加し続ける
        private static async Task RunLeakyCollectionExample()
        {
            while (true)
            {
                // 1MBのメモリを確保してリストに追加（解放されない）
                _leakyCollection.Add(new byte[1024 * 1024]);

                Console.WriteLine($"メモリリーク例1: 現在 {_leakyCollection.Count} MB のメモリがリークしています");

                // 実際のアプリケーションの動作をシミュレート
                await Task.Delay(1000);
            }
        }

        // メモリリークの例2: イベントハンドラの未登録解除
        private static async Task RunLeakyEventHandlerExample()
        {
            while (true)
            {
                // 新しいリスナーを作成し、イベントに登録
                var listener = new EventListener();
                _publisher.DataChanged += listener.OnDataChanged;

                // リスナーへの参照を失うが、イベントハンドラは登録されたままになる
                // リスナーはGCで回収されず、メモリリークが発生する

                Console.WriteLine("メモリリーク例2: イベントリスナーがメモリリークしています");

                // イベントを発行してリスナーが動作していることを確認
                _publisher.RaiseEvent();

                await Task.Delay(1500);
            }
        }
    }

    // イベント発行者クラス
    public class EventPublisher
    {
        // このイベントに登録されたリスナーはGCで回収されない可能性がある
        public event EventHandler DataChanged;

        public void RaiseEvent()
        {
            // イベントを発行
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    // イベントリスナークラス
    public class EventListener
    {
        // 大きなデータを保持して、メモリリークの影響を大きくする
        private byte[] _data = new byte[1024 * 1024]; // 1MB

        public void OnDataChanged(object sender, EventArgs e)
        {
            // イベントハンドラの処理
            // 何らかの処理を行う
        }

        // このクラスがGCで回収されると、_dataも回収されるはず
        // しかしイベントハンドラが登録されたままだと回収されない
    }

    // 循環参照によるメモリリークの例
    public class CircularReference
    {
        private sealed class NodeA
        {
            public NodeB RefToB { get; set; }
            // 大きなデータを保持
            private byte[] _data = new byte[1024 * 1024]; // 1MB
        }

        private sealed class NodeB
        {
            public NodeA RefToA { get; set; }
            // 大きなデータを保持
            private byte[] _data = new byte[1024 * 1024]; // 1MB
        }

        public static void CreateCircularReference()
        {
            NodeA a = new NodeA();
            NodeB b = new NodeB();

            // 循環参照を作成
            a.RefToB = b;
            b.RefToA = a;

            // a と b への参照がスコープから出ても、
            // 互いに参照しあっているため、メモリは解放されない
            // （ただし、最近のGCは循環参照を検出して回収できることが多い）
        }
    }
}