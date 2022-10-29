using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MobSpawnerController : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 10;

    Vector3 _spawnPos;

    [SerializeField]
    float _spawnRadius = 3.0f;

    [SerializeField]
    float _spawnTime = 5.0f;

    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }


    void Update()
    {
        while(_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine(ReserveSpawn());
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSecondsRealtime(Random.Range(0,_spawnTime));
        GameObject obj = Resources.Load("Prefabs/Mobs/Mob_FlyingEye") as GameObject;


        Vector3 playerPosition = playerTransform.position;

        float a = playerPosition.x;
        float b = playerPosition.y;

        float x = Random.Range(-_spawnRadius + a, _spawnRadius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(_spawnRadius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;

        Vector3 randPos = new Vector3(x, y, 0);

        Instantiate(obj, randPos, Quaternion.identity);
        _monsterCount++; //Test
        _reserveCount--;
    }
}
