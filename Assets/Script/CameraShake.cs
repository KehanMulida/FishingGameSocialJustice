using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
 [Header("Camera Shake Settings")]
    public Transform cameraTransform; // Reference to the main camera's transform
    public float shakeDuration = 0.1f; // Duration of the camera shake effect
    public float shakeIntensity = 0.3f; // Intensity of the camera shake effect

    private Vector3 originalPosition; // Original position of the camera
    private bool isShaking = false; // Flag to check if the camera is currently shaking

    void Start()
    {
        // Initialize the original position of the camera
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned!");
            return;
        }
        originalPosition = cameraTransform.localPosition;
    }

    /// <summary>
    /// Public method to start the camera shake effect
    /// </summary>
    public void Shake()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }

    /// <summary>
    /// Coroutine to handle the camera shake effect
    /// </summary>
    private System.Collections.IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Generate random offset within the intensity range
            float shakeAmountX = Random.Range(-1f, 1f) * shakeIntensity;
            float shakeAmountY = Random.Range(-1f, 1f) * shakeIntensity;

            // Apply the offset to the camera's position
            cameraTransform.localPosition = originalPosition + new Vector3(shakeAmountX, shakeAmountY, 0);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera's position after shaking
        cameraTransform.localPosition = originalPosition;
        isShaking = false;
    }
}
