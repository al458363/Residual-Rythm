using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // Usamos la variable 'speed' en lugar de un número fijo
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -12)
        {
            Destroy(gameObject);
        }
    }
}