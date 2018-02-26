using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JSInterface
{


    public static class Util
    {
        public static bool IsValueType(Type t)
        {
            return !t.IsEnum && t.IsValueType;
        }
    }

}