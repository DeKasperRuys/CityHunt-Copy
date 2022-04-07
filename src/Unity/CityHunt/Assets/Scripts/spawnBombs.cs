using System.Collections;
using System.Collections.Generic;
using Mapbox.Map;
using Mapbox.Unity.Map;
using UnityEngine;
using UnityEngine.UI;
public class spawnBombs : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject bombPrefab;
	public float bombScale;
	public int bombRarity;

	GameObject[] bombs;


	AbstractMap _map;
	public static bool isDone = false;


	void Awake()
	{
		//_map = FindObjectOfType<AbstractMap>();
		//_map.OnInitialized += _map_OnInitialized;

	}

	void _map_OnInitialized()
	{

		var visualizer = _map.MapVisualizer;
		visualizer.OnMapVisualizerStateChanged += (s) =>
		{

			if (this == null)
				return;

			if (s == ModuleState.Finished)
			{
				bombs = GameObject.FindGameObjectsWithTag("bomb");


				for (int i = 0; i < bombs.Length; i++)
				{
					var rnd = Random.Range(0, bombRarity);
					if (rnd == 0)
					{
						var instance = Instantiate(bombPrefab);
						instance.transform.position = bombs[i].transform.position;
						instance.transform.localScale = new Vector3(bombScale, bombScale, bombScale);
					}
				}
				isDone = true;
			}
			else if (s == ModuleState.Working)
			{

				// Uncommment me if you want the loading screen to show again
				// when loading new tiles.
				//_content.SetActive(true);
			}

		};
	}


	void Update()
	{

	}
}
