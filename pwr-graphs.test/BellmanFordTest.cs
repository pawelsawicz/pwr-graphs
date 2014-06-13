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
        [Test]
        public void GivenCollectionOfEdgesThenCalculateTotalPositiveCost()
        {
            var edges = new List<Edge>();
            edges.Add(new Edge(1,2,5));
            edges.Add(new Edge(1,3,5));
            edges.Add(new Edge(1,0,5));
            edges.Add(new Edge(0,2,5));
            
            var bellmanford = new BellmanFord();           
               
        }
    }     

    public class BellmanFord
    {
        public List<Edge> Edges = new List<Edge>();
        public int V;  

        public int GetTotalPositiveCost()
        {
            int sum = 0;
            foreach (var e in Edges)
            {
                if (e.Length > 0) sum += (int)e.Length;
            }
            return sum;
        }

        private void generateV()
        {
            foreach (var e in Edges)
            {
                V = Math.Max(V, e.FirstVertex);
                V = Math.Max(V, e.SecondVertex);
            }
            V++;
        }
        
        public int[] GetShortestPath(int startIndex)
        {
            if (V == 0 && Edges.Count > 0) generateV();

            int[] shortestPath = new int[V];
            int INF = this.GetTotalPositiveCost() + 1;

            for (int i = 0; i < V; i++) shortestPath[i] = INF;

            shortestPath[startIndex] = 0;
            while (true)
            {
                bool update = false;
                foreach (var e in Edges)
                {
                    if (shortestPath[e.FirstVertex] != INF && shortestPath[e.SecondVertex] > shortestPath[e.FirstVertex] + e.Length)
                    {
                        shortestPath[e.SecondVertex] = shortestPath[e.FirstVertex] + (int)e.Length;
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
                foreach (var e in Edges)
                {
                    if (d[e.SecondVertex] > d[e.FirstVertex] + (int)e.Length)
                    {
                        d[e.SecondVertex] = d[e.FirstVertex] + (int)e.Length;
                        if (i == V - 1) return true;
                    }
                }
            }
            return false;
        }
    }
}
