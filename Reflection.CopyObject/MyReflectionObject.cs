using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic;
using Reflection.CopyObject.AttributeDefs;

namespace Reflection.CopyObject
{
    public class MyReflectionObject : IReflectionObject
    {
        public R CopyObject<T, R>(T srcObj, R destObj)
        {
            
            Type destObjType = destObj.GetType();

            R resultObj = (R) Activator.CreateInstance(destObjType);

            Type srcObjType = srcObj.GetType();

            //取得Object 所有 Property 資訊
            PropertyInfo[] destPropertys = GetPropertyInfos(destObjType);

            foreach (PropertyInfo propertyInfo in destPropertys)
            {
                object? value = null;
                var templatePropertyType = propertyInfo.PropertyType;
                var srcPropertyValue = srcObjType.GetProperty(propertyInfo.Name)?.GetValue(srcObj, null);

                //判斷類型
                if (templatePropertyType == typeof(string) ||
                    templatePropertyType == typeof(int) ||
                    templatePropertyType == typeof(decimal) ||
                    templatePropertyType == typeof(bool))
                {
                    value = srcPropertyValue;
                }
                // 判斷集合
                else if (IsListObject(templatePropertyType))
                {
                    // 資料來源
                    var srcObjlist = (IList) srcPropertyValue;
                    //取得清單
                    value = GetCollection(srcObjlist, propertyInfo);
                }

                // 設置Value
                propertyInfo.SetValue(resultObj, value, null);
            }

            return resultObj;
        }

        public List<DiffContent> GetDiffContents<T>(T newObj, T oldObj)
        {
            List<DiffContent> diffResult = new List<DiffContent>();

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                var oOldValue = propertyInfo.GetValue(oldObj, null);
                var oNewValue = propertyInfo.GetValue(newObj, null);

                var verifyChangContent = propertyInfo.GetCustomAttributes(typeof(FieldChangContentAttribute), false);
                // 取得比對參數
                var verifyParam = (FieldChangContentAttribute) verifyChangContent.FirstOrDefault();
                
                if (verifyParam == null)
                {
                    //不比對
                    continue;
                }
                
                if (oOldValue == null && oNewValue == null)
                {
                    continue;
                }
                if (!Object.Equals(oOldValue, oNewValue))
                {
                    // 取得說明內容
                    var desc = verifyParam.Description;
                    var fieldNames = verifyParam.ResultValues;

                    var oldValue = this.GetProptryValue(oldObj, fieldNames);
                    var newValue = this.GetProptryValue(newObj, fieldNames);

                    var diffContent = new DiffContent();
                    diffContent.Option = "Modify";
                    diffContent.BeforeValue = String.IsNullOrEmpty(oldValue) ? $"{oOldValue}" : oldValue;
                    diffContent.AfterValue = String.IsNullOrEmpty(newValue) ? $"{oNewValue}" : newValue;
                    diffContent.FieldName = propertyInfo.Name;
                    diffContent.Description = desc;

                    diffResult.Add(diffContent);

                }
            }

            return diffResult;
        }
        
        /// <summary>
        /// 取得 Proptry Value
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="fieldNames">欄位名稱</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetProptryValue<T>(T obj, string fieldNames)
        {
            List<string> result = new List<string>();

            if (String.IsNullOrEmpty(fieldNames))
            {
                return "";
            }
            
            var fieldNameList = fieldNames.Split(",");

            foreach (string fieldName in fieldNameList)
            {
                var value = typeof(T).GetProperty(fieldName)?.GetValue(obj, null);
                if (value != null)
                {
                    if (String.IsNullOrEmpty($"{value}"))
                    {
                        continue;
                    }
                    
                    result.Add($"{value}");
                }
            }

            if (result.Count > 0)
            {
                return String.Join($"/", result.ToArray());
            }
            
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="elementProperty"></param>
        /// <returns></returns>
        private IList GetCollection(IList items, PropertyInfo elementProperty)
        {
            // 樣板
            object templateElementObj;
            //建立集合
            IList list = GenericList(elementProperty, out templateElementObj);

            foreach (object item in items)
            {
                var elementObj = CopyObject(item, templateElementObj);
                list.Add(elementObj);
            }

            return list;
        }

        /// <summary>
        /// 取得 All Object Property
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
        private PropertyInfo[] GetPropertyInfos(Type objType)
        {
            return objType.GetProperties();
        }

        /// <summary>
        /// 產生 List Object
        /// </summary>
        /// <param name="property">Object Property</param>
        /// <param name="elementObj">element object</param>
        /// <returns></returns>
        private static IList GenericList(PropertyInfo property, out Object elementObj)
        {
            // 取得 List element object type
            var genericType = property.PropertyType.GetGenericArguments().First();
            // element Object
            elementObj = Activator.CreateInstance(genericType);
            // 建立 List Object
            return (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));
        }


        /// <summary>
        /// 判斷是否為 List Object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static bool IsListObject(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (typeof(System.Collections.IList).IsAssignableFrom(type))
            {
                return true;
            }

            foreach (var it in type.GetInterfaces())
            {
                if (it.IsGenericType && typeof(IList<>) == it.GetGenericTypeDefinition())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 建立實例 Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T GetInstance<T>()
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
    }
}