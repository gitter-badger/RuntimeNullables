﻿using Mono.Cecil;

namespace RuntimeNullables.Fody.Extensions
{
    internal static class TypeReferenceExtensions
    {
        public static bool IsReferenceType(this TypeReference type)
        {
            if (type.IsPointer)
                return true;

            type = type.GetElementType();
            return !(type.IsValueType || (type is GenericParameter param && param.HasNotNullableValueTypeConstraint));
        }

        public static bool IsNonGenericTaskType(this TypeReference type)
        {
            string elementTypeName = type.GetElementType()?.FullName;
            return elementTypeName == "System.Threading.Tasks.Task" || elementTypeName == "System.Threading.Tasks.ValueTask";
        }

        public static bool IsTaskWithResultType(this TypeReference type)
        {
            string elementTypeName = type.GetElementType()?.FullName;
            return elementTypeName == "System.Threading.Tasks.Task`1" || elementTypeName == "System.Threading.Tasks.ValueTask`1";
        }

        public static bool IsIEnumerableType(this TypeReference type)
        {
            return type.GetElementType()?.FullName == "System.Collections.Generic.IEnumerable`1";
        }

        public static bool IsIAsyncEnumerableType(this TypeReference type)
        {
            return type.GetElementType()?.FullName == "System.Collections.Generic.IAsyncEnumerable`1";
        }
    }
}
