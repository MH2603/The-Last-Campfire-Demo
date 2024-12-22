

using UnityEngine;

public class FPSHandler : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60; // Desired FPS
    
    private void Start()
    {
        // Set the target frame rate
        Application.targetFrameRate = targetFrameRate;

        // Optional: Ensure VSync is disabled to enforce the targetFrameRate
        QualitySettings.vSyncCount = 0;
    }
}