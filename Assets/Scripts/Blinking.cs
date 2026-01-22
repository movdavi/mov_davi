using UnityEngine;

public class Blinking : MonoBehaviour
{
    [Header("Parpadeo")]
    public Color highlightColor = Color.yellow;
    public float speed = 2f;

    private Color originalColor;
    private Renderer rend;
    private bool isBlinking = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    void Update()
    {
        if (!isBlinking || rend == null) return;

        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        rend.material.color = Color.Lerp(originalColor, highlightColor, t);
    }
    public void SetHighlight(bool active)
    {
        if (active)
            isBlinking = true;
        else
            isBlinking = false;
            if (rend != null)
                rend.material.color = originalColor;
    }
}
