using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCtrl : MonoBehaviour
{
    public GameObject player;
    public Transform dustParticlePos;
    PlayerCtrl playerCtrl;

    private void Start()
	{
        playerCtrl = gameObject.transform.parent.gameObject.GetComponent<PlayerCtrl>();
	}

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground")){
            SFXCtrl.instance.ShowPalyerLanding(dustParticlePos.position);
            playerCtrl.isJumping = false;
        }
        if (other.gameObject.CompareTag("Platform"))
        {
            SFXCtrl.instance.ShowPalyerLanding(dustParticlePos.position);

            playerCtrl.isJumping = false;

            player.transform.parent = other.gameObject.transform;
        }
        if (other.gameObject.CompareTag("Coin")) {
            GameCtrl.instance.updateCoinCount1(); // Update count coin
            GameCtrl.instance.updateScore1(); // Update score
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
        }
	}
}
