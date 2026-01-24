// using UnityEngine;


// public class TargetVisual : MonoBehaviour
// {
//     private Material mat;
//     private Color baseColor = Color.gray;
//     private Coroutine flashRoutine;


//     void Awake()
//     {
//         var renderer = GetComponent<Renderer>();
//         mat = new Material(renderer.material);
//         renderer.material = mat;
//     }


//     public void SetHighlight(float value)
//     {
//         if (mat == null) return;
//         mat.color = Color.Lerp(baseColor, Color.green, value);
//     }


//     public void PlayFlash()
//     {
//         if (flashRoutine != null)
//             StopCoroutine(flashRoutine);


//         flashRoutine = StartCoroutine(FlashRoutine());
//     }


//     private System.Collections.IEnumerator FlashRoutine()
//     {
//         mat.color = Color.white;
//         yield return new WaitForSeconds(0.2f);
//         mat.color = baseColor;
//     }
// }

using UnityEngine;

public class TargetVisual : MonoBehaviour
{
    private Material mat;
    private Color baseColor = Color.gray;
    private Coroutine flashRoutine;

    void Awake()
    {
        Debug.Log($"[TargetVisual] Awake() on {gameObject.name}");

        var renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError($"[TargetVisual] ERROR: No Renderer found on {gameObject.name}!");
            return;
        }

        if (renderer.material == null)
        {
            Debug.LogError($"[TargetVisual] ERROR: Renderer has NO material on {gameObject.name}!");
            return;
        }

        Debug.Log($"[TargetVisual] Renderer and Material FOUND on {gameObject.name}");

        // Clone the material so each target gets its own
        mat = new Material(renderer.material);
        renderer.material = mat;

        Debug.Log($"[TargetVisual] Material cloned for {gameObject.name}. Mat color = {mat.color}");
    }

    public void SetHighlight(float value)
    {
        if (mat == null)
        {
            Debug.LogWarning($"[TargetVisual] WARNING: mat is NULL on {gameObject.name}, cannot highlight.");
            return;
        }

        value = Mathf.Clamp01(value);

        Debug.Log($"[TargetVisual] SetHighlight({value}) on {gameObject.name}");

        mat.color = Color.Lerp(baseColor, Color.green, value);
    }

    public void PlayFlash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        Debug.Log($"[TargetVisual] PlayFlash triggered on {gameObject.name}");

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private System.Collections.IEnumerator FlashRoutine()
    {
        if (mat == null)
        {
            Debug.LogError($"[TargetVisual] FlashRoutine ERROR: mat is NULL on {gameObject.name}");
            yield break;
        }

        mat.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        mat.color = baseColor;
    }
}
