using UnityEngine;

public class BotSpawnerScript : MonoBehaviour
{
    //Cambiamos a un Array de GameObjects para poder poner varios
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private float spawnRate = 3.0f;
    [SerializeField] private float lifeTime = 10.0f;
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        //Elegimos un número al azar entre 0 y el total de enemigos en la lista
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);

        //Calculamos el offset
        Vector3 randomOffset = new Vector3(0, Random.Range(-0.5f, 0.4f), 0);
        Vector3 spawnPosition = transform.position + randomOffset;

        //Instanciamos el enemigo elegido al azar
        GameObject newEnemy = Instantiate(enemyPrefabs[randomEnemy], spawnPosition, Quaternion.identity);

        Destroy(newEnemy, lifeTime);
    }
}