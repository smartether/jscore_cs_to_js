//#define _64_BIT_
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if _64_BIT_
using INT = System.Int64;
#else
using INT = System.Int32;
#endif
using INTPTR = System.IntPtr;
using M = System.Runtime.InteropServices.Marshal;
using BindingFlags = System.Reflection.BindingFlags;


using DLLIMPORT = System.Runtime.InteropServices.DllImportAttribute;


namespace JSInterface
{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
using UFP = System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute;
#else
    using UFP = JSInterface.UnmanagedFunctionPointerAttribute;
#endif

    public class UnmanagedFunctionPointerAttribute : System.Attribute
    {
        public UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention callingCvt)
        {

        }
    }
    

    public static class JSDLL
    {


#if !UNITY_EDITOR && UNITY_IPHONE
        const string JSCDLL = "__Internal";
#elif !UNITY_EDITOR && UNITY_ANDROID
        const string JSCDLL = "jsc";//"android-jsc";
#else
        const string JSCDLL = "JavaScriptCore";
#endif

        /** JSBase */
        //JSValueRef JSEvaluateScript(JSContextRef ctx, JSStringRef script, JSObjectRef thisObject, JSStringRef sourceURL, int startingLineNumber, JSValueRef* exception);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSEvaluateScript(INTPTR ctx, INTPTR script, INTPTR thisObject, INTPTR sourceURL, int startingLineNumber, INTPTR exception);


        /** JSObjectRef */
        //JSClassRef JSClassCreate(const JSClassDefinition* definition);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSClassCreate(ref JSClassDefinition definition);

        // JSClassRef JSClassRetain(JSClassRef jsClass);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSClassRetain(INTPTR jsClass);

        //void JSClassRelease(JSClassRef jsClass);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void JSClassRelease(INTPTR jsClass);

        // JSObjectRef JSObjectMake(JSContextRef ctx, JSClassRef jsClass, void* data);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSObjectMake(INTPTR ctx, INTPTR jsClass, INTPTR data);

        //JSObjectRef JSObjectMakeFunctionWithCallback(JSContextRef ctx, JSStringRef name, JSObjectCallAsFunctionCallback callAsFunction);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSObjectMakeFunctionWithCallback(INTPTR ctx, INTPTR name, JSObjectCallAsFunctionCallback callAsFunction);

        //void JSObjectSetPrototype(JSContextRef ctx, JSObjectRef object, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void JSObjectSetPrototype(INTPTR ctx, INTPTR obj, INTPTR value);

        //bool JSObjectHasProperty(JSContextRef ctx, JSObjectRef object, JSStringRef propertyName);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSObjectHasProperty(INTPTR ctx, INTPTR obj, INTPTR propertyName);

        //JSValueRef JSObjectGetProperty(JSContextRef ctx, JSObjectRef object, JSStringRef propertyName, JSValueRef* exception);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSObjectGetProperty(INTPTR ctx, INTPTR obj, INTPTR propertyName, INTPTR exception);

        //void JSObjectSetProperty(JSContextRef ctx, JSObjectRef object, JSStringRef propertyName, JSValueRef value, JSPropertyAttributes attributes, JSValueRef* exception);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void JSObjectSetProperty(INTPTR ctx, INTPTR obj, INTPTR propertyName, INTPTR value, JSPropertyAttributes attributes, INTPTR exception);

        //JSObjectRef JSObjectMakeError(JSContextRef ctx, size_t argumentCount, const JSValueRef arguments[], JSValueRef* exception)
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSObjectMakeError(INTPTR ctx, INT argumentCount, INTPTR[] arguments, INTPTR exception);

        /** JSStringRef */
        //JSStringRef JSStringCreateWithUTF8CString(const char* string);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSStringCreateWithUTF8CString(string str);

        //const JSChar* JSStringGetCharactersPtr(JSStringRef string);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSStringGetCharactersPtr(INTPTR str);

        //size_t JSStringGetLength(JSStringRef string)
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int JSStringGetLength(INTPTR str);



        /** JSValueRef  */
        //JSValueRef JSValueMakeNumber(JSContextRef ctx, double number);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSValueMakeNumber(INTPTR ctx, double number);

        //double JSValueToNumber(JSContextRef ctx, JSValueRef value, JSValueRef* exception);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern double JSValueToNumber(INTPTR ctx, INTPTR value, INTPTR exception);

        //bool JSValueIsObjectOfClass(JSContextRef ctx, JSValueRef value, JSClassRef jsClass);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSValueIsObjectOfClass(INTPTR ctx, INTPTR value, INTPTR jsClass);

        //JSValueRef JSValueMakeString(JSContextRef ctx, JSStringRef string);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSValueMakeString(INTPTR ctx, INTPTR str);

        //JSObjectRef JSValueToObject(JSContextRef ctx, JSValueRef value, JSValueRef* exception);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSValueToObject(INTPTR ctx, INTPTR value, INTPTR exception);

        // JSStringRef JSValueToStringCopy(JSContextRef ctx, JSValueRef value, JSValueRef* exception);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSValueToStringCopy(INTPTR ctx, INTPTR value, INTPTR exception);

        //bool JSValueIsNumber(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSValueIsNumber(INTPTR ctx, INTPTR value);

        //bool JSValueIsString(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSValueIsString(INTPTR ctx, INTPTR value);

        // bool JSValueIsBoolean(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSValueIsBoolean(INTPTR ctx, INTPTR value);

        //bool JSValueIsNull(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSValueIsNull(INTPTR ctx, INTPTR value);

        //bool JSValueIsUndefined(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSValueIsUndefined(INTPTR ctx, INTPTR value);

        //bool JSValueIsObject(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSValueIsObject(INTPTR ctx, INTPTR value);

        //bool JSValueIsArray(JSContextRef ctx, JSValueRef value)
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern bool JSValueIsArray(INTPTR ctx, INTPTR value);

        //bool JSValueIsDate(JSContextRef ctx, JSValueRef value)
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool JSValueIsDate(INTPTR ctx, INTPTR value);

        //JSType JSValueGetType(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern JSType JSValueGetType(INTPTR ctx, INTPTR value);

        //void JSValueProtect(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void JSValueProtect(INTPTR ctx, INTPTR value);

        //void JSValueUnprotect(JSContextRef ctx, JSValueRef value);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void JSValueUnprotect(INTPTR ctx, INTPTR value);

        /** JSContextRef */

        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSGlobalContextCreate(INTPTR globalObjectClass);

        //JSGlobalContextRef JSContextGetGlobalContext(JSContextRef ctx)
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSContextGetGlobalContext(INTPTR ctx);

        //JSObjectRef JSContextGetGlobalObject(JSContextRef ctx);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern INTPTR JSContextGetGlobalObject(INTPTR ctx);

        //void JSGlobalContextRelease(JSGlobalContextRef ctx);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void JSGlobalContextRelease(INTPTR ctx);

        //void JSGarbageCollect(JSContextRef ctx);
        [DLLIMPORT(JSCDLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void JSGarbageCollect(INTPTR ctx);

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct JSClassDefinition
        {
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I4)]
            public int version;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I4)]
            public int attributes;
            //public INTPTR className;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string className;
            //JSClassRef parentClass;
            public INTPTR parentClass;
            //const JSStaticValue* staticValues;
            public INTPTR staticValues;
            //const JSStaticFunction* staticFunctions;
            public INTPTR staticFunctions;
            //JSObjectInitializeCallback initialize;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectInitializeCallback initialize;
            //JSObjectFinalizeCallback finalize;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectFinalizeCallback finalize;
            //JSObjectHasPropertyCallback hasProperty;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectHasPropertyCallback hasProperty;
            //JSObjectGetPropertyCallback getProperty;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectGetPropertyCallback getProprety;
            //JSObjectSetPropertyCallback setProperty;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectSetPropertyCallback setProperty;
            //JSObjectDeletePropertyCallback deleteProperty;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectDeletePropertyCallback deleteProperty;
            //JSObjectGetPropertyNamesCallback getPropertyNames;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectGetPropertyNamesCallback getPropertyNames;
            //JSObjectCallAsFunctionCallback callAsFunction;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectCallAsFunctionCallback callAsFunction;
            //JSObjectCallAsConstructorCallback callAsConstructor;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectCallAsConstructorCallback callAsConstructor;
            //JSObjectHasInstanceCallback hasInstance;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectHasInstanceCallback hasInstance;
            //JSObjectConvertToTypeCallback convertToType;
            //[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
            public JSObjectConvertToTypeCallback convertToType;

            public JSClassAttributes Attributes
            {
                get
                {
                    return (JSClassAttributes)attributes;
                }
                set
                {
                    attributes = (int)value;
                }
            }

            public string ClassName
            {
                get
                {
                    return className;
                    //int len = JSStringGetLength(className);
                    //UnityEngine.Debug.Log(" len:" + len);
                    //var str = M.PtrToStringUni(JSStringGetCharactersPtr(className), len);
                    //UnityEngine.Debug.Log("get className:" + str);
                    //return str;

                }
                set
                {
                    UnityEngine.Debug.Log("set className:" + value);
                    className = value; //JSStringCreateWithUTF8CString(value);
                }
            }
        }

        //void (*JSObjectInitializeCallback) (JSContextRef ctx, JSObjectRef object);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void JSObjectInitializeCallback(INTPTR ctx, INTPTR obj);

        //void (*JSObjectFinalizeCallback) (JSObjectRef object);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void JSObjectFinalizeCallback(INTPTR obj);

        //bool (*JSObjectHasPropertyCallback) (JSContextRef ctx, JSObjectRef object, JSStringRef propertyName);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate bool JSObjectHasPropertyCallback(INTPTR ctx, INTPTR obj, INTPTR propertyName);

        //JSValueRef (*JSObjectGetPropertyCallback) (JSContextRef ctx, JSObjectRef object, JSStringRef propertyName, JSValueRef* exception);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate INTPTR JSObjectGetPropertyCallback(INTPTR ctx, INTPTR obj, INTPTR propertyName, INTPTR exception);

        //bool (*JSObjectSetPropertyCallback) (JSContextRef ctx, JSObjectRef object, JSStringRef propertyName, JSValueRef value, JSValueRef* exception);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate bool JSObjectSetPropertyCallback(INTPTR ctx, INTPTR obj, INTPTR propertyName, INTPTR value, INTPTR exception);

        //bool (*JSObjectDeletePropertyCallback) (JSContextRef ctx, JSObjectRef object, JSStringRef propertyName, JSValueRef* exception);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate bool JSObjectDeletePropertyCallback(INTPTR ctx, INTPTR obj, INTPTR propertyName, INTPTR exception);

        //void (*JSObjectGetPropertyNamesCallback) (JSContextRef ctx, JSObjectRef object, JSPropertyNameAccumulatorRef propertyNames);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void JSObjectGetPropertyNamesCallback(INTPTR ctx, INTPTR obj, INTPTR propertyNames);

        //JSValueRef (*JSObjectCallAsFunctionCallback) (JSContextRef ctx, JSObjectRef function, JSObjectRef thisObject, size_t argumentCount, const JSValueRef arguments[], JSValueRef* exception);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate INTPTR JSObjectCallAsFunctionCallback(INTPTR ctx, INTPTR function, INTPTR thisObject, INT argumentCount, INTPTR arguments, INTPTR exception);

        //JSObjectRef (*JSObjectCallAsConstructorCallback) (JSContextRef ctx, JSObjectRef constructor, size_t argumentCount, const JSValueRef arguments[], JSValueRef* exception);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate INTPTR JSObjectCallAsConstructorCallback(INTPTR ctx, INTPTR constructor, INT argumentCount, INTPTR arguments, INTPTR exception);

        //bool (*JSObjectHasInstanceCallback)  (JSContextRef ctx, JSObjectRef constructor, JSValueRef possibleInstance, JSValueRef* exception);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate bool JSObjectHasInstanceCallback(INTPTR ctx, INTPTR constructor, INTPTR possibleInstance, INTPTR exception);

        //JSValueRef (*JSObjectConvertToTypeCallback) (JSContextRef ctx, JSObjectRef object, JSType type, JSValueRef* exception);
        [UFP(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate INTPTR JSObjectConvertToTypeCallback(INTPTR ctx, INTPTR obj, JSType type, INTPTR exception);


        public enum JSType
        {
            kJSTypeUndefined = 0,
            kJSTypeNull = 1,
            kJSTypeBoolean = 2,
            kJSTypeNumber = 3,
            kJSTypeString = 4,
            kJSTypeObject = 5
        }

      
        public enum JSClassAttributes
        {
            kJSClassAttributeNone = 0,
            kJSClassAttributeNoAutomaticPrototype = 1 << 1
        }


        public enum JSPropertyAttributes
        {
            kJSPropertyAttributeNone = 0,
            kJSPropertyAttributeReadOnly = 1 << 1,
            kJSPropertyAttributeDontEnum = 1 << 2,
            kJSPropertyAttributeDontDelete = 1 << 3
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct JSStaticValue
        {
            INTPTR name;
            JSObjectGetPropertyCallback getProperty;
            JSObjectSetPropertyCallback setProperty;
            JSPropertyAttributes attributes;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct JSStaticFunction
        {
            INTPTR name;
            JSObjectCallAsFunctionCallback callAsFunction;
            JSPropertyAttributes attributes;
        }


    }

}