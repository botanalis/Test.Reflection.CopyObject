using System;

namespace Reflection.CopyObject
{
    /// <summary>
    /// 欄位說明
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class FieldDescContentAttribute: Attribute
    {
        /// <summary>
        /// 說明
        /// </summary>
        public string Desc { get; set; }

        public FieldDescContentAttribute(string desc)
        {
            Desc = desc;
        }
    }
}