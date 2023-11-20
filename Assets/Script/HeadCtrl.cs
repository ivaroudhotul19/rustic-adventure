using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeadCtrl : MonoBehaviour
{
    public UI ui;
    public GameData data;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Breakable")) {
            SFXCtrl.instance.HandleBoxBreaking(other.gameObject.transform.parent.transform.position);
            gameObject.transform.parent.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        if (other.gameObject.CompareTag("Coin")) {
            GameCtrl.instance.updateCoinCount1(); // Update count coin
            GameCtrl.instance.updateScore1(); // Update score
            Destroy(other.gameObject);
        }
    }
}
