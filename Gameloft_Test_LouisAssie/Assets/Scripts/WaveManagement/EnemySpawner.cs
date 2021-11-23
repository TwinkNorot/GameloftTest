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
	int remainingToSpawn;
	float timeBeforeNext;



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
			if (nextWaveCanSpawn && waveIndex != waves.Length)
			{
				nextWaveCanSpawn = false;
				gameManager.timeBeforNextWave = timeBetweenWaves;
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
					Instantiate(waves[waveIndex].enemy, transform.position, transform.rotation);
					remainingToSpawn--;
					gameManager.remainingEnemies++;
					timeBeforeNext = waves[waveIndex].rate;
					if (remainingToSpawn == 0)
                    {
						waveIndex++;

						if (waveIndex == waves.Length)
						{
							gameManager.allWavesSpawend = true;
							this.enabled = false;
						}
					}
				}
			}
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Ally"))
		{
			gameManager.playerLifePoints -= other.GetComponentInParent<Unit>().damageToPlayer;
			other.transform.parent.gameObject.SetActive(false);
		}
	}
}
