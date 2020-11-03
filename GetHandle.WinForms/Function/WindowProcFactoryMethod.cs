using GetHandle.WinForms.Properties;
using System;
using System.Reflection;
using WindowHandle.Function;

namespace GetHandle.WinForms.Function
{
    /// <summary>
    /// IWindowProcFactory を実装するクラスを生成するクラス
    /// </summary>
    public static class WindowProcFactoryMethod
    {
        private static IWindowProcFactory _instance = null;

        /// <summary>
        /// IWindowProcFactory を実装するクラスのインスタンスを取得する
        /// </summary>
        /// <returns>IWindowProcFactory を実装するクラスのインスタンス</returns>
        public static IWindowProcFactory Instance
        {
            get
            {
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
