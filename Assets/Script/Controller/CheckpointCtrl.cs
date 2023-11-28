using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCtrl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            PlayerPrefs.SetFloat("CPX", other.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("CPY", other.gameObject.transform.position.y);
        }
    }
}
