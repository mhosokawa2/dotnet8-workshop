namespace multithreading_demo
{
    public static class Program
    {
        // 共有されるカウンタ変数
        private static int _counter = 0;

        static async Task Main(string[] args)
        {
            Console.WriteLine("競合状態（Race Condition）のデモンストレーション");
            Console.WriteLine("================================================\n");

            // 競合状態が発生するケース
            await DemonstrateRaceCondition();

            // 修正版：lockを使った排他制御
            await DemonstrateThreadSafeSolution();

            Console.WriteLine("\nプログラム終了。Enterキーを押してください...");
            Console.ReadLine();
        }

        static async Task DemonstrateRaceCondition()
        {
            Console.WriteLine("1. 競合状態のあるコード実行：");

            // カウンタをリセット
            _counter = 0;

            // 複数のタスクを作成
            var tasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                tasks.Add(Task.Run(() => IncrementCounterUnsafe()));
            }

            // すべてのタスクが完了するのを待つ
            await Task.WhenAll(tasks);

            // 予想される結果は 5 * 1000 = 5000 だが、実際はそれより少ない値になる可能性が高い
            Console.WriteLine($"カウンタの最終値: {_counter} (期待値: 5000)\n");
        }

        static void IncrementCounterUnsafe()
        {
            // 1000回カウンタをインクリメント
            for (int i = 0; i < 1000; i++)
            {
                // ここで競合状態が発生！
                // 1. 現在の値を読み取る
                // 2. 1を加算
                // 3. 結果を書き込む
                // 別のスレッドが同時に実行すると、更新が失われる可能性がある
                _counter++;

                // スレッドのスイッチングを促進するために、少しランダムな遅延を入れる
                if (i % 100 == 0)
                {
                    Thread.Sleep(new Random().Next(1, 5));
                }
            }
        }

        static async Task DemonstrateThreadSafeSolution()
        {
            Console.WriteLine("2. 排他制御を使用した安全なコード実行：");

            // カウンタをリセット
            _counter = 0;

            // ロック用のオブジェクト
            object _lock = new object();

            // 複数のタスクを作成
            var tasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    // 1000回カウンタをインクリメント（ロック使用）
                    for (int j = 0; j < 1000; j++)
                    {
                        // ロックを取得してからカウンタを操作
                        lock (_lock)
                        {
                            _counter++;
                        }

                        // 同じ遅延を入れる
                        if (j % 100 == 0)
                        {
                            Thread.Sleep(new Random().Next(1, 5));
                        }
                    }
                }));
            }

            // すべてのタスクが完了するのを待つ
            await Task.WhenAll(tasks);

            // 今回は正確に5000になるはず
            Console.WriteLine($"カウンタの最終値: {_counter} (期待値: 5000)");
        }
    }
}