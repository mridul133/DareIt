﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChangeScript : MonoBehaviour
{
    public Collider2D trigger1;
    public int level;
    string levelNo;
    public Sprite star1, star2, star3;
    int Stars = 0;

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Stars = 0;
        levelNo = level.ToString();
        if (trigger1.tag == "Flag")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(wait());
            trigger1.tag = "Untagged";
            tag = "On";
            PlayerPrefs.SetInt("Level" + level.ToString() + "Passed", 10);
            //Debug.Log(PlayerPrefs.GetInt("Level" + levelNo + "Score"));
            if (PlayerPrefs.GetInt("Time Needed") < 11)
            {
                PlayerPrefs.SetInt("Level" + levelNo + "Score", 3);
                Stars = 3;
            }
            else if (PlayerPrefs.GetInt("Time Needed") < 31)
            {
                //Debug.Log(PlayerPrefs.GetInt("Level" + levelNo + "Score"));
                if (PlayerPrefs.GetInt("Level" + levelNo + "Score") < 2) PlayerPrefs.SetInt("Level" + levelNo + "Score", 2);
                Stars = 2;
            }
            else
            {
                Stars = 1;
                if (PlayerPrefs.GetInt("Level" + levelNo + "Score") != 2 && PlayerPrefs.GetInt("Level" + levelNo + "Score") != 3) PlayerPrefs.SetInt("Level" + levelNo + "Score", 1);
            }
            //Debug.Log(PlayerPrefs.GetInt("Level" + levelNo + "Score"));
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        GameObject go = new GameObject("New Sprite");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        //Debug.Log(Stars);
        if (Stars == 1) renderer.sprite = star1;
        else if (Stars == 2) renderer.sprite = star2;
        else renderer.sprite = star3;

        yield return new WaitForSeconds(2);
        PlayerPrefs.SetInt("PrevTime", 0);
        if (level == 19) SceneManager.LoadScene("FinalScene");
        else SceneManager.LoadScene("Level" + level + "Win");
    }
}