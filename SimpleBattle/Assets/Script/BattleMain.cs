using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMain : MonoBehaviour
{
	public static BattleMain _instance = null;
	private UnitFactory _unitFactory;
	[HideInInspector] public List<Unit> _selfList, _enemyList;
	public GameObject _mapObject;
	public Canvas _battleCanvas = null;
	public Collider _leftBirthPlane = null, _rightBirthPlane = null;
	public Transform _leftBirthPos = null;
	public Transform _rightBirthPos = null;

	[Header("ÊÇ·ñ¿ªÆô×Óµ¯")]
	public bool _openBullet = true;
	[Header("¹¥»÷¼ä¸ô/Ãë")]
	public float _atkIntval = 10.0f;

	void Start()
    {
		_instance = this;
		_unitFactory = GetComponent<UnitFactory>();
		_selfList = new List<Unit>();
        _enemyList = new List<Unit>();
	}

    // Update is called once per frame
    void Update()
    {
		this.checkAddAgent();

		if (_selfList.Count > 0)
		{
			foreach (Unit unit in _enemyList)
			{
				if (unit)
				{
					if (unit.getTarget() == null || unit.getTarget() == _leftBirthPos)
					{
						Unit tt = getWillFoucsUnit(unit, _selfList);
						if (tt)
						{
							unit.setTarget(tt.transform);
						}
					}
				}
			}
		}
		else 
		{
			foreach (Unit unit in _enemyList)
			{
				unit.setTarget(_leftBirthPos);
			}
		}

		if (_enemyList.Count > 0)
		{
			foreach (Unit unit in _selfList)
			{
				if (unit)
				{
					if (unit.getTarget() == null || unit.getTarget() == _rightBirthPos)
					{
						Unit tt = getWillFoucsUnit(unit, _enemyList);
						if (tt)
						{
							unit.setTarget(tt.transform);
						}
					}
				}
			}
		}
		else
		{
			foreach (Unit unit in _selfList)
			{
				unit.setTarget(_rightBirthPos);
			}
		}
	}

	private Unit getWillFoucsUnit(Unit inUnit, List<Unit> lists) {
		Unit foucsUnit = null;
		if (inUnit)
		{
			double maxDistance = 0xffffffff;
			foreach (Unit unit in lists)
			{
				float dis = Vector3.Distance(inUnit.transform.position, unit.transform.position);

				if (dis <= maxDistance)
				{
					maxDistance = dis;
					foucsUnit = unit;
				}
			}
		}
		return foucsUnit;
	}

	public void deleteUnit(Unit delUnit) {
		_selfList.Remove(delUnit);
		_enemyList.Remove(delUnit);
	}

	public void showHpChangeAni(int change, Vector3 pos) 
	{
		if (change != 0)
		{
			GameObject prefab = Resources.Load<GameObject>("prefabs/changeHp");
			GameObject obj = Instantiate(prefab, _battleCanvas.transform);
			obj.transform.position = Camera.main.WorldToScreenPoint(pos);
			HpChangeTxt txt = obj.GetComponent<HpChangeTxt>();
			if (txt)
			{
				txt.Init(change);
			}
		}
	}

	private void checkAddAgent()
	{
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				TKEnum.UnitCamp camp = TKEnum.UnitCamp.UnitState_None;
				if (hit.collider == _leftBirthPlane)
				{
					camp = TKEnum.UnitCamp.UnitState_Self;
				}
				else if (hit.collider == _rightBirthPlane)
				{
					camp = TKEnum.UnitCamp.UnitState_Enemy;
				}

				if (camp != TKEnum.UnitCamp.UnitState_None)
				{
					Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					mousePositionInWorld.y = 0;
					this.addAgent(camp, mousePositionInWorld);
				}
			}
		}
	}

	private void addAgent(TKEnum.UnitCamp campType,Vector3 bPos) 
	{
		string fileName = campType == TKEnum.UnitCamp.UnitState_Self ? "prefabs/agent1" : "prefabs/agentEnemy1";
		if (fileName.Length > 0)
		{
			Unit unit = _unitFactory.createUnit(_mapObject.transform, fileName);
			if (unit != null)
			{
				unit.transform.position = new Vector3(bPos.x, 0, bPos.z);
				unit._atkLen = 10;
				unit._closeBullet = true;
				unit._campType = campType;

				if (campType == TKEnum.UnitCamp.UnitState_Self)
				{
					_selfList.Add(unit);
				}
				else if (campType == TKEnum.UnitCamp.UnitState_Enemy)
				{
					_enemyList.Add(unit);
				}
			}
		}
	}
}
