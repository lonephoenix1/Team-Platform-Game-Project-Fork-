using UnityEngine;

public class DarkenByHeight : MonoBehaviour
{
    public Transform character; // Referencja do postaci
    public Light mainLight; // Referencja do g³ównego Ÿród³a œwiat³a

    public float minHeight = 0f; // Minimalna wysokoœæ postaci
    public float maxHeight = 10f; // Maksymalna wysokoœæ postaci
    public float minIntensity = 1f; // Minimalna intensywnoœæ œwiat³a
    public float maxIntensity = 5f; // Maksymalna intensywnoœæ œwiat³a

    void Update()
    {
        // Pobierz wysokoœæ postaci
        float height = character.position.y;

        // Zastosuj odpowiedni¹ intensywnoœæ œwiat³a w zale¿noœci od wysokoœci postaci
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.InverseLerp(minHeight, maxHeight, height));
        mainLight.intensity = intensity;
    }
}
