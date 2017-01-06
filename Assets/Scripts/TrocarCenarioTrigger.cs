using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocarCenarioTrigger : MonoBehaviour
{
	private TrocarCenario trocarCenarioScript;

	void Start ()
	{
		trocarCenarioScript = transform.parent.GetComponent<TrocarCenario>();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			trocarCenarioScript.MudarCenario();
		}
	}
}
