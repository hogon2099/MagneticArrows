using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	private LevelManager lvl;
    void Start()
    {
		lvl =  GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
		lvl.LoadNext();
	}
}
