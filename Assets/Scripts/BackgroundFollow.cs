using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float verticalPosition = 0f;
    [SerializeField] private float depth = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(
            target.position.x,
            verticalPosition,
            depth
        );
    }
}
