using UnityEngine;
using UnityEngine.InputSystem;

public class AdaptiveCollectible : MonoBehaviour
{
    public InputActionProperty leftTrigger;
    public InputActionProperty rightTrigger;
    public GameObject onCollectEffect;

    private bool isTouched = false;
    private Renderer rend;

    private void Start()
    {
        // Renderer를 자식에서 찾기
        rend = GetComponentInChildren<Renderer>();
        rend.enabled = false;

        // Mesh 크기를 기준으로 최소 2배 크기 설정
        float minObjectScale = rend.transform.localScale.x;   // 또는 직접 숫자 지정 가능
        AdjustScaleByDensity(minObjectScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble") || other.CompareTag("GameController"))
        {
            isTouched = true;
            rend.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bubble") || other.CompareTag("GameController"))
        {
            isTouched = false;
            rend.enabled = false;
        }
    }

    void Update()
    {
        if (!isTouched) return;

        bool left = leftTrigger.action.WasPressedThisFrame();
        bool right = rightTrigger.action.WasPressedThisFrame();

        if (left || right)
        {
            if (onCollectEffect != null)
                Instantiate(onCollectEffect, transform.position, transform.rotation);

            Destroy(gameObject);

            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
    }
    void AdjustScaleByDensity(float parentScale)
    {
        float radius = 1.5f; // 밀도 측정 범위 넓힘
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        // Bubble 밀도 계산
        int bubbleCount = 0;
        foreach (var h in hits)
        {
            if (h != null && h.gameObject != gameObject && h.CompareTag("Bubble"))
                bubbleCount++;
        }

        // 밀도 0 → t = 0
        // 밀도 15 이상 → t = 1
        float t = Mathf.InverseLerp(0, 15, bubbleCount);

        // 너가 원하는 값들:
        float maxScale = 0.8f;               // 밀도 낮으면 매우 크게
        float minScale = parentScale * 2f;   // 밀도 높으면 여기까지 줄어듦

        // finalScale은 big(0.8) → small(parent*2) 로 보간
        float finalScale = Mathf.Lerp(maxScale, minScale, t);

        // 실제 버블 mesh에 적용
        Transform visual = transform; // child에 스크립트가 있으므로 transform = bubble mesh
        visual.localScale = Vector3.one * finalScale;

        // Collider도 맞춰줌
        SphereCollider col = GetComponent<SphereCollider>();
        if (col != null)
            col.radius = finalScale;
    }


}
