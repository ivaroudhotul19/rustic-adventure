using System.Collections;
using UnityEngine;

public class AddLife : MonoBehaviour
{
    public float delayBeforeDestroy = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameCtrl.instance.data.lives += 1;
            GameCtrl.instance.UpdateHearts();

            SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);
            AudioCtrl.instance.AddLife(gameObject.transform.position);

            StartCoroutine(DelayBeforeDestroy());
        }
    }

    IEnumerator DelayBeforeDestroy()
    {
        yield return new WaitForSeconds(delayBeforeDestroy);
        Destroy(gameObject);
    }
}
