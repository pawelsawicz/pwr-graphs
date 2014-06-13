using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace pwr_graphs.test
{
    [TestFixture]
    public class KruskalTest
    {
        private Vertex[] _vertex { get; set; }

        [SetUp]
        public void SetUp()
        {
            _vertex = new Vertex[]{
                new Vertex(1,1),
                new Vertex(2,2),
                new Vertex(1,0),
                new Vertex(0,0)
            };
        }

        [Test]
        public void GivenUndirectWeightedGraphThenReturnMTS()
        {
            var kruskal = new Kruskal(_vertex);


            Assert.AreEqual(1, kruskal.Result);
        }


    }

    public class Edge
    {
        public double Length { get; set; }
        public int FirstVertex { get; set; }
        public int SecondVertex { get; set; }

        public Edge(int firstVertex, int secondVertex, double length)
        {
            this.FirstVertex = firstVertex;
            this.SecondVertex = secondVertex;
            this.Length = length;
        }        
    }

    public class Vertex
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vertex(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class Kruskal
    {
        public Edge[] Result { get; private set; }
        public double Span { get; private set; }

        public Kruskal(Vertex[] points)
        {
            int edgesArrayLength = 0;            
            for (int i = points.Length - 1; i > 0; i--)
                edgesArrayLength += i;
            Edge[] edges = new Edge[edgesArrayLength];

            
            for (int i = 0, index = 0; i < points.Length; i++)
                for (int j = i + 1; j < points.Length; j++)
                {
                    int dx = points[i].X - points[j].X;
                    int dy = points[i].Y - points[j].Y;
                    edges[index] = new Edge(i, j, Math.Sqrt(dx * dx + dy * dy));
                    index++;
                }

            var sortEdges = edges.OrderBy(a => a.Length);
            
            int[] sets = new int[points.Length];
            Result = new Edge[points.Length - 1];
            int processedEdges = 0;
            foreach (var edge in sortEdges)
            {
               
                if (processedEdges == points.Length - 1)
                    break;

                
                if (sets[edge.FirstVertex] == 0 || sets[edge.FirstVertex] != sets[edge.SecondVertex])
                {
                    Result[processedEdges] = edge;
                    Span += edge.Length;
                    processedEdges++;

                    if (sets[edge.FirstVertex] != 0 || sets[edge.SecondVertex] != 0)
                    {

                        int set1 = sets[edge.FirstVertex];
                        int set2 = sets[edge.SecondVertex];
                       
                        for (int i = 0; i < points.Length; i++)
                            if (sets[i] != 0 && (sets[i] == set1 || sets[i] == set2))
                                sets[i] = processedEdges;
                    }

                    sets[edge.FirstVertex] = processedEdges;
                    sets[edge.SecondVertex] = processedEdges;
                }
            }
        }
    }

}
