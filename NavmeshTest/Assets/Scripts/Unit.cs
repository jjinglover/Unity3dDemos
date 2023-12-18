using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	private NavMeshAgent _agent=null;
	private float _defaultSpeed = 0f;
	private void Start()
	{
		Debug.Log("start");
		_agent = gameObject.GetComponent<NavMeshAgent>();
		if (_agent) {
			_defaultSpeed = _agent.speed;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log(collider.name);
		if (collider.name == "agent2(Clone)") {
			Debug.Log("collision start");
			if (_agent) {
				_agent.speed = _defaultSpeed * 0.1f;
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.name == "agent2(Clone)")
		{
			Debug.Log("collision end");
			if (_agent) {
				_agent.speed = _defaultSpeed;
			}
		}
	}
}
