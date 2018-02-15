using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour {

    public GameObject prefab;
    [SerializeField] string prefabName;

	// Use this for initialization
	void Start () {
        prefab = (GameObject)Resources.Load(prefabName, typeof(GameObject));
	}
}
