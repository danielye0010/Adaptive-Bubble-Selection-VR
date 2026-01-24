using UnityEngine;

public class BubbleReveal : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;  // 처음에 안 보이게
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            rend.enabled = true;  // 컨트롤러가 가까워지면 보이기
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            rend.enabled = false; // 멀어지면 다시 숨기기
        }
    }
}
