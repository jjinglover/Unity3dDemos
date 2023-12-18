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

	//����ֵģ��
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
		Vector3 screenPosition;//���������������ת��Ϊ��Ļ����
		Vector3 mousePositionOnScreen;//��ȡ�������Ļ����Ļ����
		Vector3 mousePositionInWorld;//�������Ļ����Ļ����ת��Ϊ��������

		screenPosition = Camera.main.WorldToScreenPoint(_target.position);

		//��ȡ����ڳ���������
		mousePositionOnScreen = Input.mousePosition;

		//����������Z������ ���� ��������Ϸ�����Z������
		mousePositionOnScreen.z = screenPosition.z;

		//��������Ļ����ת��Ϊ��������
		mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

		//����Ϸ����������Ϊ�����������꣬�����������ƶ�

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
