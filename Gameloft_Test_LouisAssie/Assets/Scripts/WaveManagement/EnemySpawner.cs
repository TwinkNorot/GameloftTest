using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	GameManager gameManager;
	public Wave[] waves;
    public int timeBetweenWaves;

	public bool nextWaveCanSpawn = false;
	int waveIndex = 0;

	float timeBeforeNext;
	int remainingToSpawn;



	// Start is called before the first frame update
	void Start()
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.timeBeforNextWave = timeBetweenWaves;
	}

    // Update is called once per frame
    void Update()
    {
		if (gameManager.canPlay)
		{
			if (nextWaveCanSpawn)
			{
				nextWaveCanSpawn = false;
				gameManager.timeBeforNextWave = timeBetweenWaves;
				//StartCoroutine(SpawnWave());
				remainingToSpawn = waves[waveIndex].number;
				timeBeforeNext = waves[waveIndex].rate;
			}
			else if (gameManager.timeBeforNextWave > 0)
			{
				gameManager.timeBeforNextWave -= Time.deltaTime;
			}
			else if (gameManager.timeBeforNextWave <= 0)
			{
				nextWaveCanSpawn = true;
			}

			if(remainingToSpawn != 0)
            {
				if (timeBeforeNext > 0)
				{
					timeBeforeNext -= Time.deltaTime;
				}
				else
				{
					SpawnEnemy(waves[waveIndex].enemy);
					remainingToSpawn--;
					timeBeforeNext = waves[waveIndex].rate;
					if (remainingToSpawn == 0)
                    {
						waveIndex++;

						if (waveIndex == waves.Length)
							this.enabled = false;
					}
				}

			}


		}
    }


	void SpawnWave()
    {
		SpawnEnemy(waves[waveIndex].enemy);

		waveIndex++;

		if (waveIndex == waves.Length)
			this.enabled = false;
	}

	void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, transform.position, transform.rotation);
	}

	/*IEnumerator SpawnWave()
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
	}*/
}
