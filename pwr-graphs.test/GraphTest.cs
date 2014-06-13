using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace pwr_graphs.test
{
    [TestFixture]
    public class GraphTest
    {
        /*
         * undirected weighted graph 
         */

        private UndirectGraph _undirectGraph;
        private int _numberOfVertex;

        [SetUp]
        public void SetUp()
        {
            _numberOfVertex = 3;
            _undirectGraph = new UndirectGraph(_numberOfVertex);
        }

        [Test]
        public void GivenNumberOfVertexThenReturnMinimumNumberOfEdges()
        {
            int numberOfVertex = 5;
            int expectedNumberOfEdges = 4;

            int result = _undirectGraph.GetMinimumOfEdges(numberOfVertex);

            Assert.AreEqual(expectedNumberOfEdges, result);
        }

        [Test]
        public void GivenNumberOfVertexThenReturnMaximumNumberOfEdges()
        {
            int numberOfVertex = 4;
            int expectedNumberOfEdges = 6;

            int result = _undirectGraph.GetMaximumOfEdges(numberOfVertex);

            Assert.AreEqual(expectedNumberOfEdges, result);
        }

        [Test]
        public void GivenNumberOfVertexAndEdgesThenGenerateUndirectedGraph()
        {
            int numberOfVertex = 3;
            int numberOfEgdes = 3;
            int[,] expectedEgdes = { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };

            _undirectGraph.GenerateGraph(numberOfVertex, numberOfEgdes);
            var result = _undirectGraph.Connections;

            Assert.AreEqual(expectedEgdes, result);

        }

        [Test]        
        public void GivenNumberOfVertexAndEdgesThenValidateAndReturnTrue()
        {
            int numberOfVertex = 3;
            int numberOfEdges = 3;
            bool expectedValue = true;

            var result = _undirectGraph.ValidateVertexAndEdges(numberOfVertex, numberOfEdges);

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void GivenNumberOfVertexAndEdgesThenValidateAndReturnFalse()
        {
            int numberOfVertex = 4;
            int numberOfEdges = 10;
            bool expectedValue = false;

            var result = _undirectGraph.ValidateVertexAndEdges(numberOfVertex, numberOfEdges);

            Assert.AreEqual(expectedValue, result);
        }

    }

    public interface IGraph
    {
        int[,] Connections { get; set; }
        int GetMinimumOfEdges(int numberOfVertex);
        int GetMaximumOfEdges(int numberOfVertex);
        bool ValidateVertexAndEdges(int numberOfVertex, int numberOfEdges);
        void GenerateGraph(int numberOfVertex, int numberOfEdges);
        int NumberOfVertex { get; }
        int NumberOfEdges { get; }
    }

    public interface IWeightedGraph : IGraph
    {
        int[,] Weights { get; set; }
    }

    public class UndirectGraph : IWeightedGraph
    {
        public int[,] Connections { get; set; }
        public int[,] Weights { get; set; }

        public UndirectGraph(int numberOfVertex, int numberOfEdges)
        {
            _numberOfVertex = numberOfVertex;
            _numberOfEdges = numberOfEdges;
        }

        public UndirectGraph(int numberOfVertex)
        {
            _numberOfVertex = numberOfVertex;
        }

        public int GetMinimumOfEdges(int numberOfVertex)
        {
            int result = numberOfVertex - 1;
            return result;
        }

        public int GetMaximumOfEdges(int numberOfVertex)
        {
            int result = (numberOfVertex * (numberOfVertex - 1)) / 2;
            return result;
        }

        public bool ValidateVertexAndEdges(int numberOfVertex, int numberOfEdges)
        {
            int minimum = GetMinimumOfEdges(numberOfVertex);
            int maximum = GetMaximumOfEdges(numberOfVertex);

            if (numberOfEdges >= minimum && numberOfEdges <= maximum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GenerateGraph(int numberOfVertex, int numberOfEdges)
        {
            Connections = new int[numberOfVertex, numberOfVertex];
            Weights = new int[numberOfVertex, numberOfVertex];

            if (ValidateVertexAndEdges(numberOfVertex, numberOfEdges))
            {
                for (int i = 0; i < numberOfEdges; i++)
                {
                    int yRandom = new Random().Next(1, numberOfVertex);
                    int xRandom = new Random().Next(0, yRandom);

                    while (Connections[yRandom, xRandom] == 1)
                    {
                        yRandom = new Random().Next(1, numberOfVertex);
                        xRandom = new Random().Next(0, yRandom);
                    }

                    Connections[yRandom, xRandom] = 1;
                    Weights[yRandom, xRandom] = new Random().Next(0, 10);
                }

                for (int i = 0; i < Connections.GetLength(0); i++)
                {
                    for (int j = 0; j < Connections.GetLength(1); j++)
                    {
                        if (Connections[i, j] == 1)
                        {
                            Connections[j, i] = 1;
                            Weights[j, i] = Weights[i, j];
                        }
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        //public int[,] GenerateRandomUndirectGraph(int numberOfVertex)
        //{
        //    int[,] result = new int[numberOfVertex, numberOfVertex];
        //    var upperLimitOfEdges = GetMaximumOfEdges(numberOfVertex) + 1;
        //    var lowerLimitOfEdges = GetMinimumOfEdges(numberOfVertex);
        //    var randomnumberOfEdges = new Random().Next(lowerLimitOfEdges, upperLimitOfEdges);
        //    result = GenerateUndirectGraph(numberOfVertex, randomnumberOfEdges);
        //    return result;
        //}



        private int _numberOfVertex;
        public int NumberOfVertex
        {
            get { return _numberOfVertex; }
        }

        private int _numberOfEdges;
        public int NumberOfEdges
        {
            get { return _numberOfEdges; }
        }
    }
    
}
