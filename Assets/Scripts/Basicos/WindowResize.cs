using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowResize : MonoBehaviour
{
	private int[] resolucao = new int[2];
	private ControladorCena controladorCena;

	void Start ()
	{
		controladorCena = ControladorCena.Pegar();
		resolucao[0] = Screen.width;
		resolucao[1] = Screen.height;
	}
	
	void Update ()
	{
		ChecarResolucao();
	}

	void ChecarResolucao()
	{
		if (resolucao[0] != Screen.width || resolucao[1] != Screen.height)
		{
			resolucao[0] = Screen.width;
			resolucao[1] = Screen.height;
			ResolucaoAlterada();
		}
	}

	void ResolucaoAlterada()
	{
		AlterarTamanhoFonteTextoNpc();
	}

	void AlterarTamanhoFonteTextoNpc()
	{
		int novoTamanho = controladorCena.textoNpc.fontSize;

		if (Screen.width <= 1024 && Screen.height <= 768)
			novoTamanho = 50;
		else if (Screen.width <= 1366 && Screen.height <= 768)
			novoTamanho = 60;
		else if (Screen.width <= 1920 && Screen.height <= 1080)
			novoTamanho = 64;

		controladorCena.textoNpc.fontSize = novoTamanho;
	}
}
