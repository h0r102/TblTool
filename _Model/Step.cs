using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TblTool._Model
{
	class Step
	{
		private string m_ClassName;

		private int m_StepNo;
		private string m_StepName;
		private string m_FuncName;
		private int m_OkStepNo;
		private int m_Next1StepNo;
		private int m_Next2StepNo;

		public int StepNo { get { return m_StepNo; } }

		/// <summary>
		/// ステップの内容をセットする
		/// </summary>
		/// <param name="stepNo">ステップ番号</param>
		/// <param name="stepName">ステップ名</param>
		/// <param name="className">クラス名</param>
		/// <param name="funcName">関数名</param>
		/// <param name="okStepNo">RC_OK時の遷移先ステップ番号</param>
		/// <param name="next1StepNo">RC_NEXT1時の遷移先ステップ番号</param>
		/// <param name="next2StepNo">RC_NEXT2時の遷移先ステップ番号</param>
		public Step(string className, int stepNo, string stepName, string funcName, int okStepNo, int next1StepNo, int next2StepNo)
		{
			m_ClassName = className;

			m_StepNo = stepNo;
			m_StepName = stepName;
			m_FuncName = funcName;
			m_OkStepNo = okStepNo;
			m_Next1StepNo = next1StepNo;
			m_Next2StepNo = next2StepNo;
		}

		public int RegStepInfo(int stepNo, string stepName, string funcName, int okStepNo, int next1StepNo, int next2StepNo)
		{
			m_StepNo = stepNo;
			m_StepName = stepName;
			m_FuncName = funcName;
			m_OkStepNo = okStepNo;
			m_Next1StepNo = next1StepNo;
			m_Next2StepNo = next2StepNo;
			return 0;
		}

		public int StepInfo2Text(out string line)
		{
			line = String.Format(
				"\t{0}({{ {1}, \"{2}\", &{3}::{4}, {5}, {6}, {7} }});"
				, Const.REGSTEP_FUNC_NAME // 0
				, m_StepNo // 1
				, m_StepName // 2
				, m_ClassName // 3
				, m_FuncName // 4
				, m_OkStepNo // 5
				, m_Next1StepNo // 6
				, m_Next2StepNo // 7
				);
			return 0;
		}




	}
}
