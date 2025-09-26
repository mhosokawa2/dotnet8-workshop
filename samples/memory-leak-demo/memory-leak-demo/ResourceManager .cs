namespace memory_leak_demo
{
    public class ResourceManager : IDisposable
    {
        private bool _disposed = false;
        private FileStream _fileStream;

        public ResourceManager()
        {
            // リソースの初期化
            _fileStream = new FileStream("temp.dat", FileMode.Create);
        }

        // リソースを使用するメソッド
        public void UseResources()
        {
            // リソースを使った処理
        }

        // Disposeパターンの実装
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // マネージドリソースの解放
                    _fileStream?.Dispose();
                }

                // アンマネージドリソースの解放（必要な場合）

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ResourceManager()
        {
            Dispose(false);
        }

        // usingステートメントを使った例
        public void SafeResourceUsage()
        {
            // usingブロックを抜けると自動的にDisposeが呼ばれる
            using (var manager = new ResourceManager())
            {
                manager.UseResources();
            }
            // ここでmanager.Dispose()が自動的に呼ばれる
        }
    }
}

