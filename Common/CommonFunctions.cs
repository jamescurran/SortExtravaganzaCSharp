using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace SortExtravaganza.Common
{
	public static class CommonFunctions
	{
		public static void PrintInitial(this IEnumerable<int> array)
		{
			Console.Write("Initial array is: ");
			Print(array);
		}

		public static void PrintFinal(this IEnumerable<int> array)
		{
			Console.Write("Sorted array is: ");
			Print(array);
		}

		private static void Print(this IEnumerable<int> array)
		{
			foreach(var num in array)
			{
				Console.Write(num + " ");
			}
			Console.WriteLine();
		}

		public static int[] CreateTestArray(int size, TestType type, int seed = -1)
		{
			var rnd = seed ==-1 ? new Random() : new Random(seed);

			if (type == TestType.Random)
			{
				var random = new int[size];
				for (int i = 0; i < size; i++)
					random[i] = rnd.Next(size);
				return random;
			}

			var shuffled = Enumerable.Range(1, size).ToArray();
			for (int i = size - 1; i > 0; --i)
			{
				int pos = rnd.Next(i);
				var temp = shuffled[i];
				shuffled[i] = shuffled[pos];
				shuffled[pos] = temp;
			}

			return shuffled;

		}

		public static Bitmap GraphArray(int[] array, IList<int> highlight, Color highlightColor)
		{
			var width = 277.0f;
			var height = 344.0f;
			var backgroundColor = Color.FromArgb(43, 136, 196);
			var margin = 6.0f;

			var backgroundBrush = new SolidBrush(backgroundColor);
			var bitmap = new Bitmap((int)width, (int)height, PixelFormat.Format32bppArgb);
			var graph = Graphics.FromImage(bitmap);
			graph.FillRectangle(backgroundBrush, 0.0f, 0.0f, width - 1, height - 1);
			graph.DrawRectangle(Pens.Cyan, margin, margin, width - (margin * 2) - 1, height - (margin * 2) - 1);

			var highlightbrush = new SolidBrush(highlightColor);

			var max = array.Max();
			var unitheight = ((height - margin * 2) * .95) / max;
			var unitwidth = ((width - margin * 2) / array.Length);
			for (int i = 0; i < array.Length; ++i)
			{
				var val = array[i];
				var brush = highlight.Contains(i) ? highlightbrush : Brushes.Orange;
				graph.FillRectangle(brush, i * unitwidth + margin + 2, (float)(height - 2 - margin - (val * unitheight)), unitwidth - 2, (float)(val * unitheight));
			}


//			bitmap.Save(@"C:\temp\graph.gif", ImageFormat.Gif);


			return bitmap;

		}

	}

	public enum TestType
	{
		Shuffled,
		Random
	};
}
