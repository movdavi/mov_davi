using UnityEngine;

public class Blinking : MonoBehaviour
{
    [Header("Parpadeo")]
    public Color highlightColor = Color.yellow;
    public float speed = 2f;

    private Color originalColor;
    private Renderer rend;
    private Material runtimeMaterial;

    private bool isBlinking = false;


    void Start()
    {
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            // Importante: instanciamos el material UNA sola vez
            runtimeMaterial = rend.material;
            originalColor = runtimeMaterial.color;
        }
    }
    void Update()
    {
        if (!isBlinking || runtimeMaterial == null)
            return;

        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        runtimeMaterial.color = Color.Lerp(originalColor, highlightColor, t);
    }
    /// <summary>
    /// Activa o desactiva el parpadeo.
    /// Si la pieza está deshabilitada permanentemente, no hace nada.
    /// </summary>
    public void SetHighlight(bool active)
    {
        isBlinking = active;
        if (!active && runtimeMaterial != null)
        {
            runtimeMaterial.color = originalColor;
        }
    }

    /// <summary>
    /// Apaga el parpadeo para siempre (cuando la pieza ya se ha usado en el tutorial).
    /// </summary>
}
