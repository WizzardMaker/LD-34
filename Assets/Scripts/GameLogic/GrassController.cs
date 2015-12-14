using UnityEngine;
using System.Collections;

public class GrassController : MonoBehaviour {

	int units = 0;
	public Vector2 range;
	public GameObject grassPrefab;
	public Terrain terrain;
	public TerrainData terrainData;
	public static GrassController active;
	public int[,] oldMap;

	// Use this for initialization
	void Start() {
		/*for (int x = 0; x < range.x; x++) {
			for (int y = 0; y < range.y; y++) {
				Instantiate(grassPrefab).transform.position = new Vector3(x,.45f,y);
			}
		}*/
		active = this;

		terrainData = terrain.terrainData;
		oldMap = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, 0);
	}



	public void OnDestroy() {
		terrainData.SetDetailLayer(0, 0, 0, oldMap);
	}

	public void RemoveGrassInBounds(Bounds bounds, string tag) {
		var map = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, 0);

		//Debug.Log(bounds.min.z + "/" + bounds.min.x);
		for (int x = (int)bounds.min.x * 1; x < bounds.max.x * 1; x++) {
			for (int y = (int)bounds.min.z * 1; y < bounds.max.z * 1; y++) {
				try {
					if (map[(int)y, (int)x] != 0) {
						map[(int)y, (int)x] = 0;
						if (tag == "Player") {
							UiController.active.yProgress -= 0.001f;
						} else {
							UiController.active.oProgress -= 0.00075f;
						}
					}


				} catch (System.IndexOutOfRangeException) {
					//Debug.LogError("Grass is out of Range, someone, I dont say who, tried to access:" + x + "/" + y);
				}

			}

		}

		terrainData.SetDetailLayer(0, 0, 0, map);
	}

	// Update is called once per frame
	void Update() {

		/*
		actTimer -= Time.deltaTime;

		if (actTimer < 0) {
			actTimer = growTimer;

			if (units >= maxUnits)
				return;

			it += .1f;

			units++;

			GameObject tempObj = Instantiate(grassPrefab);
			tempObj.transform.SetParent(transform);
			tempObj.transform.localPosition = new Vector3(Random.Range(-0.3f, .3f) * it, .45f, Random.Range(-0.3f, .3f) * it);
		}*/
	}
}
