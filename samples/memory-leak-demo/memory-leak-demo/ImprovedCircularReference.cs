namespace memory_leak_demo
{
    // 循環参照問題の解決例
    public class ImprovedCircularReference
    {
        // 親→子の参照はあるが、子→親の参照は弱参照にする
        private class Parent
        {
            public Child Child { get; set; }
            private byte[] _data = new byte[1024 * 1024]; // 1MB
        }

        private class Child
        {
            // 親への参照を弱参照にすることで循環参照問題を解決
            public WeakReference<Parent> ParentRef { get; set; }
            private byte[] _data = new byte[1024 * 1024]; // 1MB

            public void DoSomethingWithParent()
            {
                if (ParentRef.TryGetTarget(out Parent parent))
                {
                    // 親オブジェクトが有効な場合の処理
                }
            }
        }

        public static void CreateSafeReferences()
        {
            Parent parent = new Parent();
            Child child = new Child();

            // 参照を設定
            parent.Child = child;
            child.ParentRef = new WeakReference<Parent>(parent);

            // parent変数がスコープを出ると、Parent→Childの強参照があっても
            // Child→Parentは弱参照なので、GCが循環を検出して両方回収できる
        }
    }
}
