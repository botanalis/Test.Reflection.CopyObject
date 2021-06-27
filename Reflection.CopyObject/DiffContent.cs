namespace Reflection.CopyObject
{
    /// <summary>
    /// 異動內容
    /// </summary>
    public class DiffContent
    {
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 說明內容
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Option { get; set; }
        /// <summary>
        /// 原始設定
        /// </summary>
        public string BeforeValue { get; set; }
        /// <summary>
        /// 異動設定
        /// </summary>
        public string AfterValue { get; set; }
    }
}