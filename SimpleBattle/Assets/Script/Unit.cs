using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	//是否关闭子弹【测试使用】
	public bool _closeBullet = false;

	[HideInInspector] public NavMeshAgent _agent = null;
	//目标单位
	[HideInInspector] private Transform _target = null;

	//攻击距离
	public float _atkLen = 0.0F;
	//单位阵营
	public TKEnum.UnitCamp _campType = TKEnum.UnitCamp.UnitState_None;
	//单位当前的状态
	private TKEnum.UnitState _unitState = TKEnum.UnitState.UnitState_Move;
	//单位生命值
	private int _unitHpValue = 566;

	//【子弹模块】
	//攻击间隔
	public float _atkIntval = 2.0F;
	//攻击cd
	private float _atkCd = 0.0F;

	public void initSet()
	{
		Vector3 angle = transform.localEulerAngles;
		angle.y = 90;
		transform.localEulerAngles = angle;

		_agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		_closeBullet = !BattleMain._instance._openBullet;
		_atkIntval = BattleMain._instance._atkIntval;

		if (_campType != TKEnum.UnitCamp.UnitState_None)
		{
			NavMeshAgent agent = _agent;
			if (agent.isStopped == false && _target)  
			{
				bool stop = false;
				if (_unitState == TKEnum.UnitState.UnitState_Move)
				{
					Vector3 targetPos = _target.position;
					agent.SetDestination(targetPos);
					float disSeq = Vector3.Distance(targetPos, transform.position);
					if (disSeq < _atkLen)
					{
						stop = true;
					}
				}
				this.stopMoveOrNot(stop);
			}
		}

		this.updateUnitState();

		if (this.canAtking())
		{
			this.createBullet(BattleMain._instance._mapObject.transform);
		}
	}

	//更新单位状态
	public void updateUnitState()
	{
		switch (_unitState)
		{
			case TKEnum.UnitState.UnitState_Move:
				_atkCd = 0;
				break;
			case TKEnum.UnitState.UnitState_CdAtking:
				{
					_atkCd += Time.deltaTime;
					if (_atkCd >= _atkIntval)
					{
						_unitState = TKEnum.UnitState.UnitState_Atking;
						_atkCd = 0;
					}
				}
				break;
			default:
				break;
		}
	}

	public void stopMoveOrNot(bool stop)
	{
		if (stop == false)
		{
			_unitState = TKEnum.UnitState.UnitState_Move;
		}

		_agent.isStopped = stop;
		if (stop)
		{
			_unitState = TKEnum.UnitState.UnitState_Atking;
			_agent.SetDestination(transform.position);
		}
	}

	public bool canAtking() {
		if (_closeBullet) {
			return false;
		}
		return _unitState == TKEnum.UnitState.UnitState_Atking;
	}

	public void createBullet(Transform parent) {
		if (_target) {
			_unitState = TKEnum.UnitState.UnitState_CdAtking;

			GameObject prefab = Resources.Load<GameObject>("prefabs/bulleball");
			GameObject obj = GameObject.Instantiate(prefab, parent);
			obj.transform.position = transform.position;
			obj.AddComponent<Bullet>();
			Bullet bullet = obj.GetComponent<Bullet>();
			bullet.setTargetPos(_target.position);
			bullet._bulletCamp = this._campType;
		}
	}

	public void changeHpValue(int value) {

		BattleMain._instance.showHpChangeAni(value, transform.position);

		_unitHpValue += value;
		if (_unitHpValue<=0) {
			BattleMain._instance.deleteUnit(this);
			Destroy(this.gameObject);
		}
	}

	public void setTarget(Transform target) {
		if (_target != target) 
		{
			float dis = Vector3.Distance(target.position, transform.position);
			if (dis > _atkLen)
			{
				this.stopMoveOrNot(false);
			}
		}
		_target = target;
	}

	public Transform getTarget(){
		return _target;
	}
}
