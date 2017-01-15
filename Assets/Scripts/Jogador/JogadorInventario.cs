using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorInventario : MonoBehaviour
{
	private GameObject maoEsquerda;
	private GameObject maoDireita;

	internal GameObject itemDisponivel;
	internal GameObject interacaoDisponivel;
	internal GameObject interacaoDestino;

	void Start()
	{
		maoEsquerda = GameObject.Find("E_mao").gameObject;
		maoDireita = GameObject.Find("D_mao").gameObject;
	}
	
	void Update()
	{
		if (!interacaoDisponivel && itemDisponivel)
		{
			if (Input.GetButtonDown("LB"))
			{
				ColetarItem("esquerda");
			}
			else if (Input.GetButtonDown("RB"))
			{
				ColetarItem("direita");
			}
		}
		else if (interacaoDisponivel)
		{
			if (Input.GetButton("LB") && Input.GetButton("RB"))
			{
				IniciarInteracao();
			}
		}

		if (Input.GetAxisRaw("LT") == 1 && maoEsquerda.transform.childCount > 0)
		{
			DroparItem(maoEsquerda.transform.GetChild(0));
		}
		else if (Input.GetAxisRaw("RT") == 1 && maoDireita.transform.childCount > 0)
		{
			DroparItem(maoDireita.transform.GetChild(0));
		}
	}

	void IniciarInteracao()
	{
		if (maoEsquerda.transform.childCount == 0 && maoDireita.transform.childCount == 0)
		{
			interacaoDestino.transform.position = new Vector3(
				interacaoDestino.transform.position.x,
				12f,
				interacaoDestino.transform.position.z
			);

			interacaoDisponivel.GetComponent<Alavanca>().interacaoDisponivel = false;

			interacaoDisponivel = null;

			Debug.Log("Alavanca Pressionada e Desativada. Portão Aberto.");
		}
		else
			Debug.Log("Eu preciso das duas mãos para fazer isso.");
	}

	void DroparItem(Transform item)
	{
		Debug.Log("Dropar item " + item.name + ".");

		item.parent = null;
		item.position = transform.position;
	}

	void ColetarItem(string mao)
	{
		// Tocar Animação

		if (maoEsquerda.transform.childCount > 0)
			DroparItem(maoEsquerda.transform.GetChild(0));

		Debug.Log("Coleta item " + itemDisponivel.name);

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

		itemDisponivel = null;
	}
}
