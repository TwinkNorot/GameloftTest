using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Wave[] waves;
    public int timeBetweenWaves;

	public bool nextWaveCanSpawn = true;
	int waveIndex = 0;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextWaveCanSpawn)
        {
			nextWaveCanSpawn = false;
			StartCoroutine(SpawnWave());
			StartCoroutine(Cooldown());
		}
    }


	void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, transform.position, transform.rotation);
	}

	IEnumerator SpawnWave()
	{
		Wave wave = waves[waveIndex];

		for (int i = 0; i < wave.number; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(wave.rate);
		}

		waveIndex++;

		if (waveIndex == waves.Length)
			this.enabled = false;
	}

	IEnumerator Cooldown()
    {
		yield return new WaitForSeconds(timeBetweenWaves);
		nextWaveCanSpawn = true;
	}

}
