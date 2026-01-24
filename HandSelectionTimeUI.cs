using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class HandSelectionTimeUI : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor handInteractor;   // 손
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable targetBubble;     // 타겟
    public TextMeshProUGUI timeText;

    private float startTime;

    void Start()
    {
        startTime = Time.time;

        handInteractor.selectEntered.AddListener(OnSelect);
        timeText.text = "Waiting...";
    }

    void OnSelect(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform == targetBubble.transform)
        {
            float selectTime = Time.time - startTime;
            timeText.text = $"Hand Selection Time: {selectTime:F3} sec";
        }
    }
}
