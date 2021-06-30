using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflection.CopyObject
{
    public static class AnnotationExtensions
    {
        /// <summary>
        /// 取得 Enum Description 說明字串
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetEnumDescriptionText<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.FirstOrDefault()?.Description;
        }
        
        /// <summary>
        /// 取得 Property Description 說明字串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        public static string GetDescriptionText<T,R>(this T obj, Expression<Func<T, R>> value)
        {
            var memberExpression = value.Body as MemberExpression;
            
            var attr = memberExpression.Member.GetCustomAttributes(typeof(DescriptionAttribute), true);
            
            return ((DescriptionAttribute)attr[0]).Description;
        }
    }
}