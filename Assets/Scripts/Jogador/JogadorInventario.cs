using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorInventario : MonoBehaviour
{
	private GameObject maoEsquerda;
	private GameObject maoDireita;

	internal GameObject lanterna;
	internal GameObject pa;

	private GameObject lanternaPrefab;
	private GameObject paPrefab;

	internal GameObject itemDisponivel;
	internal GameObject interacaoDisponivel;
	internal GameObject interacaoDestino;

	private Jogador jogadorScript;
	private JogadorAnimation animationScript;

	void Start()
	{
		lanternaPrefab = GameObject.Find("LanternaPrefab");
		paPrefab = GameObject.Find("PáPrefab");

		lanterna = GameObject.Find("lantern");
		pa = GameObject.Find("shovel");

		lanterna.SetActive(false);
		pa.SetActive(false);

		jogadorScript = GetComponent<Jogador>();
		animationScript = jogadorScript.animationScript;
	}
	
	void Update()
	{
		if (!interacaoDisponivel && itemDisponivel)
		{
			/*if (Input.GetButtonDown("LB"))
			{
				ColetarItem();
			}
			else if (Input.GetButtonDown("RB"))
			{
				ColetarItem();
			}*/
		}
		else if (interacaoDisponivel)
		{
			if (Input.GetButton("LB") && Input.GetButton("RB"))
			{
				IniciarInteracao();
			}
		}
		else if (lanterna.activeSelf || pa.activeSelf)
		{
			if (Input.GetAxisRaw("7th Axis") == -1)
			{
                if (lanterna.activeSelf)
                    //if (lanterna.activeSelf && Input.GetButton("LB"))
                    DroparItem(lanterna);
				else if (pa.activeSelf && Input.GetButton("RB"))
					DroparItem(pa);
			}
		}
	}

	void IniciarInteracao()
	{
		if (!lanterna.activeSelf && !pa.activeSelf)
		{
			animationScript.alavanca = true;

			interacaoDestino.transform.position = new Vector3(
				interacaoDestino.transform.position.x,
				12f,
				interacaoDestino.transform.position.z
			);

			Alavanca alavanca = interacaoDisponivel.GetComponent<Alavanca>();
			alavanca.interacaoDisponivel = false;
			alavanca.portaoNpc.SetActive(false);

			transform.position = alavanca.posicaoJogador;
			transform.rotation = new Quaternion(
				alavanca.rotacaoJogador.x,
				alavanca.rotacaoJogador.y,
				alavanca.rotacaoJogador.z,
				alavanca.rotacaoJogador.w
			);

			interacaoDisponivel = null;

			Debug.Log("Alavanca Pressionada e Desativada. Portão Aberto.");
		}
		else
			Debug.Log("Eu preciso das duas mãos para fazer isso.");
	}

	void DroparItem(GameObject item)
	{
		Debug.Log("Dropar item " + item.name + ".");

		item.SetActive(false);

		GameObject prefab = null;
		if (item.name == "lantern")
			prefab = lanternaPrefab;
		else if (item.name == "shovel")
			prefab = paPrefab;

		prefab.GetComponent<ItensColetaveis>().itemColetado = false;

		prefab.SetActive(true);

		prefab.transform.position = new Vector3(
			transform.position.x + 1f,
			1f,
			transform.position.z
		);

		//if (item.name == "lanterna")

		//item.GetComponent<ItensColetaveis>().itemColetado = true;
		//item.parent = null;
		//item.position = transform.position;
	}

	public void ColetarItem()
	{
		Debug.Log("Coletar item " + itemDisponivel.name + ".");

		// Tocar Animação

		if (lanterna.activeSelf)
			DroparItem(lanterna);
		else if (pa.activeSelf)
			DroparItem(pa);

		if (itemDisponivel.name == "LanternaPrefab")
			lanterna.SetActive(true);
		else if (itemDisponivel.name == "PáPrefab")
			pa.SetActive(true);

		if (itemDisponivel.transform.FindChild("Camera"))
		{
			Destroy(itemDisponivel.transform.FindChild("Camera").gameObject);
			itemDisponivel.GetComponent<ItensColetaveis>().camera = null;
			itemDisponivel.GetComponent<FalaNpc>().frases = new List<string>();
		}

		jogadorScript.forcarCameraDisponivel = null;

		itemDisponivel.GetComponent<ItensColetaveis>().itemColetado = true;
		itemDisponivel = null;

		jogadorScript.forcarCamera = null;

		/*
		itemDisponivel.transform.parent = maoEsquerda.transform;

		if (mao == "esquerda")
		{
			itemDisponivel.transform.localPosition = new Vector3(
				0.0811f,
				0.0504f,
				0.0447f
			);
		}
		else if (mao == "direita")
		{
			itemDisponivel.transform.localPosition = new Vector3(
				0,
				0,
				0
			);
		}
		*/
	}
}
