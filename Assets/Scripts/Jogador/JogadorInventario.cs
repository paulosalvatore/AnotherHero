using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorInventario : MonoBehaviour
{
	private GameObject maoEsquerda;
	private GameObject maoDireita;

	internal GameObject itemDisponivel;

	void Start()
	{
		maoEsquerda = GameObject.Find("E_mao").gameObject;
		maoDireita = GameObject.Find("D_mao").gameObject;
	}
	
	void Update()
	{
		if (itemDisponivel)
		{
			if (Input.GetButtonDown("LB"))
			{
				Debug.Log("LB Pressed");
				ColetarItem("esquerda");
			}
			else if (Input.GetButtonDown("RB"))
			{
				Debug.Log("RB Pressed");
				ColetarItem("direita");
			}
		}
	}

	void ColetarItem(string mao)
	{
		// Tocar Animação

		if (mao == "esquerda")
		{
			itemDisponivel.transform.parent = maoEsquerda.transform;

			itemDisponivel.transform.localPosition = new Vector3(
				0.0811f,
				0.0504f,
				0.0447f
			);
		}
		else if (mao == "direita")
		{

		}
	}
}
