using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] btn;

    private DataManager data;
    private void Start()
    {
        data = DataManager.instance;
        data.LoadFromFile();
        for (int i = data.maxUnlockedLevel; i < btn.Length; i++)
        {
            LockLevel(i);
        }
    }
    public void LoadLevel(int level)
    {
        try
        {
            SceneManager.LoadScene(level);
        }
        catch (System.Exception)
        {
            Debug.Log("Scene can't be loaded, add scene to build manager");
            throw;
        }          
    }

    public void LockLevel(int level)
    {
        btn[level].interactable = false;
    }

    public void UnlockLevel(int level)
    {
        btn[level].interactable = true;
    }
}
