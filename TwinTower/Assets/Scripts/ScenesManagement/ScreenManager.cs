﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwinTower
{
    /// <summary>
    /// ScreenManager 클래스입니다.  게임 씬을 관리하는 클래스입니다.
    /// </summary>
    public class ScreenManager : Manager<ScreenManager>
    {
        public IEnumerator CurrentScreenReload()
        {
            yield return StartCoroutine(UI_ScreenFader.FadeScenOut());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            yield return StartCoroutine(UI_ScreenFader.FadeSceneIn());
            GameManager.Instance.FindPlayer();
        }

        public IEnumerator NextSceneload(string s = null)
        {
            UIManager.Instance.iscutSceenCheck = false;
            yield return StartCoroutine(UI_ScreenFader.FadeScenOut());
            if (SceneManager.GetActiveScene().buildIndex + 1 >= 5)
            {
                SceneManager.LoadScene("MainScene");
            }
            if (s == null)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(s);
            
            if(s == "MainScene" || SceneManager.GetActiveScene().buildIndex + 1 >= 5)
                yield return StartCoroutine(UI_ScreenFader.FadeSceneIn());
            //GameManager.Instance.FindPlayer();
        }
    }
}