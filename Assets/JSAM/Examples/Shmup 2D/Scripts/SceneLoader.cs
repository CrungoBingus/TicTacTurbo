﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JSAM.Example.Shmup2D
{
    public class SceneLoader : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        public void SwitchScene(int scene)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        }
    }
}