using UnityEngine;
using TMPro;

public class SelectionTimeTracker : MonoBehaviour
{
    public static float startTime;
    public static bool taskRunning = false;

    public TextMeshProUGUI timeText; // UI 표시

    private void Update()
    {
        if (taskRunning && timeText != null)
        {
            float elapsed = Time.time - startTime;
            timeText.text = $"Time: {elapsed:F2}s";
        }
    }

    // Task 시작
    public static void BeginTask()
    {
        startTime = Time.time;
        taskRunning = true;
    }

    // Target 선택되면 종료
    public void StopTask()
    {
        taskRunning = false;

        float selectionTime = Time.time - startTime;
        Debug.Log($"[Final Selection Time] {selectionTime:F2} seconds");
    }
}
