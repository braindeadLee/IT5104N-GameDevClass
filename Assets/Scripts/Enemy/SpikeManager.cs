using System.Collections;
using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] GameObject spikePrefab;
    private int numToSpawn = 0;
    private float longCooldown = 3f;
    private float speed = 3f;
    private bool canSpawn = true;
    private float shortCoolDown = 0.2f;

    private void Start()
    {
        StartCoroutine(nextWave());
    }
    private void Update()
    {
        if (canSpawn && numToSpawn > 0)
        {
            numToSpawn = spawnNextSpike(numToSpawn);
        }
    }

    private IEnumerator nextWave()
    {
        numToSpawn = Random.Range(1, 6);
        longCooldown = Random.Range(4f, 6f);
        speed = Random.Range(3f, 6f);

        yield return new WaitForSeconds(longCooldown);
        StartCoroutine(nextWave());
    }

    private int spawnNextSpike(int queue)
    {
        GameObject spike = Instantiate(spikePrefab, startPoint.position, Quaternion.identity);
        StartCoroutine(spike.GetComponent<Spike>().moveSpike(speed, startPoint.position, endPoint.position));

        canSpawn = false;
        Invoke(nameof(enableSpawn), shortCoolDown);

        return queue - 1;
    }
    private void enableSpawn() => canSpawn = true;
}