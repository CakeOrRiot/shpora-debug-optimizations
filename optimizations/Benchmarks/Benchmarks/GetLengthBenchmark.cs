using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.Benchmarks
{
    [DisassemblyDiagnoser]
    public class GetLengthBenchmark
    {
        private byte[,] array;
        private List<byte> list;
        private int size = 10000;

        [GlobalSetup]
        public void SetUp()
        {
            array = new byte[size, size];
            list = new List<byte>();
            for (int i = 0; i < 100 * size; i++)
            {
                list.Add(0);
            }
        }

        [Benchmark]
        public void CashedArray()
        {
            var height = array.GetLength(0);
            var width = array.GetLength(1);

            for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                array[i, j] = 1;
        }

        [Benchmark]
        public void NotCashedArray()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
                array[i, j] = 1;
        }

        [Benchmark]
        public void CashedList()
        {
            var count = list.Count;
            for (int i = 0; i < count; i++)
                list[i] = 1;
        }

        [Benchmark]
        public void NotCashedList()
        {
            for (int i = 0; i < list.Count; i++)
                list[i] = 1;
        }
    }
}