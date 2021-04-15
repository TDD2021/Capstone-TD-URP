﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpamer : MonoBehaviour
{
	public Transform spawnPoint;
	public Transform enemy;

	public float timeBetweenWaves = 2f;// The delay spamwing time between each spawm 
	private float countdown = 1f;// The count down between the first wave 


	private int waveNumber = 1;// The number of wave have been generate 

	void Update()
	{

		if (countdown <= 0f)// when the countdown reach 0 second, minion will start spawmming 
		{
			//StartCoroutine(SpawnWave()); // spawm the minion wave 
			SpawnWave();
			countdown = timeBetweenWaves;// From this point each wave will sapwn after timeBetweenWaves second 
			return;
		}

		countdown -= Time.deltaTime;//Decrease the countdown time by one every frame. 
		//Debug.Log(countdown);
		//countdown -= 0.1f;

		//countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);


	}

	//IEnumerator SpawnWave()
	public void SpawnWave()
	{


		//for (int i = 0; i < waveNumber; i++)
		//{
		SpawnEnemy();
		//yield return new WaitForSeconds(1f);
		//yield return null;
		//}

		waveNumber++;
	}

	void SpawnEnemy() // spawm minion of each wave 
	{
		if (enemy != null)
		{
			Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		}
		else
		{

			Debug.Log("Enemy no longer respawn");
		}
	}
}