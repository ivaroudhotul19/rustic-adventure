using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCtrl : MonoBehaviour
{
    public Transform dustParticlePos;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground")){
            SFXCtrl.instance.ShowPalyerLanding(dustParticlePos.position);
        }
    }
}
