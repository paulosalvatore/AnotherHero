using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatPlayer : MonoBehaviour
{
	private ControladorCena controladorCena;

	void Update()
	{
		if (controladorCena == null)
			controladorCena = ControladorCena.Pegar();
		else
			transform.LookAt(controladorCena.jogador.transform);
	}
}
