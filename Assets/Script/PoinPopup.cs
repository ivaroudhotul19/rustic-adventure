using System.Collections;
using UnityEngine;
using TMPro;

public class PoinPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;

    private static GameObject FloatingText; // Ganti "GameObject" menjadi "GameObject"

    public static PoinPopup Create(Vector3 position, int poinPopup, Transform floatingTextPrefab)
    {
        Transform poinPopupTransform = Instantiate(floatingTextPrefab, position, Quaternion.identity);
        PoinPopup poinPopupComponent = poinPopupTransform.GetComponent<PoinPopup>();
        poinPopupComponent.Setup(poinPopup);

        return poinPopupComponent;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        disappearTimer = 1f;
    }

    private void Update()
    {
        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // Start disappearing
            float disappearSpeed = 3f;
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
