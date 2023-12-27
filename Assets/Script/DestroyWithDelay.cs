using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    public float delay; //set 1.5 
	void Start () {
		Destroy (gameObject, delay);
	}
}
