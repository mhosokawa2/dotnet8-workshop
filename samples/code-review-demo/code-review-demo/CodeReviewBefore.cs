namespace code_review_demo
{
    /// <summary>
    /// コードレビュー前のサンプルクラス
    /// </summary>
    public class CodeReviewBefore
    {
        // データを処理するメソッド
        public void process(List<string?> data)
        {
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var s = data[i];
                    if (s.Length > 0)
                    {
                        s = s.Trim();
                        var processed = s.ToLower() + DateTime.Now;
                        Console.WriteLine("処理結果: " + processed);
                        if (processed.Contains("error")) return;
                    }
                }
            }
        }
    }
}
