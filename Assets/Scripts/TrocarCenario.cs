using UnityEngine;
using System.Collections;

public class TrocarCenario : MonoBehaviour
{
	[Header("Destino")]
	public int cenaDestinoId;

	private ControladorCena controladorCena;

	void Start()
	{
		controladorCena = ControladorCena.Pegar();
	}

	public void MudarCena()
	{
		if (controladorCena.jogadorScript.mudandoCena)
			return;

		controladorCena.jogadorMovimentoScript.AlterarMovimento(true);
		controladorCena.saida = gameObject.name;
		controladorCena.CarregarCena(cenaDestinoId);
	}
}
