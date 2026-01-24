using UnityEngine;

public class BubbleTarget : MonoBehaviour
{
    public Transform bubbleVisual;     // child mesh
    public Renderer bubbleRenderer;    // child renderer

    [HideInInspector] public Material runtimeMat;

    void Awake()
    {
        if (bubbleRenderer == null)
        {
            Debug.LogError($"BubbleRenderer missing on {gameObject.name}");
            return;
        }

        // Clone material so modifying color won't affect others
        runtimeMat = new Material(bubbleRenderer.material);
        bubbleRenderer.material = runtimeMat;
    }
}
