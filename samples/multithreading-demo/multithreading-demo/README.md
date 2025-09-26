## サンプル紹介

```text
.
+-- Program.cs － メモリリーク再現プログラム
+-- ImprovedCollection.cs － 静的コレクションのメモリリーク回避
+-- ImprovedEventHandling.cs － イベントハンドラの適切な登録解除
+-- WeakReferenceExample.cs － 弱参照（WeakReference）の活用
+-- ResourceManager － IDisposableとusingステートメントの適切な使用
+-- ImprovedCircularReference.cs － キャッシュと循環参照の適切な管理
```

## メモリリーク再現プログラム実行方法


```powershell
cd dotnet8-workshop\samples\memory-leak-demo\memory-leak-demo
dotnet run --configuration Debug 
```

※ ディレクトリは利用者側で適宜合わせてください。
