using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    TileMap currentMap = null;
    Camera cam = null;

    float camDistance = 1.0f;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        if (cam == null) {
            Debug.LogError("Couldn't get main camera");
            return;
        }
        Debug.Log("Loading tilemap");
        TileMap map = Resources.Load<TileMap>("Prefabs/TileMap");
        Debug.Log(map);
        currentMap = Instantiate(map) as TileMap;

        Vector3 mapPos = currentMap.transform.position;

        Vector3 camTarget = new Vector3(mapPos.x+0.5f, mapPos.y+camDistance, mapPos.z-0.5f);
        cam.transform.position = camTarget;
        cam.transform.Rotate(90, 0, 0);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
