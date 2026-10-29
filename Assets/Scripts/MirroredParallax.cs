using UnityEngine;

[DefaultExecutionOrder(100)]
public class MirroredParallax : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [Range(0f, 1f)]
    [SerializeField] private float parallax = 0.3f;

    [Header("Ajuste de tamaño")]
    [Tooltip("Actívalo en el fondo completo. Desactívalo en la sombra.")]
    [SerializeField] private bool coverFullHeight = true;
    [SerializeField] private float heightMargin = 1.03f;

    [Header("Posición vertical")]
    [Tooltip("Actívalo en la sombra para que quede pegada al borde inferior de la pantalla.")]
    [SerializeField] private bool anchorToBottom = false;

    private SpriteRenderer[] tiles = new SpriteRenderer[3];
    private float step;
    private float z;
    private float bottomOffset;
    private float viewHeight;

    private void Awake()
    {
        if (targetCamera == null) targetCamera = Camera.main;

        SpriteRenderer original = GetComponent<SpriteRenderer>();
        z = transform.position.z;
        viewHeight = targetCamera.orthographicSize * 2f;

        float scale = 1f;
        if (coverFullHeight)
        {
            float spriteHeight = original.sprite.bounds.size.y;
            scale = Mathf.Max(1f, viewHeight / spriteHeight * heightMargin);
        }
        transform.localScale = new Vector3(scale, scale, 1f);

        step = original.sprite.bounds.size.x * scale * 0.995f;
        bottomOffset = original.sprite.bounds.min.y * scale;

        tiles[0] = original;
        for (int i = 1; i < 3; i++)
        {
            GameObject go = new GameObject(name + "_Copia" + i);
            go.transform.localScale = transform.localScale;
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = original.sprite;
            sr.sharedMaterial = original.sharedMaterial;
            sr.color = original.color;
            sr.sortingLayerID = original.sortingLayerID;
            sr.sortingOrder = original.sortingOrder;
            tiles[i] = sr;
        }
    }

    private void LateUpdate()
    {
        float camX = targetCamera.transform.position.x;
        float camY = targetCamera.transform.position.y;

        float y = anchorToBottom ? camY - viewHeight / 2f - bottomOffset : camY;

        float rel = camX * parallax;
        int k = Mathf.RoundToInt(rel / step);
        float baseX = camX * (1f - parallax);

        for (int i = 0; i < 3; i++)
        {
            int n = k - 1 + i;
            tiles[i].transform.position = new Vector3(baseX + n * step, y, z);
            tiles[i].flipX = Mathf.Abs(n) % 2 == 1;
        }
    }
}