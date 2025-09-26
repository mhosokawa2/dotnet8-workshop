// 改善バージョン: イベントハンドラの登録・解除
using memory_leak_demo;

public class ImprovedEventHandling
{
    private EventPublisher _publisher;
    private EventListener _listener;

    public ImprovedEventHandling(EventPublisher publisher)
    {
        _publisher = publisher;
        _listener = new EventListener();
    }

    public void StartListening()
    {
        // イベントハンドラを登録
        _publisher.DataChanged += _listener.OnDataChanged;
    }

    public void StopListening()
    {
        // 明示的にイベントハンドラを解除
        _publisher.DataChanged -= _listener.OnDataChanged;
    }

    // IDisposableを実装してリソース解放を確実に
    public void Dispose()
    {
        // イベントハンドラを解除
        StopListening();

        // その他のリソース解放処理
    }
}

// イベントリスナーの改善版
public class EventListener : IDisposable
{
    private byte[] _data = new byte[1024 * 1024]; // 1MB
    private bool _disposed = false;

    public void OnDataChanged(object sender, EventArgs e)
    {
        // イベント処理
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            // リソースを解放
            _data = null;
            _disposed = true;
        }
    }
}
