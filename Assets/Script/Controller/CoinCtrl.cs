using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCtrl : MonoBehaviour
{
    public enum CoinFX
    {
        Vanish,
        Fly
    }

    public CoinFX coinFX;
    public float speed;
    public bool startFlying;
    GameObject coinMeter;

    public Transform FloatingTextPrefab;

    void Start()
    {
        startFlying = false;
        if(coinFX == CoinFX.Fly) {
            coinMeter = GameObject.Find("img_coin_count");
        }
    }

    void Update(){
        if(startFlying) {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, speed);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (coinFX == CoinFX.Vanish) // Menggunakan coinFX bukan CoinFX
            {
                int value = GameCtrl.instance.getItemValue(GameCtrl.Item.Coin);
                PoinPopup poinPopup = PoinPopup.Create(transform.position, value, FloatingTextPrefab);
                if (poinPopup != null)
                {
                    poinPopup.SetPosition(transform.position + new Vector3(0f, 1f, 0f));
                }
                Destroy(gameObject);
            } else if (coinFX == CoinFX.Fly) {
                gameObject.layer = 0;
                startFlying = true;
            }
        }
    }
}
