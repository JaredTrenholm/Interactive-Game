using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChange : MonoBehaviour
{
    public GameObject[] TileMaps;

    private float timePassed = 0f;
    private float timeTarget = 0.175f;

    private int mapPhase = 0;

    private void Start()
    {
        for (int x = 0; x < TileMaps.Length; x++)
        {
            TileMaps[x].SetActive(false);
        }

        TileMaps[mapPhase].SetActive(true);
    }


    private void Update()
    {
        

        if(mapPhase == TileMaps.Length)
        {
            mapPhase = 0;
        }

        if(timePassed >= timeTarget)
        {
            timePassed = 0f;
            mapPhase += 1;
            if (mapPhase == TileMaps.Length)
            {
                mapPhase = 0;
            }

            for (int x = 0; x < TileMaps.Length; x++)
            {
                TileMaps[x].SetActive(false);
            }

            TileMaps[mapPhase].SetActive(true);
        } else
        {
            timePassed += Time.deltaTime;
        }
    }
}
