using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public TKEnum.UnitCamp _bulletCamp= TKEnum.UnitCamp.UnitState_None;
	public Vector3 _moveDir;

	//子弹最大存活时间
	private float _maxLiveTime = 2.0F;
	//子弹已经存在的时间
	private float _livedTime = 0.0F;

	void Update()
	{
		Vector3 newPos = transform.position + _moveDir * 0.75F;
		newPos.y = TKEnum.ZOrder_Bullet;
		transform.position = newPos;

		_livedTime += Time.deltaTime;
		if (_livedTime >= _maxLiveTime)
		{
			this.destorySelf();
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag=="u_human")
		{
			Unit unit = collider.gameObject.GetComponent<Unit>();
			if (unit && unit._campType != this._bulletCamp)
			{
				int vv = -Random.Range(50, 120);
				unit.changeHpValue(vv);

				this.destorySelf();
			}
		}
	}

	private void destorySelf() {
		Destroy(this.gameObject);
	}

	public void setTargetPos(Vector3 pos) {
		_moveDir = pos - transform.position;
		_moveDir.Normalize();
	}
}
