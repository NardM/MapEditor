using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApplication3
{
    public class FileManeger
    {
        private static IEnumerable<string> ReadFile(string path)
        {
            var result = new List<string>();
            var s = new StreamReader(path);
            string line;
            while((line=s.ReadLine())!=null)
            {
                result.Add(line);
            }
            s.Close();
            return result;
        }

        public static List<V> BuildGraph(string designation, string graph)
        {
            var vs = ReadFile(designation);
            var v = vs.Select(line => line.Split(' ')).Select(param => new V(int.Parse(param[0]), double.Parse(param[1].Replace(".", ",")), double.Parse(param[2].Replace(".", ",")))).ToList();
            var es = ReadFile(graph);
            foreach (var line in es)
            {
                var param = line.Split(' ');
                var v1 = int.Parse(param[0]);
                var v2 = int.Parse(param[1]);
                var w = int.Parse(param[2]);
                v[v1 - 1].e.Add(new E(w, v2));
                v[v2 - 1].e.Add(new E(w, v1));
            }
            return v;
        }
    }
}
