using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
	void Start()
	{
		if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
			Destroy(gameObject);
		else
			DontDestroyOnLoad(gameObject);
	}
}
