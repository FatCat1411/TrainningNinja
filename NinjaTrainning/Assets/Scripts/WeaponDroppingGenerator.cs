using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDroppingGenerator : MonoBehaviour
{
    private float minX, maxX, y, z;
    private bool canSpawn;

    public GameObject spawnPrefab;
    public float spawnRate = 1.0f;
    public float fallingSlowness = 4.0f;
    public float increaseSpawnRate = 10.0f;

	// Use this for initialization
	void Start ()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        minX = boxCollider.bounds.min.x;
        maxX = boxCollider.bounds.max.x;
        y = boxCollider.bounds.center.y;
        z = boxCollider.bounds.center.z;

	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    private IEnumerator SpawnWeapon()
    {
        if(null == spawnPrefab)
        {
            yield return null;
        }
        while (true == canSpawn)
        {
            yield return new WaitForSeconds(spawnRate);

            float spawnX = Random.Range(minX, maxX);

            Vector3 spawnPosition = new Vector3(spawnX, y, z);
            Quaternion spawnQuaterion = Quaternion.Euler(-90, 0, Random.Range(-180, 180));

            GameObject spawnObj = Instantiate<GameObject>(spawnPrefab, spawnPosition, spawnQuaterion);
            spawnObj.GetComponent<FallingWeapon>().Slowness = fallingSlowness;
        }

    }

    private IEnumerator UpdateDifficultLevel()
    {
        while(true == canSpawn)
        {
            yield return new WaitForSeconds(increaseSpawnRate);
            
            spawnRate = Mathf.Clamp(spawnRate - 0.01f, 0.02f, 100);

            fallingSlowness = Mathf.Clamp(fallingSlowness - 0.01f, -10, 100);
        }
    }

    public void SetSpawnSetting(bool isEnable)
    {
        canSpawn = isEnable;
        if(true == isEnable)
        {
            StartCoroutine(SpawnWeapon());
            StartCoroutine(UpdateDifficultLevel());
        }
        else
        {
            StopCoroutine(SpawnWeapon());
            StopCoroutine(UpdateDifficultLevel());
        }
    }
}
