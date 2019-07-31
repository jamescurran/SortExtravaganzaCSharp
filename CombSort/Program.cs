using SortExtravaganza.Common;
using System;
using System.Drawing;
using Common;

namespace CombSort
{
    //Comb Sort sorts a list by comparing values across a "gap" and switching them if they are 
    //out of order.  On each iteration through the list, the gap shrinks by a certain amount,
    //until it is comparing elements that are next to each other.
    //The gap is length of the list N divided by shrink factor K.
    //K has been empirically proven to be most efficient at 1.3.
class CombSort
{
    static int GetNextGap(int gap)
    {
        //The "shrink factor", empirically shown to be 1.3
        gap = (gap * 10) / 13;
        if (gap < 1)
        {
            return 1;
        }
        return gap;
    }

    static void Sort(int[] array)
    {
	    int length = array.Length;
	    int[] hilite = new Int32[2];

	    int gap = length;

	    //We initialize this as true to enter the while loop.
	    bool swapped = true;

	    while (gap != 1 || swapped == true)
	    {
		    gap = GetNextGap(gap);

		    //Set swapped as false.  Will go to true when two values are swapped.
		    swapped = false;

		    //Compare all elements with current gap 
		    for (int i = 0; i < length - gap; i++)
		    {

			    hilite[0] = i;
			    hilite[1] = i + gap;
			    graph = CommonFunctions.GraphArray(array, hilite, Color.Chocolate);
			    gifWriter.WriteFrame(graph);

			    if (array[i] > array[i + gap])
			    {
				    //Swap
				    int temp = array[i];
				    array[i] = array[i + gap];
				    array[i + gap] = temp;

				    swapped = true;
				    graph = CommonFunctions.GraphArray(array, hilite, Color.Yellow);
				    gifWriter.WriteFrame(graph);
			    }
		    }
	    }
    }

    private static IGifWriter gifWriter;
    private static Image graph;

		public static void Main()
    {
        //int[] array = { 10, 28, 1, 55, 6, 21, 36, 3, 45, 15, 0 };
        int[] array = CommonFunctions.CreateTestArray(33, TestType.Shuffled);

			Console.WriteLine("Comb Sort");

        CommonFunctions.PrintInitial(array);

        using (gifWriter = new GifWriter(@"C:\Temp\CombSort.33.ani.gif", 150, 0))
	        Sort(array);

			CommonFunctions.PrintFinal(array);
        Console.ReadLine();

    }
}
}
