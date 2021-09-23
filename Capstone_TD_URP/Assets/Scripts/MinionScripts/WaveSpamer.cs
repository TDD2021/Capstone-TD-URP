﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpamer : MonoBehaviour
{
	public Transform spawnPoint;
	public Transform enemy;

	public float timeBetweenWaves = 60f;// The delay spamwing time between each spawm, need to adjust in Unity 
	private float countdown = 2f;// The count down between the first wave 


	private int waveNumber = 0;// The number of wave have been generate 

	//Ui text for display
	public Text  waveCountdowntxt;
	public Text currentWave;

    private void Update()
    {
		

		if (countdown <= 0f)
		{
			StartCoroutine( SpawnWave() );
			countdown = timeBetweenWaves;
			
		}

		countdown -= Time.deltaTime;

		//Apply txt Display
		waveCountdowntxt.text = Mathf.Round(countdown).ToString();
		currentWave.text = waveNumber.ToString();
	}

	IEnumerator SpawnWave() // Inumerator allow minions not collap when spawming 
	{
		waveNumber++;
		//now wave number be come the amount of enemies. eg. if wave number = 2 => the wave will spawn 2 enemeies 
		for (int i = 0; i < waveNumber; i++)
		 {
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f); // wait 0.5s before spawnning another enemie in the same wave. 
        }
		
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





















		/*
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
		}*/
	}