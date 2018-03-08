#define  _64_BIT_
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JSInterface
{
#if _64_BIT_
using INT = System.Int64;
#else
    using INT = System.Int32;
#endif
    using INTPTR = System.IntPtr;
    using M = System.Runtime.InteropServices.Marshal;
    using BindingFlags = System.Reflection.BindingFlags;


    public class JSContextPtr
    {
        protected INTPTR J;
        public JSClass MakeJSClass(string name)
        {
            JSDLL.JSClassDefinition def = new JSDLL.JSClassDefinition()
            {
                Attributes = JSDLL.JSClassAttributes.kJSClassAttributeNone,
                ClassName = name,
                 
            };
            var clz = JSDLL.JSClassCreate(ref def);
            JSClass jsClz = new JSClass(clz);
            return jsClz;
        }

        public INTPTR JSClassCreate(ref JSDLL.JSClassDefinition jsClzDef)
        {
            var clz = JSDLL.JSClassCreate(ref jsClzDef);
            return clz;
        }

        public INTPTR JSMakeObject(INTPTR clz, INTPTR data)
        {
            var obj = JSDLL.JSObjectMake(J, clz, data);
            return obj;
        }

        public INTPTR JSObjectMakeFunctionWithCallback(string name, JSDLL.JSObjectCallAsFunctionCallback cb)
        {
            var funPtr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(cb);
            INTPTR jsname = JSDLL.JSStringCreateWithUTF8CString(name);
            var func = JSDLL.JSObjectMakeFunctionWithCallback(J, jsname, cb);
            return func;
        }

    }
}