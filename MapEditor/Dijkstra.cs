using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WindowsFormsApplication3
{
    class Dijkstra
    {
        private readonly List<V> _v;
        private static bool _fullGraph;

        public static void main(string[] args)
        {
            var h = new List<string>();
            var s = StreamReader.Null;
            var count = s.Read();
            for (var i = 0; i < count; i++)
            {
                h.Add(s.ReadLine());
            }
            s.Close();
            var d = new Dijkstra(BuildGraph(h));
            var m = d.SerchPath(1, 21);

            foreach (var v in m)
            {
                Console.Write(v + @" ");
            }
        }

        public Dijkstra(List<V> v)
        {
            _v = v;
            var countEdge = v.Aggregate<V, double>(0, (current, x) => current + x.e.Count);
            _fullGraph = countEdge/((double) v.Count*v.Count) >= 0.7;
        }

        public List<int> SerchPath(int vStart, int vEnd)
        {
            if (_fullGraph)
                return DijkstraFull(vStart, vEnd);
            return DijkstraNotFull(vStart, vEnd);
        }

        private List<int> DijkstraFull(int vStart, int vEnd)
        {
            var parent = new int[_v.Count];
            const int inf = 1 << 30; 
            var dist = new int[_v.Count]; 
            var used = new bool[_v.Count]; 
            for (var i = 0; i < _v.Count; i++)
            {
                used[i] = false;
                dist[i] = inf;
            }
            dist[vStart - 1] = 0; 

            for (var i = 0; i < _v.Count; i++)
            {
                var x = -1; 
                for (var j = 0; j < _v.Count; j++) 
                    if (!used[j] && (x == -1 || dist[j] < dist[x])) 
                        x = j; 
                used[x] = true; 
                for (var j = 0; j < _v[x].e.Count; j++)
                {
                    var u = _v[x].e[j].V - 1; 
                    var w = _v[x].e[j].W; 
                    if (dist[u] <= dist[x] + w) continue;
                    dist[u] = dist[x] + w; 
                    parent[u] = x; 
                }
            }
            var step = vEnd - 1;
            var answ = new List<int>();
            while (step != 0)
            {
                answ.Add(step + 1);
                step = parent[step];
            }
            answ.Add(vStart);
            return answ;
        }

        private List<int> DijkstraNotFull(int vStart, int vEnd)
        {
            var parent = new int[_v.Count];
            var tree = new SortedDictionary<int, V>();
            var dist = new int[_v.Count];
            const int inf = 1 << 30;
            for (var i = 0; i < _v.Count; i++)
                dist[i] = inf; 
            dist[vStart - 1] = 0; 
            tree.Add(0, _v[vStart - 1]);
            while (tree.Count != 0)
            {
                var v1 = tree.Keys.First();
                var x = tree[v1].v - 1; 
                tree.Remove(v1); 
                foreach (var t in _v[x].e)
                {
                    var u = t.V - 1;
                    var w = t.W;
                    if (dist[u] <= dist[x] + w) continue;
                    tree.Remove(u);
                    dist[u] = dist[x] + w;
                    if(!tree.ContainsKey(dist[u]))
                    tree.Add(dist[u], _v[u]); 
                    parent[u] = x;
                }
            }
            var step = vEnd - 1;
            var answ = new List<int>();
            while (step != 0)
            {
                answ.Add(step + 1);
                step = parent[step];
            }
            answ.Add(vStart);
            return answ;
        }

        public static List<V> BuildGraph(List<string> es)
        {

            var v = new List<V>();
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
