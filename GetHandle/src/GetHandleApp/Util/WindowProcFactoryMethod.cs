using System;
using System.Reflection;
using GetHandle.Function;
using GetHandleApp.Properties;

namespace GetHandleApp.Util
{
    /// <summary>
    /// IWindowProcFactory を実装するクラスを生成するクラス
    /// </summary>
    public static class WindowProcFactoryMethod
    {
        private static IWindowProcFactory _instance = null;

        /// <summary>
        /// IWindowProcFactory を実装するクラスのインスタンスを取得する。
        /// </summary>
        public static IWindowProcFactory Instance
        {
            get
            {
                // インスタンスがまだ生成されていない場合、生成する。
                if (_instance == null)
                {
                    Type factoryType;

                    if (string.IsNullOrEmpty(Settings.Default.WindowProcFactoryAssemblyFile))
                    {
                        factoryType = Type.GetType(Settings.Default.WindowProcFactoryClass, true);
                    }
                    else
                    {
                        factoryType = Assembly.LoadFrom(Settings.Default.WindowProcFactoryAssemblyFile).GetType(Settings.Default.WindowProcFactoryClass, true);
                    }

                    _instance = (IWindowProcFactory)Activator.CreateInstance(factoryType);
                }

                return _instance;
            }
        }
    }
}
