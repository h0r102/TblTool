using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TblTool
{
	class Const
	{
		public const int TBL_MAX_LENGTH = 256;
		public const int STEP_MAX_LENGTH = 256;
		public const string REGSTEP_FUNC_NAME = "RegStepInfo";
		public const string REGTBL_FUNC_NAME = "RegTblInfo";

		public enum REGSTEP_TEXT : int
		{
			REGFUNC_NAME = 0,
			TBL_NAME,
			STEP_NO,
			STEP_NAME,
			CLASS_NAME,
			FUNC_NAME,
			OK_STEP_NO,
			NEXT1_STEP_NO,
			NEXT2_STEP_NO,
			NUM_MAX
		}
		public enum REGTBL_TEXT : int
		{
			REGFUNC_NAME = 0,
			TBL_TYPE,
			TBL_NAME,
			TBL_HDL,
			TIMEOUT,
			ERR_RUN,
			NUM_MAX
		}
	}
}
