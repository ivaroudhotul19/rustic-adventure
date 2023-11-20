using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    public float delay; //set 1.5

	// Use this for initialization
	void Start () {
		Destroy (gameObject, delay);
	}
}
