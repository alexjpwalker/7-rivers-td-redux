using SevenRiversTD.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SevenRiversTD.Model
{
	public class Map
	{
		// Path format: 
		// Path count -
		// Path 1 Length(+6) - Start X - Start Y - Start D - Turn 1 - Turn 2 - Turn ... - End X - End Y - -1
		// Path 2 ...
		public static sbyte[] FromFile(String filename, bool CurrentDirectory)
		{
			FileStream fs;
			StreamReader sr = null;
			try
			{
				if (CurrentDirectory)
					filename = String.Concat(Environment.CurrentDirectory, "\\", filename);
				fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
				sr = new StreamReader(fs, Encoding.ASCII);
				String file = sr.ReadToEnd();
				sr.Close();
				String[] data = new String[Config.SSMAX];
				data = file.Split(',', Config.SSMAX);
				sbyte[] n = new sbyte[data.Length];
				for (int i = 0; i<n.Length; i++)
					n[i] = Convert.ToSByte(data[i]);
				return n;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Load failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				try
				{
					sr.Close();
				}
				catch (Exception)
				{
					return new sbyte[1];
				}
				return new sbyte[1];
			}
		}

		public static sbyte[,] ToGrid(sbyte[] m)
		{
			int i = 0;
			try
			{
				sbyte[,] grid = new sbyte[MW, MH];
				for (i = 0; i<MW; i++)
				{
					for (int j = 0; j<MH; j++)
						grid[i, j] = m[j * MW + i];
				}
				return grid;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Concat(ex.ToString(), "\n\ni = ", i.ToString()), "7 Rivers TD", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return new sbyte[MW, MH];
			}
		}

		public static sbyte[,] ToPaths(sbyte[] m) // Explanations are in the more confusing version
		{
			int i = 0, pn, pc, ml;
			try
			{
				pc = m[MW * MH];
				if (pc == 0)
					throw new Exception("Path count cannot be zero!");
				i = MW* MH + 1;
				ml = 0;
				for (pn = 0; pn<pc; pn++)
				{
					if (m[i] > ml)
						ml = m[i];
					if (m[i] < Config.PA_MIN)
						throw new Exception("Path length cannot be zero!");
				i += m[i];
				}
				sbyte[,] paths = new sbyte[pc, ml];
				int k = MW * MH;
				for (i = 0; i<pc; i++)
				{
					for (int j = 0; j<ml; j++)
					{
						k++;
						if (m[k] == -1)
							break;
						paths[i, j] = m[k];
					}
				}
				return paths;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Concat(ex.ToString(), "\n\ni = ", i.ToString()), "7 Rivers TD", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return new sbyte[1, 1];
			}
		}

		public static sbyte[,] ToPaths(sbyte[] m, int cmax)
		{
			int i = 0, pn, pc, ml;
			try
			{
				pc = m[MW * MH]; // pc = path count
				if (pc == 0)
					throw new Exception("Path count cannot be zero!");
				pc = ((pc<cmax) ? pc : cmax); // Considers only the first [cmax] paths
				i = MW* MH + 1;
				ml = 0;
				for (pn = 0; pn<pc; pn++)
				{
					if (m[i] > ml)
						ml = m[i]; // m[i] = path length
					if (m[i] < Config.PA_MIN)
						throw new Exception("Path length cannot be zero!");
				i += m[i]; // This only considers the "Path X Length" values
				}
				sbyte[,] paths = new sbyte[pc, ml];
				int k = MW * MH;
				for (i = 0; i<pc; i++) // Considers each path in turn.
				{
					for (int j = 0; j<ml; j++)
					{
						k++;
						if (m[k] == -1)
							break; // Start a new path or end the final path, exclude -1 from the path
						paths[i, j] = m[k];
					}
				}
				return paths;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Concat(ex.ToString(), "\n\ni = ", i.ToString()), "7 Rivers TD", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return new sbyte[1, 1];
			}
		}

		public static char[,] ToPaths(char[] m, int cmin, int c_count)
		{
			int i = 0, pn, pc, ml;
			try
			{
				pc = m[MW * MH]; // pc = path count
				if (pc == 0 || c_count == 0)
					throw new Exception("Path count cannot be zero!");
				i = MW* MH + 1;
				ml = 0;
				for (pn = 0; pn<pc; pn++)
				{
					if (m[i] > ml)
						ml = m[i]; // m[i] = path length
					if (m[i] < Config.PA_MIN)
						throw new Exception("Path length cannot be zero!");
				i += m[i]; // This only considers the "Path X Length" values
				}
				char[,] paths = new char[pc, ml];
				int k = MW * MH;
				for (i = cmin; i<cmin + c_count; i++) // Considers each path in turn.
				{
					for (int j = 0; j<ml; j++)
					{
						k++;
						if (m[k] == -1)
							break; // Start a new path or end the final path, exclude -1 from the path
						paths[i - cmin, j] = m[k];
					}
				}
				return paths;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Concat(ex.ToString(), "\n\ni = ", i.ToString()), "7 Rivers TD", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return new char[1, 1];
			}
		}

		public const int MW = 23; // Map width
		public const int MH = 20; // Map height
	};
};
