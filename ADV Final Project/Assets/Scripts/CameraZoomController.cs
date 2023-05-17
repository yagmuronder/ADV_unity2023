using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    public ScoringSystem scoringSystem; // Reference to the ScoringSystem script
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    public float zoomInDistance = 4f; // The distance to zoom in when the score reaches 16

    void Update()
    {
        // Check the current score from the ScoringSystem script
        int currentScore = ScoringSystem.theScore;

        // If the score reaches 16, zoom in on the character
        if (currentScore >= 19)
        {
            CinemachineFramingTransposer framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (framingTransposer != null)
            {
                framingTransposer.m_CameraDistance = zoomInDistance;
            }
        }
    }
}
