using IronRuby;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;

namespace Smith_Chart.Help
{
    public static class Engine
    {
        private static readonly ScriptEngine engine;
        private static readonly ExceptionOperations operation;

        static Engine()
        {
            engine = Ruby.CreateEngine();
            engine.Runtime.LoadAssembly(typeof(Engine).Assembly);
            engine.Runtime.LoadAssembly(typeof(Model.Point).Assembly);
            operation = engine.GetService<ExceptionOperations>();
            //ExecuteFile("Script/Initialize.rb");
        }

        public static dynamic Execute(string code)
        {
            return engine.Execute(code);
        }

        public static dynamic ExecuteFile(string path)
        {
            var file = File.OpenRead(path);
            var reader = new StreamReader(file);
            var str = reader.ReadToEnd();
            file.Close();
            return Execute(str);
        }

        public static string FormatException(Exception ex)
        {
            var str = operation.FormatException(ex);
            return str;
        }
    }
}