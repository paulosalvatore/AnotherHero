using UnityEngine;
using System.Collections;

public class TrocarCenario : MonoBehaviour
{
	[Header("Destino")]
	public bool trocarCena;
	public int cenaDestinoId;

	private ControladorCena controladorCena;

	void Update()
	{
		if (controladorCena == null)
			controladorCena = ControladorCena.Pegar();
	}

	public void MudarCenario()
	{
		if (trocarCena)
			controladorCena.jogadorScript.IniciarMudancaCena(gameObject.name, cenaDestinoId);
		else
			controladorCena.jogadorScript.IniciarMudancaCenario(gameObject);
	}
}
