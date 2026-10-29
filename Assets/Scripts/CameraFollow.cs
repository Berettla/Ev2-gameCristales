using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, -10f);

    [Header("Fondo (parallax)")]
    [SerializeField] private Transform backgroundLayer00;
    [SerializeField] private Transform backgroundLayer01;
    [SerializeField] private float parallaxAmount = 0.03f;

    private Vector3 startLocal00;
    private Vector3 startLocal01;

    private void Awake()
    {
        if (backgroundLayer00 != null) startLocal00 = backgroundLayer00.localPosition;
        if (backgroundLayer01 != null) startLocal01 = backgroundLayer01.localPosition;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.position + offset;

        Vector3 shift = new Vector3(-transform.position.x * parallaxAmount, 0f, 0f);
        if (backgroundLayer00 != null) backgroundLayer00.localPosition = startLocal00 + shift;
        if (backgroundLayer01 != null) backgroundLayer01.localPosition = startLocal01 + shift;
    }
}