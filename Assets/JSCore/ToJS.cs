#define _64_BIT_
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSInterface;

#if _64_BIT_
using INT = System.Int64;
#else
using INT = System.Int32;
#endif
using INTPTR = System.IntPtr;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
using UFP = System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute;
#else
#endif
using M = System.Runtime.InteropServices.Marshal;
using BindingFlags = System.Reflection.BindingFlags;


public class ToJS : MonoBehaviour {
    //int elementSize = M.SizeOf(typeof(INTPTR));

    static INTPTR lastException = M.AllocHGlobal(M.SizeOf(typeof(INTPTR)));
    public static INTPTR LastException
    {
        get
        {
            return lastException;
        }
    }
    static INTPTR gtx = INTPTR.Zero;
    public static INTPTR Gtx
    {
        get { return gtx; }
    }

    static INTPTR gobj = INTPTR.Zero;
    public static INTPTR GObj
    {
        get
        {
            if (gobj == INTPTR.Zero)
                gobj = JSDLL.JSContextGetGlobalObject(gtx);
            return gobj;
        }
    }

    ~ToJS()
    {
        M.FreeHGlobal(lastException);
    }

    JSDLL.JSClassDefinition DefaultClz = new JSDLL.JSClassDefinition()
    {
        attributes = JSDLL.JSClassAttributes.kJSClassAttributeNoAutomaticPrototype,
        className = "DefaultClz"
    };

    static Dictionary<int, object> cachedObject = new Dictionary<int, object>();

    public ToJS()
    {
        JSDLL.JSClassDefinition GlobalContextClz = new JSDLL.JSClassDefinition()
        {
            attributes = JSDLL.JSClassAttributes.kJSClassAttributeNoAutomaticPrototype,
            className = "GlobalContext"
        };
        
        INTPTR clz = JSDLL.JSClassCreate(GlobalContextClz);
        gtx = JSDLL.JSGlobalContextCreate(clz);
    }


    public void PrintLastException(INTPTR ctx)
    {
        INTPTR p = M.ReadIntPtr(lastException);
        if (p != INTPTR.Zero)
        {
            JSDLL.JSType t = JSDLL.JSType.kJSTypeUndefined;
            t = JSDLL.JSValueGetType(ctx, p);
            if (t != JSDLL.JSType.kJSTypeUndefined)
            {
                print(t.ToString());
                if (t != JSDLL.JSType.kJSTypeNull)
                {
                    var eStr = JSDLL.JSValueToStringCopy(ctx, p, lastException);
                    var eStrPtr = JSDLL.JSStringGetCharactersPtr(eStr);
                    var eStrLen = JSDLL.JSStringGetLength(eStr);
                    var eString = M.PtrToStringAuto(eStrPtr, eStrLen);
                    print(eString);
                }

            }
        }
        M.WriteIntPtr(lastException, INTPTR.Zero);
    }

    public Dictionary<System.Type, JSDLL.JSClassDefinition> bindedDef = new Dictionary<System.Type, JSDLL.JSClassDefinition>();
    public Dictionary<System.Type, INTPTR> bindedClaz = new Dictionary<System.Type, INTPTR>();

    [ContextMenu("BindTypes")]
    public void BindTypes()
    {
        M.WriteIntPtr(lastException, INTPTR.Zero);

        var gobj = JSDLL.JSContextGetGlobalObject(gtx);
        System.Reflection.BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase;


        JSDLL.JSClassDefinition CommonClass = new JSDLL.JSClassDefinition()
        {
            attributes = JSDLL.JSClassAttributes.kJSClassAttributeNoAutomaticPrototype,
            className = "CommonClass"
        };

        System.Type type = typeof(UnityEngine.GameObject);
        INTPTR clz = INTPTR.Zero;

        var nodes = type.FullName.Split('.');
        CommonClass.className = nodes[nodes.Length - 1];

        INTPTR obj = gobj;
        for (int i = 0, count = nodes.Length; i < count - 1; i++)
        {
            bool hasProperty = false;
            var node = nodes[i];
            INTPTR propertyNamePtr = JSDLL.JSStringCreateWithUTF8CString(node);
            hasProperty = JSDLL.JSObjectHasProperty(gtx, obj, propertyNamePtr);
            if (!hasProperty)
            {
                var o = JSDLL.JSObjectMake(gtx, JSDLL.JSClassCreate(DefaultClz), INTPTR.Zero);
                JSDLL.JSObjectSetProperty(gtx, obj, propertyNamePtr, o, JSDLL.JSPropertyAttributes.kJSPropertyAttributeNone, lastException);
                obj = o;
            }
            else
            {
                obj = JSDLL.JSObjectGetProperty(gtx, obj, propertyNamePtr, lastException);
            }
        }



        var constructors = type.GetConstructors(bindingFlags);
        foreach (var c in constructors)
        {
            if (c.GetParameters().Length == 1)
            {
                CommonClass.callAsConstructor = delegate (INTPTR ctx, INTPTR constructor, INT argumentCount, INTPTR arguments, INTPTR exception)
                {
                    print("Constructor fun called...");

                    //c.Invoke(new object[] { M.PtrToStringAuto(JSStringGetCharactersPtr(arguments)) });
                    var arg = M.ReadIntPtr(arguments, 0);
                    var jsStr = JSDLL.JSValueToStringCopy(gtx, arg, lastException);
                    var jsStrLen = JSDLL.JSStringGetLength(jsStr);
                    var charPtr = JSDLL.JSStringGetCharactersPtr(jsStr);
                    string gName = M.PtrToStringAuto(charPtr, jsStrLen);
                    print(gName);
                    var go = new GameObject(gName);


                    var t = type;
                    var cz = bindedClaz[t];
                    var o = JSDLL.JSObjectMake(gtx, cz, INTPTR.Zero);

                    cachedObject[go.GetHashCode()] = go;
                    var goValue = JSDLL.JSValueMakeNumber(gtx, go.GetHashCode());

                    //var goHandle = System.Runtime.InteropServices.GCHandle.Alloc(go, System.Runtime.InteropServices.GCHandleType.Weak);
                    //var goPtr = System.Runtime.InteropServices.GCHandle.ToIntPtr(goHandle);
                    //var goValue = JSValueMakeNumber(gtx, goPtr.ToInt64());

                    //var hr = new System.Runtime.InteropServices.HandleRef(go, goPtr);
                    //print("handleRef addr:" + ((INTPTR)hr).ToInt64());

                    JSDLL.JSObjectSetProperty(gtx, o, JSDLL.JSStringCreateWithUTF8CString("TargetRefId"), goValue, JSDLL.JSPropertyAttributes.kJSPropertyAttributeNone, lastException);
                    print(o.ToInt32());
                    //print(goPtr.ToInt32());
                    return o;
                };

                CommonClass.finalize = delegate (INTPTR objPtr)
                {
                    print("On finalize...");
                    //var targetRefId = JSObjectGetProperty(gtx, objPtr, JSStringCreateWithUTF8CString("TargetRefId"), lastException);
                    //var gcHandle = System.Runtime.InteropServices.GCHandle.FromIntPtr(targetRefId);
                    //gcHandle.Free();
                };
            }
        }

        bindedDef[type] = CommonClass;

        clz = JSDLL.JSClassCreate(CommonClass);

        bindedClaz[type] = clz;

        var obj1 = JSDLL.JSObjectMake(gtx, clz, INTPTR.Zero);
        JSDLL.JSValueProtect(gtx, obj1);

        JSDLL.JSObjectSetProperty(gtx, obj, JSDLL.JSStringCreateWithUTF8CString(CommonClass.className), obj1, JSDLL.JSPropertyAttributes.kJSPropertyAttributeReadOnly, lastException);

        JSDLL.JSValueUnprotect(gtx, obj1);

        var fun = JSDLL.JSObjectMakeFunctionWithCallback(gtx, JSDLL.JSStringCreateWithUTF8CString("Print"), delegate (INTPTR ctx, INTPTR function, INTPTR thisObject, INT argumentCount, INTPTR arguments, INTPTR exception)
        {
            //var jsErrStr = JSValueMakeString(gtx, JSStringCreateWithUTF8CString("TestError throw exception from c# to js"));

            //var err = JSObjectMakeError(gtx, 1, new INTPTR[1] { jsErrStr }, lastException);
            //M.WriteIntPtr(exception, err);
            var res = JSDLL.JSValueMakeString(gtx, JSDLL.JSStringCreateWithUTF8CString("hello"));
            print("Function fun called...");
            return res;
        });

        var funPrintGoName = JSDLL.JSObjectMakeFunctionWithCallback(gtx, JSDLL.JSStringCreateWithUTF8CString("PrintGo"), delegate (INTPTR ctx, INTPTR function, INTPTR thisObject, INT argumentCount, INTPTR arguments, INTPTR exception)
        {
            if (argumentCount == 1)
            {
                var arg = M.ReadIntPtr(arguments, 0);
                print(arg.ToInt32());
                var o = JSDLL.JSValueToObject(gtx, arg, lastException);
                if (o != null)
                {
                    var targetRefId = JSDLL.JSObjectGetProperty(gtx, o, JSDLL.JSStringCreateWithUTF8CString("TargetRefId"), lastException);
                    int hashCode = (int)targetRefId;//(int)JSValueToNumber(gtx,targetRefId, lastException);
                    GameObject go = null;
                    if (cachedObject.ContainsKey(hashCode))
                    {
                        go = cachedObject[hashCode] as GameObject;
                    }
                    //var targetRefId = JSObjectGetProperty(gtx, o, JSStringCreateWithUTF8CString("TargetRefId"), lastException);
                    //var targetGcHandle = System.Runtime.InteropServices.GCHandle.FromIntPtr(targetRefId);
                    //var go = targetGcHandle.Target as GameObject;
                    print("go Name:" + go.name);
                }
            }
            return INTPTR.Zero;
        });

        //var fun1 = JSObjectMakeFunctionWithCallback(gtx, JSStringCreateWithUTF8CString("Print1"), delegate (INTPTR ctx, INTPTR function, INTPTR thisObject, INT argumentCount, INTPTR arguments, INTPTR exception)
        //{
        //    var res = JSValueMakeString(gtx, JSStringCreateWithUTF8CString("hello"));
        //    print("Function fun1 called...");
        //    return res;
        //});

        PrintLastException(gtx);


        JSDLL.JSObjectSetProperty(gtx, gobj, JSDLL.JSStringCreateWithUTF8CString("Print"), fun, JSDLL.JSPropertyAttributes.kJSPropertyAttributeNone, lastException);
        JSDLL.JSObjectSetProperty(gtx, gobj, JSDLL.JSStringCreateWithUTF8CString("PrintGo"), funPrintGoName, JSDLL.JSPropertyAttributes.kJSPropertyAttributeNone, lastException);

        PrintLastException(gtx);

    }

    [ContextMenu("Test")]
    public void Test()
    {
        M.WriteIntPtr(lastException, INTPTR.Zero);
        JSDLL.JSEvaluateScript(gtx, JSDLL.JSStringCreateWithUTF8CString("a = new UnityEngine.GameObject('a123');"), INTPTR.Zero, INTPTR.Zero, 0, lastException);
        PrintLastException(gtx);
    }
    [ContextMenu("Test1")]
    public void Test1()
    {
        M.WriteIntPtr(lastException, INTPTR.Zero);
        JSDLL.JSEvaluateScript(gtx, JSDLL.JSStringCreateWithUTF8CString("a.finalize();delete a;"), INTPTR.Zero, INTPTR.Zero, 0, lastException);
        JSDLL.JSGarbageCollect(gtx);
        PrintLastException(gtx);
    }
    [ContextMenu("Test2")]
    public void Test2()
    {
        M.WriteIntPtr(lastException, INTPTR.Zero);
        JSDLL.JSEvaluateScript(gtx, JSDLL.JSStringCreateWithUTF8CString(" PrintGo(a);"), INTPTR.Zero, INTPTR.Zero, 0, lastException);
        PrintLastException(gtx);
    }

    [ContextMenu("Test3")]
    public void Test3()
    {
        M.WriteIntPtr(lastException, INTPTR.Zero);
        JSDLL.JSEvaluateScript(gtx, JSDLL.JSStringCreateWithUTF8CString(" PrintGo(b);"), INTPTR.Zero, INTPTR.Zero, 0, lastException);
        PrintLastException(gtx);
    }


    [ContextMenu("Release")]
    public void Release()
    {
        JSDLL.JSGarbageCollect(gtx);
        JSDLL.JSGlobalContextRelease(gtx);
    }

}
