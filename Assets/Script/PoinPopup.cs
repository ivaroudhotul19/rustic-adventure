using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoinPopup : MonoBehaviour
{
    // private TextMeshPro textMesh;
    private float disappearTimer;
    private color textColor;

    // public static PoinPopup Create(Vector3 position, int poinPopup){
    //   Transform poinPopupTransform = Instantiate(FloatingTextPrefab, Vector3.zero, Quaternion.identity);
    //   PoinPopup poinPopup = poinPopupTransform.GetComponent<PoinPopup>();
    //   poinPopup.Setup(poinPopup);

    //   return poinPopup;
    // }

    // private void Awake()
    // {
    //   textMesh = transform.GetComponent<TextMeshPro>();
    // }

    // public void Setup(int damageAmount)
    // {
    //   textMesh.SetText(damageAmount.ToString());
    //  textColor = textMesh.color;
    //  disappearTimer =1f;
    // }

    // private void Update(){
    //   float moveYSpeed = 20f;
    //   transform.position +=new Vector3(0, meveYSpeed) * Time.deltaTime;

    //  disappearTimer -= Time.deltaTime;
    //  if(disappearTimer <0 ) {
    //       float disappearSpeed = 3f;
    //       textColor.a -= disappearSpeed * Time.deltaTime;
    //       textMesh.color = textColor;
    //  }
    // }
}
