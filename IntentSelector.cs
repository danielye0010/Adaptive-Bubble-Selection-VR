using UnityEngine;
using TMPro;

public class IntentSelector : MonoBehaviour
{
    [Header("Selection Settings")]
    public float detectRange = 2.0f;
    public float wDistanceBase = 0.7f;
    public float wDirectionBase = 0.3f;
    public float alpha = 0.4f;
    public int maxExpectedTargets = 12;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private Vector3 lastPos;
    private GameObject bestTarget;
    private float bestScore;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        //---------------------------------------------------------
        // 1. Movement direction
        //---------------------------------------------------------
        Vector3 velocity = (transform.position - lastPos) / Time.deltaTime;
        Vector3 velDir = velocity.sqrMagnitude > 0.0001f ? velocity.normalized : Vector3.zero;
        lastPos = transform.position;

        //---------------------------------------------------------
        // 2. Find all bubble targets
        //---------------------------------------------------------
        BubbleTarget[] targets = FindObjectsOfType<BubbleTarget>();

        if (targets.Length == 0)
        {
            if (scoreText != null)
                scoreText.text = "Score: - (no targets left)";
            return;
        }

        float effectiveRange = Mathf.Max(detectRange, 0.01f);

        //---------------------------------------------------------
        // 3. Density
        //---------------------------------------------------------
        int nearbyCount = 0;
        foreach (var t in targets)
        {
            if (t == null) continue;
            float d = Vector3.Distance(transform.position, t.transform.position);
            if (d < effectiveRange) nearbyCount++;
        }
        float rho = Mathf.Clamp01((float)nearbyCount / maxExpectedTargets);

        //---------------------------------------------------------
        // 4. Adaptive weights
        //---------------------------------------------------------
        float wDistance = wDistanceBase + alpha * rho;
        float wDirection = wDirectionBase * (1f - rho);

        //---------------------------------------------------------
        // 5. Score ALL targets (no distance cutoff)
        //---------------------------------------------------------
        float maxScore = float.NegativeInfinity;
        GameObject winner = null;

        foreach (var t in targets)
        {
            if (t == null || t.runtimeMat == null) continue;

            float dist = Vector3.Distance(transform.position, t.transform.position);

            // distance score (smooth falloff)
            float D = Mathf.Clamp01(1f - (dist / effectiveRange));

            // direction score
            Vector3 toTarget = (t.transform.position - transform.position).normalized;
            float T = velDir == Vector3.zero ? 0f : Mathf.Clamp01(Vector3.Dot(velDir, toTarget));

            float score = wDistance * D + wDirection * T;

            // scoreText.text = $"Score: {score:F2}  ρ={rho:F2}";

            if (score > maxScore)
            {
                maxScore = score;
                winner = t.gameObject;
            }
        }

        bestTarget = winner;
        bestScore = maxScore;

        //---------------------------------------------------------
        // 6. Highlight winner ONLY (using runtimeMat, not shared)
        //---------------------------------------------------------
        foreach (var t in targets)
        {
            if (t == null || t.runtimeMat == null) continue;

            if (t.gameObject == winner)
                t.runtimeMat.SetColor("_BaseColor", Color.green);   // winner
            else
                t.runtimeMat.SetColor("_BaseColor", Color.gray);    // normal
        }

        //---------------------------------------------------------
        // 7. Update score UI
        //---------------------------------------------------------
        if (scoreText != null)
        {
            if (bestTarget != null)
                scoreText.text = $"Score: {bestScore:F2}  ρ={rho:F2}";
            else
                scoreText.text = $"Score: -  ρ={rho:F2}";
        }
    }
}
