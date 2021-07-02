using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	private float _nextTimeSpawn;
    [SerializeField]
	private float _spawnDelay = 12f;
	[SerializeField]
	private Enemy _enemyPrefab;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if(ReadyToSpawn())
		    StartCoroutine(Spawn());
    }

	IEnumerator Spawn()
	{
		_nextTimeSpawn = Time.time + _spawnDelay;
		var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
		//TODO: Добавление анимации спавна
		//GetComponent<Animator>().SetBool("Open", true);
		yield return new WaitForSeconds(1f);
		enemy.StartWalking();
		yield return new WaitForSeconds(3f);
		//GetComponent<Animator>().SetBool("Open", false);
	}

    private bool ReadyToSpawn()
    {
	    return Time.time >= _nextTimeSpawn;
    }
}
