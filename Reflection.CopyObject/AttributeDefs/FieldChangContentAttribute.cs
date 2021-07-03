using System;

namespace Reflection.CopyObject.AttributeDefs
{
    /// <summary>
    /// 欄位檢查
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class FieldChangContentAttribute: Attribute
    {
        /// <summary>
        /// 說明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 驗證結果顯示內容欄位Value
        /// </summary>
        public string ResultValues { get; set; }
        
        public FieldChangContentAttribute(string description, string resultValues = "")
        {
            Description = description;
            ResultValues = resultValues;
        }
    }
}