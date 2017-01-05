using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jogador : MonoBehaviour
{
	internal List<GameObject> cameras = new List<GameObject>();
	internal GameObject[] camerasGO;
	internal List<Camera> camerasDisponiveis = new List<Camera>();

	internal bool mudandoCena = false;

	void Update()
	{
		foreach (GameObject camera in camerasGO)
		{
			if (camerasDisponiveis.Count > 0)
			{
				if (camera && camerasDisponiveis[0])
					camera.GetComponent<Camera>().enabled = (camerasDisponiveis[0].gameObject == camera ? true : false);
			}
		}
	}

	public void AtualizarCamerasCenario()
	{
		camerasGO = GameObject.FindGameObjectsWithTag("MainCamera");
		foreach (GameObject camera in camerasGO)
			camera.GetComponent<Camera>().enabled = false;
	}

	public void LimparCameras()
	{
		camerasGO = new GameObject[0];
		camerasDisponiveis = new List<Camera>();
	}
}