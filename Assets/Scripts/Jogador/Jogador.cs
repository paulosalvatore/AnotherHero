using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jogador : MonoBehaviour
{
	// Câmeras
	internal List<GameObject> cameras = new List<GameObject>();
	internal GameObject[] camerasGO;
	internal List<Camera> camerasDisponiveis = new List<Camera>();

	// Cena/Cenário
	private ControladorCena controladorCena;
	private bool mudandoCena = true;
	private bool mudandoCenario = false;
	private GameObject destino;

	// Jogador
	internal JogadorMovimento movimentoScript;
	internal JogadorAnimation animationScript;
	internal JogadorInventario inventarioScript;

	void Awake()
	{
		movimentoScript = GetComponent<JogadorMovimento>();
		animationScript = GetComponent<JogadorAnimation>();
		inventarioScript = GetComponent<JogadorInventario>();
		controladorCena = ControladorCena.Pegar();
	}

	void Update()
	{
		AtivarCameraDisponivel();
	}

	void AtivarCameraDisponivel()
	{
		if (camerasGO == null || camerasGO.Length == 0 || camerasDisponiveis.Count == 0)
			return;

		foreach (GameObject camera in camerasGO)
			if (camera && camerasDisponiveis[0])
				camera.GetComponent<Camera>().enabled = (camerasDisponiveis[0].gameObject == camera ? true : false);
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

	public void IniciarMudancaCena(string saida, int cenaDestinoId)
	{
		if (mudandoCena)
			return;

		mudandoCena = true;

		movimentoScript.AlterarMovimento(false);
		
		controladorCena.CarregarCena(cenaDestinoId);
		controladorCena.saida = saida;

		LimparCameras();
	}

	public void FinalizaMudancaCena()
	{
		if (!mudandoCena)
			return;

		movimentoScript.AlterarMovimento(true);

		AtualizarCamerasCenario();

		controladorCena.saida = null;

		destino = null;

		mudandoCena = false;
	}

	void DefinirDestino(GameObject saida)
	{
		GameObject[] checarSaidas = GameObject.FindGameObjectsWithTag("Saída");
		foreach (GameObject checarSaida in checarSaidas)
			if (checarSaida.name == saida.name && saida != checarSaida)
				destino = checarSaida;
	}

	public void IniciarMudancaCenario(GameObject saida)
	{
		if (mudandoCenario)
			return;

		mudandoCenario = true;

		movimentoScript.AlterarMovimento(false);

		DefinirDestino(saida);

		StartCoroutine(AlterarPosicaoJogadorAposDelay());
	}

	void FinalizarMudancaCenario()
	{
		if (!mudandoCenario)
			return;

		movimentoScript.AlterarMovimento(true);

		destino = null;

		mudandoCenario = false;
	}

	IEnumerator AlterarPosicaoJogadorAposDelay()
	{
		controladorCena.Fade("fadeOut");

		yield return new WaitForSeconds(controladorCena.delayCarregamentoCena);
		
		transform.position = destino.transform.position;

		FinalizarMudancaCenario();

		controladorCena.Fade("fadeIn");
	}
}