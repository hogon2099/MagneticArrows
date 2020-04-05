using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private string currLevel = "CurrentLevel"; // ключ PlayerPrefs для сохранения номера загруженной сцены
	public bool isAtMainMenu = false;
	private int currLevelNumber;

	SpriteRenderer exitMenu;
	private bool isAtExitMenu = false;
	private void Start()
	{
		isAtExitMenu = false;
		if (isAtMainMenu)
		{
			UpdatePlayerPrefs(0);
		}
		else
		{
			exitMenu = GameObject.FindGameObjectWithTag("Menu").GetComponent<SpriteRenderer>();
			exitMenu.enabled = false;
		}

	}
	private void Update()
	{
		if (isAtMainMenu)
		{
			if (Input.GetKeyDown(KeyCode.Space))
				LoadNext();
			if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();
		}
		else
		{
			// не в главном меню
			if (isAtExitMenu)
			{
				if (Input.GetKeyDown(KeyCode.Escape))
					Application.Quit();
				if (Input.GetKeyDown(KeyCode.Space))
				{
					isAtExitMenu = false;
					exitMenu.enabled = false;
					Time.timeScale = 1;
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					isAtExitMenu = true;
					exitMenu.enabled = true;
					Time.timeScale = 0;
				}
			}
		}
	}

	public void LoadNext()
	{
		GameObject obj = GameObject.FindGameObjectWithTag("Music");
		DontDestroyOnLoad(obj);

		int curreLevelNumber = PlayerPrefs.GetInt(currLevel);
		int nextLevelNumber = curreLevelNumber + 1;
		if (SceneManager.sceneCountInBuildSettings < nextLevelNumber + 1) nextLevelNumber = 0;

		UpdatePlayerPrefs(nextLevelNumber);
		SceneManager.LoadScene(nextLevelNumber);
		Time.timeScale = 1;

	}
	public void Reload()
	{
		int levelNumber = PlayerPrefs.GetInt(currLevel);
		SceneManager.LoadScene(levelNumber);
		Time.timeScale = 1;
	}
	public void LoadMainMenu()
	{
		SceneManager.LoadScene(0);
		Time.timeScale = 1;
	}

	private void UpdatePlayerPrefs(int levelNumber)
	{
		if (PlayerPrefs.HasKey(currLevel))
		{
			PlayerPrefs.DeleteKey(currLevel);
			PlayerPrefs.SetInt(currLevel, levelNumber);
		}
		else
			PlayerPrefs.SetInt(currLevel, levelNumber);
	}
}
