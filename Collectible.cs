using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Collectible : MonoBehaviour
// {
//     public GameObject onCollectEffect;

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (!other.CompareTag("GameController"))
//             return;

//         if (onCollectEffect != null)
//             Instantiate(onCollectEffect, transform.position, transform.rotation);

//         Destroy(gameObject);
//         Destroy(transform.parent.gameObject);

//     }

// }

// using UnityEngine;
// using UnityEngine.InputSystem;

// public class Collectible : MonoBehaviour
// {
//     public InputActionProperty leftTrigger;
//     public InputActionProperty rightTrigger;
//     public GameObject onCollectEffect;

//     private bool isTouched = false;

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("GameController"))
//             isTouched = true;
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("GameController"))
//             isTouched = false;
//     }

//     void Update()
//     {
//         if (!isTouched) return;

//         // 왼손 또는 오른손 Trigger 눌림
//         bool left = leftTrigger.action.WasPressedThisFrame();
//         bool right = rightTrigger.action.WasPressedThisFrame();

//         if (left || right)
//         {
//             if (onCollectEffect != null)
//                 Instantiate(onCollectEffect, transform.position, transform.rotation);

//             Destroy(gameObject);

//             if (transform.parent != null)
//                 Destroy(transform.parent.gameObject);
//         }
//     }
// }

using UnityEngine;
using UnityEngine.InputSystem;

public class Collectible : MonoBehaviour
{
    public InputActionProperty leftTrigger;
    public InputActionProperty rightTrigger;
    public GameObject onCollectEffect;

    private bool isTouched = false;
    private Renderer rend;

    private void Start()
    {
        // Renderer 캐싱
        rend = GetComponent<Renderer>();

        // 처음에는 버블을 숨김
        rend.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            isTouched = true;

            // 버블을 보이게
            rend.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            isTouched = false;

            // 컨트롤러에서 떨어지면 다시 숨김
            rend.enabled = false;
        }
    }

    void Update()
    {
        if (!isTouched) return;

        // 왼손 또는 오른손 Trigger 눌림
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
}
