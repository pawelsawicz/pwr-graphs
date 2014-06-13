using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace pwr_graphs.test
{
    [TestFixture]
    public class BellmanFordTest
    {

    }

    public class BellmanFord
    {
        public List<edge> Edge = new List<edge>();
        public int V;
        
        public class edge
        {
            public int from, to, cost;

            public edge(int _from, int _to, int _cost)
            {
                from = _from;
                to = _to;
                cost = _cost;
            }
        }

        private int GetTotalPositiveCost()
        {
            int sum = 0;
            foreach (var e in Edge)
            {
                if (e.cost > 0) sum += e.cost;
            }
            return sum;
        }

        private void generateV()
        {
            foreach (var e in Edge)
            {
                V = Math.Max(V, e.from);
                V = Math.Max(V, e.to);
            }
            V++;
        }
        
        public int[] GetShortestPath(int startIndex)
        {
            if (V == 0 && Edge.Count > 0) generateV();

            int[] shortestPath = new int[V];
            int INF = this.GetTotalPositiveCost() + 1;

            for (int i = 0; i < V; i++) shortestPath[i] = INF;

            shortestPath[startIndex] = 0;
            while (true)
            {
                bool update = false;
                foreach (edge e in Edge)
                {
                    if (shortestPath[e.from] != INF && shortestPath[e.to] > shortestPath[e.from] + e.cost)
                    {
                        shortestPath[e.to] = shortestPath[e.from] + e.cost;
                        update = true;
                    }
                }
                if (!update) break;
            }

            return shortestPath;
        }
       
        public bool HasNegativeLoop()
        {
            int[] d = new int[V];
            for (int i = 0; i < V; i++)
            {
                foreach (edge e in Edge)
                {
                    if (d[e.to] > d[e.from] + e.cost)
                    {
                        d[e.to] = d[e.from] + e.cost;
                        if (i == V - 1) return true;
                    }
                }
            }
            return false;
        }
    }
}
