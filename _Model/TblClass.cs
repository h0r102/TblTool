using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TblTool._Model
{
	class TblClass
	{
		Tbl[] m_Tbls;
		int m_TblLength;

		private string m_ClassName;

		public TblClass(string className)
		{
			m_Tbls = new Tbl[Const.TBL_MAX_LENGTH];
			m_TblLength = 0;

			m_ClassName = className;
		}

		public int AddTbl(out int tblNo)
		{
			tblNo = m_TblLength;
			m_TblLength++;

			m_Tbls[tblNo] = new Tbl(m_ClassName);
			return 0;
		}

		public int AddTbl(Step[] steps, out int tblNo)
		{
			tblNo = m_TblLength;
			m_TblLength++;

			m_Tbls[tblNo] = new Tbl(m_ClassName);
			Tbl tbl = m_Tbls[tblNo];
			for (int i = 0; i < steps.Length; i++)
			{
				int ret = tbl.AddStep(steps[i]);
				if (ret < 0)
				{
					return ret;
				}
			}
			return 0;
		}

		public int AddStep(int tblNo, int stepNo, string stepName, string funcName, int okStepNo, int next1StepNo, int next2StepNo)
		{
			if (tblNo >= m_TblLength)
			{
				return -1;
			}
			return m_Tbls[tblNo].AddStep(stepNo, stepName, funcName, okStepNo, next1StepNo, next2StepNo);
		}

		public int AddTblByText(string[] lines)
		{
			// TODO
			// 想定する文字列
			// RegStep()
			char[] delims = { ' ', ',', '.', ':', '\t', ';', '{', '}', '(', ')', '&' };
			
			int stepNo = 0;
			int tblNo = 0;
			
			foreach (string l in lines)
			{
				string[] words = l.Split(delims);

				if (words.Length == (int)Const.REGSTEP_TEXT.NUM_MAX)
				{
					if (words[0].Equals(Const.REGSTEP_FUNC_NAME))
					{
						// ステップ情報
						if (stepNo == 0)
						{
							AddTbl(out tblNo);
						}
						AddStepByText(tblNo, l);
						stepNo++;
					}
				}
				else if (words.Length == (int)Const.REGTBL_TEXT.NUM_MAX)
				{
					if (words[0].Equals(Const.REGTBL_FUNC_NAME))
					{
						// テーブル情報
						RegTblInfoByText(tblNo, l);
						stepNo = 0;
					}
				}
				else
				{
					continue;
				}
			}
			return 0;
		}

		public int RegTblInfoByText(int tblNo, string line)
		{
			if (tblNo >= m_TblLength)
			{
				return -1;
			}
			return m_Tbls[tblNo].RegTblInfoByText(line);
		}

		public int AddStepByText(int tblNo, string line)
		{
			if (tblNo >= m_TblLength)
			{
				return -1;
			}
			return m_Tbls[tblNo].AddStepByText(line);
		}

		public int StepInfosText(int tblNo, out string[] lines)
		{
			if (tblNo >= m_TblLength)
			{
				lines = null;
				return -1;
			}
			return m_Tbls[tblNo].StepInfos2Text(out lines);
		}

		public int TblInfo2Text(int tblNo, out string line)
		{
			if (tblNo >= m_TblLength)
			{
				line = "";
				return -1;
			}
			return m_Tbls[tblNo].TblInfo2Text(out line);
		}
	}
}
