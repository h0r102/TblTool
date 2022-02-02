using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TblTool._Model
{
	class Tbl
	{
		Step[] m_Steps;
		int m_StepLength;

		private string m_ClassName;

		private string m_TblType;
		private string m_TblName;
		private string m_TblHdl;
		private string m_Timeout;
		private string m_ErrRun;

		public Tbl(string className)
		{
			m_Steps = new Step[Const.STEP_MAX_LENGTH];
			m_StepLength = 0;

			m_ClassName = className;

			m_TblType = "";
			m_TblName = "";
			m_TblHdl = "";
			m_Timeout = "";
			m_ErrRun = "";
		}

		/// <summary>
		/// ステップ数を返す
		/// </summary>
		/// <returns>ステップ数</returns>
		public int Length()
		{
			return m_StepLength;
		}

		public int RegTblInfo(string tblType, string tblName, string tblHdl, string timeout, string errRun)
		{
			m_TblType = tblType;
			m_TblName = tblName;
			m_TblHdl = tblHdl;
			m_Timeout = timeout;
			m_ErrRun = errRun;
			return 0;
		}

		public int RegTblInfoByText(string line)
		{
			// 想定する文字列
			// RegTblInfo(....)
			char[] delims = { ' ', ',', '.', ':', '\t', ';', '{', '}', '(', ')', '&' };
			string[] words = line.Split(delims);
			if (words.Length != (int)Const.REGTBL_TEXT.NUM_MAX)
			{
				return -1;
			}

			m_TblType = words[(int)Const.REGTBL_TEXT.TBL_TYPE];
			m_TblName = words[(int)Const.REGTBL_TEXT.TBL_NAME];
			m_TblHdl = words[(int)Const.REGTBL_TEXT.TBL_HDL];
			m_Timeout = words[(int)Const.REGTBL_TEXT.TIMEOUT];
			m_ErrRun = words[(int)Const.REGTBL_TEXT.ERR_RUN];
			return 0;
		}

		/// <summary>
		/// ステップを追加する
		/// </summary>
		/// <param name="StepNo">ステップ番号</param>
		/// <param name="StepName">ステップ名</param>
		/// <param name="FuncName">関数名</param>
		/// <param name="OKStepNo">RC_OK時の遷移先ステップ番号</param>
		/// <param name="Next1StepNo">RC_NEXT1時の遷移先ステップ番号</param>
		/// <param name="Next2StepNo">RC_NEXT2時の遷移先ステップ番号</param>
		public int AddStep(int stepNo, string stepName, string funcName, int okStepNo, int next1StepNo, int next2StepNo)
		{
			if (stepNo >= Const.STEP_MAX_LENGTH)
			{
				return -1;
			}
			m_Steps[stepNo] = new Step(m_ClassName, stepNo, stepName, funcName, okStepNo, next1StepNo, next2StepNo);
			for (int i = 0; i < Const.STEP_MAX_LENGTH; i++)
			{
				if (m_Steps[i] == null)
				{
					m_StepLength = i;
					break;
				}
				else
				{
					if (m_Steps[i].StepNo != i)
					{
						m_StepLength = i;
						break;
					}
				}
			}
			return 0;
		}

		public int AddStep(in Step step)
		{
			int stepNo = step.StepNo;
			if (stepNo >= Const.STEP_MAX_LENGTH)
			{
				return -1;
			}
			m_Steps[stepNo] = step;
			for (int i = 0; i < Const.STEP_MAX_LENGTH; i++)
			{
				if (m_Steps[i] == null)
				{
					m_StepLength = i;
					break;
				}
				else
				{
					if (m_Steps[i].StepNo != i)
					{
						m_StepLength = i;
						break;
					}
				}
			}
			return 0;
		}

		public int AddStepByText(string line)
		{
			// 想定する文字列
			// RegStepInfo(....)
			char[] delims = { ' ', ',', '.', ':', '\t', ';', '{', '}', '(', ')', '&', '\"'};
			string[] words = line.Split(delims);
			if (words.Length != (int)Const.REGSTEP_TEXT.NUM_MAX)
			{
				return -1;
			}

			string className = words[(int)Const.REGSTEP_TEXT.CLASS_NAME];
			if (className.Equals(m_ClassName))
			{
				// クラス名が一致しないので不正
				return -1;
			}

			string sStepNo = words[(int)Const.REGSTEP_TEXT.STEP_NO];
			int stepNo;
			if (!int.TryParse(sStepNo, out stepNo))
			{
				return -1;
			}
			string stepName = words[(int)Const.REGSTEP_TEXT.STEP_NAME];
			string funcName = words[(int)Const.REGSTEP_TEXT.FUNC_NAME];
			string sOkStepNo = words[(int)Const.REGSTEP_TEXT.OK_STEP_NO];
			int okStepNo;
			if (!int.TryParse(sOkStepNo, out okStepNo))
			{
				return -1;
			}
			string sNext1StepNo = words[(int)Const.REGSTEP_TEXT.NEXT1_STEP_NO];
			int next1StepNo;
			if (!int.TryParse(sNext1StepNo, out next1StepNo))
			{
				return -1;
			}
			string sNext2StepNo = words[(int)Const.REGSTEP_TEXT.NEXT2_STEP_NO];
			int next2StepNo;
			if (!int.TryParse(sNext2StepNo, out next2StepNo))
			{
				return -1;
			}

			return AddStep(stepNo, stepName, funcName, okStepNo, next1StepNo, next2StepNo);
		}

		public int StepInfos2Text(out string[] lines)
		{
			lines = new string[m_StepLength];
			for (int stepNo = 0; stepNo < m_StepLength; stepNo++)
			{
				int ret = StepInfo2Text(stepNo, out lines[stepNo]);
				if (ret < 0)
				{
					return ret;
				}
			}
			return 0;
		}

		public int StepInfo2Text(int stepNo, out string line)
		{
			if (stepNo >= m_StepLength)
			{
				line = "";
				return -1;
			}
			return m_Steps[stepNo].StepInfo2Text(out line);
		}

		public int TblInfo2Text(out string line)
		{
			line = String.Format(
				"\t{0}({1}, \"{2}\", {3}, {4}, {5});"
				, Const.REGTBL_FUNC_NAME // 0
				, m_TblType // 1
				, m_TblName // 2
				, m_TblHdl // 3
				, m_Timeout // 4
				, m_ErrRun // 5
				);
			return 0;
		}

	}
}
