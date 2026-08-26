using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomManager : MonoBehaviour
{
    [System.Serializable]
    public class ZoomTarget
    {
        [Header("UI Button")]
        public Button button;

        [Header("Camera Target")]
        public Transform target;
    }

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Zoom Settings")]
    public float zoomDuration = 0.5f;

    [Header("Keep Distance From Target")]
    public float targetDistance = 5f;

    [Header("Targets")]
    public List<ZoomTarget> targets = new List<ZoomTarget>();

    private Coroutine zoomCoroutine;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        foreach (ZoomTarget zoomTarget in targets)
        {
            if (zoomTarget.button != null)
            {
                ZoomTarget currentTarget = zoomTarget;

                currentTarget.button.onClick.AddListener(() =>
                {
                    ZoomToTarget(currentTarget);
                });
            }
        }
    }

    public void ZoomToTarget(ZoomTarget zoomTarget)
    {
        if (zoomTarget.target == null)
        {
            Debug.LogWarning("Zoom target is missing!");
            return;
        }

        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(
            ZoomCoroutine(zoomTarget.target)
        );
    }

    private IEnumerator ZoomCoroutine(Transform target)
    {
        Vector3 startPosition = mainCamera.transform.position;

        // Keep camera rotation exactly as it is
        Quaternion currentRotation = mainCamera.transform.rotation;

        // Camera's current forward direction
        Vector3 forward = mainCamera.transform.forward;

        // Find the point directly in front of the target
        // while keeping the camera facing direction unchanged.
        Vector3 targetPosition =
            target.position - forward * targetDistance;

        float time = 0f;

        while (time < zoomDuration)
        {
            time += Time.deltaTime;

            float t = time / zoomDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            // Move only
            mainCamera.transform.position =
                Vector3.Lerp(startPosition, targetPosition, t);

            // Never rotate camera
            mainCamera.transform.rotation = currentRotation;

            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = currentRotation;

        zoomCoroutine = null;
    }
}