using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duri : MonoBehaviour
{
     [Header("Durian Parameters")]
    [SerializeField] private float damageAmount = 0.25f;

    private bool playerInside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!playerInside)
            {
                DealDamageToPlayer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInside = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !playerInside)
        {
            DealDamageToPlayer();
        }
    }

    private void DealDamageToPlayer()
    {
        // Kurangi nyawa pemain menggunakan metode pada kelas GameCtrl atau kelas lain yang mengatur nyawa pemain
        GameCtrl.instance.DecreaseLivesDuri(damageAmount);

        playerInside = true;
    }

}
