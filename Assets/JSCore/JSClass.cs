#define _64_BIT_
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if _64_BIT_
using INT = System.Int64;
#else
using INT = System.Int32;
#endif
using INTPTR = System.IntPtr;

namespace JSInterface
{
    /// <summary>
    /// JSClass in C#
    /// </summary>
    public class JSClass
    {
        INTPTR clz = INTPTR.Zero;


        public JSClass(INTPTR clzPtr)
        {
            clz = clzPtr;
        }


    }

}