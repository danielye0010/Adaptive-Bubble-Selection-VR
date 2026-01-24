// using UnityEngine;
// using TMPro;

// public class TimerUI : MonoBehaviour
// {
//     public TextMeshProUGUI timerText;   
//     public bool timerRunning = false;  

//     private float startTime;
//     private bool stoppedOnce = false;  // 중복 종료 방지

//     void Update()
//     {
//         // 타깃이 0개면 타이머 종료
//         if (!stoppedOnce)
//         {
//             BubbleTarget[] targets = FindObjectsOfType<BubbleTarget>();
//             if (targets.Length == 0)   // 타겟 모두 선택됨
//             {
//                 StopTimer();
//                 stoppedOnce = true;   // StopTimer 여러 번 실행 방지
//                 return;
//             }
//         }

//         // 타이머 업데이트
//         if (timerRunning)
//         {
//             float elapsed = Time.time - startTime;
//             timerText.text = elapsed.ToString("F2") + " s";
//         }
//     }

//     // 타이머 시작
//     public void StartTimer()
//     {
//         startTime = Time.time;
//         timerRunning = true;
//         stoppedOnce = false;  // 새 라운드 시작 시 초기화
//     }

//     // 타이머 정지
//     public void StopTimer()
//     {
//         timerRunning = false;
//         Debug.Log("⏹ Timer Stopped – All targets cleared");
//     }

//     // 타이머 리셋
//     public void ResetTimer()
//     {
//         timerRunning = false;
//         timerText.text = "0.00 s";
//         stoppedOnce = false;
//     }
// }

using UnityEngine;
using TMPro;
using System.IO;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TMP_InputField userIdInput;   // 🔥 입력받는 필드

    public bool timerRunning = false;

    private float startTime;
    private bool stoppedOnce = false;

    void Update()
    {
        if (!stoppedOnce)
        {
            BubbleTarget[] targets = FindObjectsOfType<BubbleTarget>();
            if (targets.Length == 0)
            {
                StopTimer();
                stoppedOnce = true;
                return;
            }
        }

        if (timerRunning)
        {
            float elapsed = Time.time - startTime;
            timerText.text = elapsed.ToString("F2") + " s";
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerRunning = true;
        stoppedOnce = false;
    }

    public void StopTimer()
    {
        timerRunning = false;
        float finalTime = Time.time - startTime;

        string userID = userIdInput != null ? userIdInput.text : "UnknownUser";
        StoreResultToFile(userID, finalTime);
    }

    public void ResetTimer()
    {
        timerRunning = false;
        timerText.text = "0.00 s";
        stoppedOnce = false;
    }

    private void StoreResultToFile(string userID, float finalTime)
    {
        string folder = Application.persistentDataPath;
        string filePath = Path.Combine(folder, "experiment_results.txt");

        string line = $"{userID}, {finalTime:F2}s";
        File.AppendAllText(filePath, line + "\n");

        Debug.Log($"📄 Saved to: {filePath}\n{line}");
    }
}
