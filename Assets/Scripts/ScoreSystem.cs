using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
	[SerializeField] TMP_Text _scoreText;
	private int _score;

	void Start()
    {
	    Enemy.OnEnemyDied += OnEnemyDie;
    }

    private void OnEnemyDie(int value)
    {
	    _score += value;
        _scoreText.SetText(_score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
