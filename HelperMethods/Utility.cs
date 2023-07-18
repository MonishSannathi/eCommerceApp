using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Ecommerce.HelperMethods
{
    public static class Utility
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            return memberExpression?.Member.Name;
        }
    }
}