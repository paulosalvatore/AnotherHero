using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCamera : MonoBehaviour
{
	private ControladorCena controladorCena;

	void Update()
	{
		if (controladorCena == null)
			controladorCena = ControladorCena.Pegar();
		else if (Camera.main)
			transform.LookAt(Camera.main.transform);
	}
}
