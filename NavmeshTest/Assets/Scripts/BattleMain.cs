using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BattleMain : MonoBehaviour
{
	public static BattleMain _instance = null;
	private List<NavMeshAgent> _enemy1List= new List<NavMeshAgent>();
	private List<NavMeshAgent> _enemy2List = new List<NavMeshAgent>();

	public GameObject _mapObject;
	public Transform _target1, _target2, _target;
	public Collider _target1Collider, _target2Collider;
	public NavMeshAgent _agent1, _agent2;

	//生命值模块
	private bool m_inDraging = false;
	// Start is called before the first frame update
	void Start()
    {
		_instance = this;
	}

    // Update is called once per frame
    void Update()
    {
		this.checkAddAgent();

		foreach (NavMeshAgent agent in _enemy1List)
		{
			agent.SetDestination(_target1.position);
		}

		foreach (NavMeshAgent agent in _enemy2List)
		{
			agent.SetDestination(_target2.position);
		}
		//test move target
		this.checkMoveTargetSuc();
	}

	private bool checkMoveTargetSuc() {

		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if (hit.collider == _target1Collider)
				{
					m_inDraging = true;
					_target = _target1;
				}
				else if(hit.collider == _target2Collider) {
					m_inDraging = true;
					_target = _target2;
				}
			}
		}
		else if (Input.GetMouseButtonUp(0)) {
			m_inDraging = false;
		}

		if (m_inDraging) {
			this.updateTargetPos();
			return true;
		}
		return false;
	}

	private void updateTargetPos()
	{
		Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
		Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
		Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标

		screenPosition = Camera.main.WorldToScreenPoint(_target.position);

		//获取鼠标在场景中坐标
		mousePositionOnScreen = Input.mousePosition;

		//让鼠标坐标的Z轴坐标 等于 场景中游戏对象的Z轴坐标
		mousePositionOnScreen.z = screenPosition.z;

		//将鼠标的屏幕坐标转化为世界坐标
		mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

		//将游戏对象的坐标改为鼠标的世界坐标，物体跟随鼠标移动

		_target.position = new Vector3(mousePositionInWorld.x, Mathf.Max(0,mousePositionInWorld.y), mousePositionInWorld.z);
	}

	private void checkAddAgent() {
		int randX = 0;// 4 - Random.Range(0, 8);
		float randZ = -3F;
		if (Input.GetKeyDown(KeyCode.Q))
		{
			NavMeshAgent tmpAgent = Instantiate(_agent1);
			tmpAgent.transform.position = new Vector3(randX, 0, randZ);
			tmpAgent.enabled = true;
			tmpAgent.gameObject.AddComponent<Unit>();
			_enemy1List.Add(tmpAgent);
		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			NavMeshAgent tmpAgent = Instantiate(_agent2);
			tmpAgent.transform.position = new Vector3(randX, 0, randZ);
			tmpAgent.enabled = true;
			tmpAgent.gameObject.AddComponent<Unit>();
			_enemy2List.Add(tmpAgent);
		}
	}
}
