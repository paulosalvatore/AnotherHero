using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{
	public GameObject portao;
	internal bool interacaoDisponivel = true;

	private ControladorCena controladorCena;

	void Update()
	{
		if (controladorCena == null)
			controladorCena = ControladorCena.Pegar();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!interacaoDisponivel)
			return;

		if (collider.CompareTag("Player"))
		{
			Debug.Log("LB + RB = interact");
			controladorCena.jogadorScript.inventarioScript.interacaoDisponivel = gameObject;
			controladorCena.jogadorScript.inventarioScript.interacaoDestino = portao;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (!interacaoDisponivel)
			return;

		if (collider.CompareTag("Player"))
		{
			if (controladorCena.jogadorScript.inventarioScript.interacaoDisponivel == gameObject)
				controladorCena.jogadorScript.inventarioScript.interacaoDisponivel = null;
		}
	}
}
