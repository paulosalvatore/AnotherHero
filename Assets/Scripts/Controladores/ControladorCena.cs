using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCena : MonoBehaviour
{
	// Pausar
	private Pausar pausar;
	internal bool jogoPausado;

	// Informações da Cena
	[Header("Cena")]
	public float delayCarregamentoCena;
	public int cenaInicialId;
	internal int cenaAtiva;
	private int carregarCenaId;

	// Fade
	[Header("Fade")]
	public GameObject fade;
	private Animator fadeAnimator;

	// Jogo
	private bool jogoRodando = false;

	// Jogador
	[Header("Jogador")]
	public GameObject jogadorPrefab;
	private GameObject jogador;
	internal Jogador jogadorScript;
	internal string saida;

	void Start()
	{
		fadeAnimator = Instantiate(fade).GetComponent<Animator>();

		pausar = GetComponent<Pausar>();

		if (!jogador)
		{
			jogador = Instantiate(jogadorPrefab) as GameObject;
			jogador.name = jogador.name.Replace("(Clone)", "");
			jogadorScript = jogador.GetComponent<Jogador>();
			jogador.GetComponent<Rigidbody>().isKinematic = true;
		}

		if (SceneManager.GetActiveScene().buildIndex == 0)
			CarregarCena(cenaInicialId);
	}

	void Update()
	{
		if (jogoRodando)
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		else
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		cenaAtiva = SceneManager.GetActiveScene().buildIndex;
	}

	public void CarregarCena(int cenaId)
	{
		if (jogoPausado)
			pausar.DespausarJogo();
		
		carregarCenaId = cenaId;
		StartCoroutine(CarregarCenaAposDelay());
	}

	IEnumerator CarregarCenaAposDelay()
	{
		fadeAnimator.SetTrigger("fadeOut");

		yield return new WaitForSeconds(delayCarregamentoCena);

		SceneManager.LoadScene(carregarCenaId);
		carregarCenaId = 0;

		fadeAnimator.SetTrigger("fadeIn");
	}

	public void Fade(string fade = null)
	{
		if (fade != null)
			fadeAnimator.SetTrigger(fade);
		else
		{
			fadeAnimator.SetTrigger("fadeOut");
			fadeAnimator.SetTrigger("fadeIn");
		}
	}

	public void Sair()
	{
		#if UNITY_STANDALONE
			Application.Quit();
		#endif

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	static public ControladorCena Pegar(GameObject controladorCenaPrefab = null)
	{
		GameObject controladorCenaObjeto = GameObject.Find("ControladorCena");

		if (!controladorCenaObjeto && controladorCenaPrefab)
		{
			controladorCenaObjeto = Instantiate(controladorCenaPrefab) as GameObject;
			controladorCenaObjeto.name = controladorCenaObjeto.name.Replace("(Clone)", "");
		}

		return (controladorCenaObjeto ? controladorCenaObjeto.GetComponent<ControladorCena>() : null);
	}
}
