using Jint;
using Jint.Native;

namespace Zin.Editor.Plugin.Config;

public struct APIContainer<T> where T : class
{
    public readonly string Name;
    public readonly T AsNative;
    public readonly JsValue AsJSValue;

    public APIContainer(Engine engine, string name, T native)
    {
        Name = name;
        AsNative = native;
        AsJSValue = JsValue.FromObject(engine, native);
    }
}