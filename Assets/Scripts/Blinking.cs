using UnityEngine;

public class Blinking : MonoBehaviour
{
    [Header("Parpadeo")]
    public Color highlightColor = Color.yellow; // color de la iluminación
    public float speed = 2f;                    // velocidad del parpadeo
    private Color originalColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color; // guardar color original
        }
    }

    void Update()
    {
        if (rend != null)
        {
            // Parpadeo sinusoidal entre color original y highlightColor
            float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // valor entre 0 y 1
            rend.material.color = Color.Lerp(originalColor, highlightColor, t);
        }
    }
    public void SetHighlight(bool active)
    {
        if (active)
            rend.material.color = highlightColor;
        else
            rend.material.color = originalColor;
    }
}
