using SortExtravaganza.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using Common;

namespace ShellSort
{
//The main idea behind Shell Sort is to exchange items which
//are far apart.  To that end, we do a "gapped" insertion sort,
//in which the gap is the number of "subarrays" we will sort
//independently of each other.
class ShellSort
{
	static int Sort(int[] array)
	{
		int length = array.Length;

		for (int h = length / 2; h > 0; h /= 2)
		{
			var hilite = new List<int>();
			for (int i = h; i < length; i += 1)
			{
				int temp = array[i];

				int j;
				for (j = i; j >= h && array[j - h] > temp; j -= h)
				{
						array[j] = array[j - h];
						hilite.Add(j);
				}

				array[j] = temp;
				hilite.Add(j);
				graph = CommonFunctions.GraphArray(array, hilite, Color.Yellow);
				gifWriter.WriteFrame(graph);
			}
		}

		gifWriter.WriteFrame(graph, 1000);		// repeat final image, hold for a second.
		return 0;
	}

	private static IGifWriter gifWriter;
	private static Image graph;
	public static void Main()
	{
			//int[] array = { 53, 19, 71, 3, 66, 62, 20, 84 };
			int[] array = CommonFunctions.CreateTestArray(33, TestType.Shuffled);

			Console.WriteLine("Shell Sort");

		CommonFunctions.PrintInitial(array);
		using(gifWriter = new GifWriter(@"C:\Temp\ShellSort.ani.gif",150, 0))
				Sort(array);

		CommonFunctions.PrintFinal(array);
		Console.ReadKey();
	}
}
}
