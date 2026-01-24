using UnityEngine;


public class BubbleScaler : MonoBehaviour
{
    private Vector3 baseScale;      


    public float minScale = 0.6f;     // bubble size when very close
    public float maxScale = 1.6f;     // bubble size when far
    public float detectRange = 0.6f;  // must match IntentSelector
    public float scaleSmooth = 8f;


    private float targetScale = 1f;
    private bool isHighlighted = false;


    public Transform hand;   // <-- MUST BE SET from IntentSelector


    void Start()
    {
        baseScale = transform.localScale;
    }


    // Called by IntentSelector
    public void SetHighlighted(bool highlight)
    {
        isHighlighted = highlight;
    }


    void Update()
    {
        if (hand == null) return;  // wait for assignment


        // Distance between hand and target
        float dist = Vector3.Distance(hand.position, transform.position);


        // Distance → bubble size mapping
        float distRatio = Mathf.Clamp01(dist / detectRange);
        float dynamicScale = Mathf.Lerp(minScale, maxScale, distRatio);


        // Highlight boost
        float highlightScale = isHighlighted ? 1.15f : 1f;



        targetScale = dynamicScale * highlightScale;


        // Apply smooth scale
        Vector3 goal = baseScale * targetScale;
        transform.localScale = Vector3.Lerp(transform.localScale, goal, Time.deltaTime * scaleSmooth);
    }
}
