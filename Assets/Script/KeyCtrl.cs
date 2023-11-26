using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.UpdateKeyCount();

            AudioCtrl.instance.KeyFound(other.gameObject.transform.position);

            SFXCtrl.instance.ShowKeySparkle(other.gameObject.transform.position);

            Destroy(gameObject);
        }
	}
}

