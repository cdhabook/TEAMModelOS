using System;
using System.Collections;
using System.Collections.Generic;

using System.Text;

namespace TEAMModelOS.SDK.Helper.Common.CollectionHelper
{
    public static class CollectionHelper
    {
        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsEmpty(this ICollection collection)
        {
            if (collection != null && collection.Count > 0)
            {
                return false;
            }
            else {
                return true;
            }
        }
        /// <summary>
        /// 判断集合是否不为空
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this ICollection collection)
        {
            if (collection != null && collection.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
