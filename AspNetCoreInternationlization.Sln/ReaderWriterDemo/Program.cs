using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Loader;

namespace ReaderWriterDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // read
            // var path = AppContext.BaseDirectory +
            //            "\\de-DE\\ReaderWriterDemo.resources.dll";
            // var assembly = AssemblyLoadContext.Default
            //     .LoadFromAssemblyPath(path);
            //
            // var names = assembly.GetManifestResourceNames();
            //
            // foreach (var name in names)
            // {
            //     Console.WriteLine(name);
            //
            //     using var stream = assembly.GetManifestResourceStream(name);
            //     using var reader = new ResourceReader(stream);
            //
            //     var enumerator = reader.GetEnumerator();
            //     while (enumerator.MoveNext()) Console.WriteLine($@"{enumerator.Key}:{enumerator.Value}");
            //
            //     Console.WriteLine(@"----------------------------");
            // }

            // write
            var path = AppContext.BaseDirectory + "\\RobotCommands";

            using (FileStream stream = File.OpenWrite(path))
            {
                using var resourceWriter = new ResourceWriter(stream);
                resourceWriter.AddResource("TL", "Turn left");
                resourceWriter.AddResource("TR", "Turn right");
                resourceWriter.Generate();
            }

            using (FileStream stream = File.OpenRead(path))
            {
                using var reader = new ResourceReader(stream);

                var enumerator = reader.GetEnumerator();
                while (enumerator.MoveNext()) Console.WriteLine($@"{enumerator.Key}:{enumerator.Value}");

                Console.WriteLine(@"----------------------------");
            }
        }
    }
}
