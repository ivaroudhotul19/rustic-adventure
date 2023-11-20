using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.CompareTag("Coin")){
        Destroy(other.gameObject);
    }
   }
}
