using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorInventario : MonoBehaviour
{
	private GameObject maoEsquerda;
	private GameObject maoDireita;

	internal GameObject lanterna;
	internal GameObject pa;

	public GameObject lanternaPrefab;
	public GameObject paPrefab;

	internal GameObject itemDisponivel;
	internal GameObject interacaoDisponivel;
	internal GameObject interacaoDestino;

	private JogadorAnimation animationScript;

	void Start()
	{
		//maoEsquerda = GameObject.Find("E_mao");
		//maoDireita = GameObject.Find("D_mao");

		lanterna = GameObject.Find("lantern");
		pa = GameObject.Find("shovel");

		lanterna.SetActive(false);
		pa.SetActive(false);

		animationScript = GetComponent<JogadorAnimation>();
	}
	
	void Update()
	{
		if (!interacaoDisponivel && itemDisponivel)
		{
			if (Input.GetButtonDown("LB"))
			{
				ColetarItem();
			}
			else if (Input.GetButtonDown("RB"))
			{
				ColetarItem();
			}
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
				if (lanterna.activeSelf && Input.GetButton("LB"))
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

			transform.position = alavanca.posicaoJogador;
			transform.rotation = new Quaternion(
				alavanca.rotacaoJogador.x,
				alavanca.rotacaoJogador.y,
				alavanca.rotacaoJogador.z,
				alavanca.rotacaoJogador	.w
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
			prefab = lanternaPrefab;

		GameObject instancia = Instantiate(prefab, transform.position, paPrefab.transform.rotation) as GameObject;

		instancia.transform.position = new Vector3(
			instancia.transform.position.x,
			0.55f,
			instancia.transform.position.z
		);

		instancia.name = instancia.name.Replace("(Clone)", "");

		//if (item.name == "lanterna")

		//item.GetComponent<ItensColetaveis>().itemColetado = true;
		//item.parent = null;
		//item.position = transform.position;
	}

	void ColetarItem()
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
		
		itemDisponivel.GetComponent<ItensColetaveis>().itemColetado = true;
		itemDisponivel = null;

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
