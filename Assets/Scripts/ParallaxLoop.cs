using UnityEngine;

public class ParallaxLoop : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureUnitSizeX = sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        float cameraYposition = cameraTransform.position.y;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, 0, 0);
        lastCameraPosition = cameraTransform.position;
        float relativeDist = cameraTransform.position.x - transform.position.x;
        if (Mathf.Abs(relativeDist) >= textureUnitSizeX)
        {
            float offsetPositionX = relativeDist % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x - offsetPositionX, transform.position.y);
        }
    }
}