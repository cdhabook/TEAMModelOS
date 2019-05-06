using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TEAMModelOS.SDK.Helper.Common.ValidateHelper
{
    public static class ValidateHelper
    {
        public static string AllowCountry { get; set; }
        /// <summary>
        /// 验证对象是否有效
        /// </summary>
        /// <param name="obj">要验证的对象</param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool IsValid(this object obj, Collection<ValidationResult> validationResults)
        {
            return Validator.TryValidateObject(obj, new ValidationContext(obj, null, null), validationResults, true);
        }

        /// <summary>
        /// 验证对象是否有效
        /// </summary>
        /// <param name="obj">要验证的对象</param>
        /// <returns></returns>
        public static T ValidObj<T>(T value)
        {
            var results = new Collection<ValidationResult>();
            var validationContext = new ValidationContext(value, null, null);
            bool f = Validator.TryValidateObject(value, validationContext, results, true);
            if (f)
            {
                return value;
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 验证对象是否有效
        /// </summary>
        /// <param name="obj">要验证的对象</param>
        /// <returns></returns>
        public static bool IsValid(object value)
        {
            var results = new Collection<ValidationResult>();
            var validationContext = new ValidationContext(value, null, null);
            bool f = Validator.TryValidateObject(value, validationContext, results, true);
            if (!f)
            {
                string s = "";
                foreach (ValidationResult result in results)
                {
                    IEnumerator<string> enumerator = result.MemberNames.GetEnumerator();
                    enumerator.MoveNext();
                    s = s + enumerator.Current + ",";
                }
                throw new Exception(s + "字段不正确");
            }
            return f;
        }
    }
}
