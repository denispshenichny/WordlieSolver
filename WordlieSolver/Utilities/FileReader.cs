using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WordlieSolver.Utilities
{
    public static class FileReader
    {
        public static IEnumerable<string> ReadAllLines(string resourceName)
        {
            using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"WordlieSolver.{resourceName}");
            if (stream == null)
                return Array.Empty<string>();

            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(new [] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
