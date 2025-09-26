namespace memory_leak_demo
{
    // 改善バージョン: キャッシュ制限付きコレクション
    public static class ImprovedCollection
    {
        // 静的コレクションに上限を設ける
        private const int MAX_ITEMS = 100;
        private static List<byte[]> _managedCollection = new List<byte[]>();

        public static void AddItem(byte[] item)
        {
            // コレクションがいっぱいなら古いアイテムを削除
            if (_managedCollection.Count >= MAX_ITEMS)
            {
                _managedCollection.RemoveAt(0);
            }

            _managedCollection.Add(item);
            Console.WriteLine($"管理されたコレクション: 現在 {_managedCollection.Count} アイテム");
        }

        // 不要になったアイテムを明示的に削除するメソッド
        public static void RemoveItem(int index)
        {
            if (index >= 0 && index < _managedCollection.Count)
            {
                _managedCollection.RemoveAt(index);
            }
        }

        // コレクションをクリアするメソッド
        public static void ClearCollection()
        {
            _managedCollection.Clear();
        }
    }
}
