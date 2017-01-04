using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixaTrigger : MonoBehaviour
{
	internal Camera camera;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			Jogador jogador = collider.GetComponent<Jogador>();
			jogador.camerasDisponiveis.Add(camera);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			Jogador jogador = collider.GetComponent<Jogador>();
			jogador.camerasDisponiveis.Remove(camera);
		}
	}
}
