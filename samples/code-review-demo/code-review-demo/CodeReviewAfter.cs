namespace code_review_demo
{
    /// <summary>
    /// コードレビュー後のサンプルクラス
    /// </summary>
    public static class CodeReviewAfter
    {

        /// <summary>
        /// エラーを示すキーワード
        /// </summary>
        private const string ErrorKeyword = "error";

        /// <summary>
        /// 文字列データのリストを処理し、小文字に変換して現在のタイムスタンプを追加します。
        /// エラーを含む項目が見つかった場合は処理を中止します。
        /// </summary>
        /// <param name="dataItems">処理する文字列のリスト</param>
        /// <exception cref="ArgumentNullException">dataItems が null の場合</exception>
        public static void ProcessData(List<string?> dataItems)
        {
            // null チェックおよび空リスト判定で堅牢性を確保
            if (dataItems == null || dataItems.Count == 0)
            {
                throw new ArgumentNullException(nameof(dataItems), "データリストは null であってはなりません。");
            }

            foreach (string? item in dataItems)
            {
                if (string.IsNullOrEmpty(item) || item.Contains(ErrorKeyword, StringComparison.OrdinalIgnoreCase))
                {
                    // 空文字列またはエラー項目の場合は処理を中止
                    break;
                }
                var processedItem = FormatItem(item);
                Console.WriteLine($"処理結果: {processedItem}");
            }
        }

        /// <summary>
        /// 文字列アイテムをフォーマットします。
        /// </summary>
        /// <param name="item">フォーマットする文字列</param>
        /// <returns>フォーマットされた文字列</returns>
        private static string FormatItem(string item)
        {
            string trimmedItem = item.Trim();
            string lowerItem = trimmedItem.ToLowerInvariant();
            // ISO 8601 形式で日付を追加し、明確で一貫したフォーマットを確保（システムのタイムゾーンを使用）
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");

            return $"{lowerItem} ({timestamp})";
        }

    }
}
