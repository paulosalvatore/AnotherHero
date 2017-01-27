using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoAuxilio : MonoBehaviour
{
	public void Ativar()
	{
		gameObject.SetActive(true);
	}

	public void Desativar()
	{
		gameObject.SetActive(false);
	}
}
