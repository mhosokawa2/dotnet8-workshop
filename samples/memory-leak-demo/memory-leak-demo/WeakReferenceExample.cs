namespace memory_leak_demo
{
    public class WeakReferenceExample
    {
        // 弱参照を使用することでGCがオブジェクトを回収できるようにする
        private WeakReference<EventListener> _weakListener;

        public void CreateListener()
        {
            var listener = new EventListener();
            _weakListener = new WeakReference<EventListener>(listener);

            // このスコープを出ると、listenerへの強参照がなくなる
            // _weakListenerはlistenerへの弱参照のみを持つので、GCで回収される可能性がある
        }

        public void UseListener()
        {
            // 弱参照からオブジェクトを取得（既に回収されている可能性あり）
            if (_weakListener.TryGetTarget(out EventListener listener))
            {
                // listenerが有効な場合の処理
                listener.OnDataChanged(this, EventArgs.Empty);
            }
            else
            {
                // listenerが既に回収されている場合の処理
                Console.WriteLine("リスナーは既にGCで回収されています");
            }
        }
    }
}
