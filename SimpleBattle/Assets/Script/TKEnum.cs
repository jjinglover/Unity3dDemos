using System.Collections;
using System.Collections.Generic;

public class TKEnum
{
	public static float ZOrder_Unit = 0.5F;
	public static float ZOrder_Bullet = 0.6F;

	public enum UnitState {
		UnitState_None,
		UnitState_Move,
		UnitState_Atking,
		UnitState_CdAtking,
	}

	public enum UnitCamp
	{
		UnitState_None,
		UnitState_Self,
		UnitState_Enemy
	}
}
