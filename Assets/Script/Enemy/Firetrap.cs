using System.Collections;
using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; 
    private bool active; 
    private bool playerInside;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered) {
                DecreaseLivesAndActivateFiretrap();
            }
        }
    }

    private void DecreaseLivesAndActivateFiretrap()
    {
        GameCtrl.instance.DecreaseLivesFireTrap();
        StartCoroutine(ActivateFiretrap());
        playerInside = true;
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
        if (collision.tag == "Player" && active && !playerInside)
        {
            GameCtrl.instance.DecreaseLivesFireTrap();
            playerInside = true; 
        }
    }

    
    private IEnumerator ActivateFiretrap() {
        triggered = true;
        spriteRend.color = Color.red; 

        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; 
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
    
}