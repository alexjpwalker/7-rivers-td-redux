using SevenRiversTD.Data;
using SevenRiversTD.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SevenRiversTD.Model
{
	public class Wave
	{
		public Wave(int count, EnemyType type, int HP, int speed, int spawnTime, int gold, sbyte[,] paths)
		{
			EnemyList = new Enemy[count];
			Count = count;
			Type = type;
			for (int i = 0; i < count; i++)
				EnemyList[i] = new Enemy(Type, HP, speed, spawnTime, gold, paths);
		}
		public Enemy[] EnemyList { get; set; }
		public int Count { get; set; }
		public EnemyType Type { get; set; }
	}

	public class WaveSequence
	{
		public WaveSequence(int length)
		{
			try
			{
				_w = new Wave[length];
				Count = 0;
			}
			catch (Exception)
			{
				Count = 0;
			}
		}

		public WaveSequence()
		{
			Count = 0;
		}

		private Wave[] _w;
		public int Count { get; private set; }
		public void AddWave(Wave wv)
		{
			_w[Count] = wv;
			Count++;
		}

		public Wave GetWave(int i)
		{
			return _w[i];
		}

		public static WaveSequence get_FileSequence(string filename, sbyte[] map, bool CurrentDirectory)
		{
			int length = InternalFromFile(filename, map, CurrentDirectory, out Wave[] ws);
			WaveSequence temp = new WaveSequence(length);
			for (int i = 0; i < length; i++)
				temp.AddWave(ws[i]);
			return temp;
		}
		public static int get_FileSequenceCount(string filename, sbyte[] map)
		{
			int length = InternalFromFile(filename, map);
			return length;
		}

		/** Returns wave count, or -1 if load failed */
		private static int InternalFromFile(string filename, sbyte[] map, bool CurrentDirectory, out Wave[] ws)
		{
			FileStream fs;
			StreamReader sr = null;
			ws = null;
			try
			{
				if (CurrentDirectory)
					filename = string.Concat(Environment.CurrentDirectory, "\\", filename);
				fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
				sr = new StreamReader(fs, Encoding.ASCII);
				string file = sr.ReadToEnd();
				sr.Close();
				string[] data = new string[Config.SSMAX];
				data = file.Split(",".ToCharArray(), Config.SSMAX);
				int[] n = new int[data.Length];
				for (int i = 0; i < n.Length; i++)
					n[i] = Convert.ToInt32(data[i]);
				// Now that all that is over and done with, the program is left with a useful data array.
				// (n[0] is the wave count.)
				ws = new Wave[n[0]];
				for (int i = 0; i < n[0]; i++)
				{
					if (n[i * 7 + 7] == 0)
						ws[i] = new Wave(n[i * 7 + 1], (EnemyType)n[i * 7 + 2], n[i * 7 + 3], n[i * 7 + 4], n[i * 7 + 5], n[i * 7 + 6], Map.ToPaths(map));
					else
						ws[i] = new Wave(n[i * 7 + 1], (EnemyType)n[i * 7 + 2], n[i * 7 + 3], n[i * 7 + 4], n[i * 7 + 5], n[i * 7 + 6], Map.ToPaths(map, n[i * 7 + 7]));
				}
				return n[0];
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
					return -1;
				}
				return -1;
			}
		}
		private static int InternalFromFile(string filename, sbyte[] map)
		{
			FileStream fs;
			StreamReader sr = null;
			try
			{
				fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
				sr = new StreamReader(fs, Encoding.ASCII);
				string file = sr.ReadToEnd();
				sr.Close();
				string[] data = new string[Config.SSMAX];
				data = file.Split(",".ToCharArray(), Config.SSMAX);
				int[] n = new int[data.Length];
				for (int i = 0; i < n.Length; i++)
					n[i] = Convert.ToInt32(data[i]);
				// Now that all that is over and done with, the program is left with a useful data array.
				return n[0];
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
					return -1;
				}
				return -1;
			}
		}
	}
};
