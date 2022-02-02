using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TblTool._Model
{
	class FileIOCpp
	{
		string m_Filename;

		public FileIOCpp()
		{
			m_Filename = "";
		}

		public int SetFilename(string filename)
		{
			m_Filename = filename;
			return 0;
		}

		public int ReadFile(out string[] lines)
		{
			lines = System.IO.File.ReadAllLines(m_Filename);
			return 0;
		}

		public int ReadFile(string filename, out string[] lines)
		{
			SetFilename(filename);
			lines = System.IO.File.ReadAllLines(m_Filename);
			return 0;
		}

		public int WriteFile(string[] lines)
		{
			System.IO.File.WriteAllLines(m_Filename, lines);
			return 0;
		}

		public int WriteFile(string filename, string[] lines)
		{
			SetFilename(filename);
			System.IO.File.WriteAllLines(m_Filename, lines);
			return 0;
		}
	}
}
