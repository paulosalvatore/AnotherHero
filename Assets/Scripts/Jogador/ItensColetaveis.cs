using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetaveis : MonoBehaviour
{
	private ControladorCena controladorCena;

	void Update()
	{
		if (controladorCena == null)
			controladorCena = ControladorCena.Pegar();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			Debug.Log("LB or RB = pick");
			controladorCena.jogadorScript.inventarioScript.itemDisponivel = gameObject;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			if (controladorCena.jogadorScript.inventarioScript.itemDisponivel == gameObject)
				controladorCena.jogadorScript.inventarioScript.itemDisponivel = null;
		}
	}
}
