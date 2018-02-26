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
                attributes = JSDLL.JSClassAttributes.kJSClassAttributeNone,
                className = name,
                 
            };
            var clz = JSDLL.JSClassCreate(def);
            JSClass jsClz = new JSClass(clz);
            return jsClz;
        }

        public JSClass JSClassCreate(JSDLL.JSClassDefinition def)
        {
            var clz = JSDLL.JSClassCreate(def);
            JSClass jsClz = new JSClass(clz);
            return jsClz;
        }

        public JSObject JSMakeObject()
        {
            return null;
        }
    }
}