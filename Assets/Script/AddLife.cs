using System.Collections;
using UnityEngine;

public class AddLife : MonoBehaviour
{
    public float delayBeforeDestroy = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Menerapkan logika penambahan nyawa
            GameCtrl.instance.data.lives += 1;
            GameCtrl.instance.UpdateHearts();

            // Menjalankan animasi atau efek suara jika diperlukan
            // Misalnya, Anda dapat menampilkan efek kilatan dan suara
            // SFXCtrl.instance.ShowLifeUpEffect(transform.position);

            // Menunggu sebentar sebelum menghapus objek
            StartCoroutine(DelayBeforeDestroy());
        }
    }

    IEnumerator DelayBeforeDestroy()
    {
        yield return new WaitForSeconds(delayBeforeDestroy);

        // Menghapus objek setelah menunggu beberapa saat
        Destroy(gameObject);
    }
}
