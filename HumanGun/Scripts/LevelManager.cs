using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Orbay.Core.Singelton;
using System.Threading.Tasks;

public class LevelManager : Singelton<LevelManager>
{

   
    // Start is called before the first frame update
    void Start()
    {
        StartUp();
    }

    public async void LoadScene(int sceneNum)
    {
        if(PlayerPrefs.GetInt("Level", 1) > SceneManager.sceneCountInBuildSettings - 1)
        {
            int a = PlayerPrefs.GetInt("Level", 1) % SceneManager.sceneCountInBuildSettings;
            if (a == 0 || a == 1) a = 2;
            sceneNum = a;
        }
        
        var scene = SceneManager.LoadSceneAsync(sceneNum);
        Debug.Log(sceneNum);
        scene.allowSceneActivation = false;

        while(scene.progress <0.9f)
        {
            await Task.Yield();
        }
        
        scene.allowSceneActivation = true;

    }

    public async void StartUp()
    {
        int sceneNum = PlayerPrefs.GetInt("Level", 1);
        if (PlayerPrefs.GetInt("Level", 1) > SceneManager.sceneCountInBuildSettings - 1)
        {
            int a = PlayerPrefs.GetInt("Level", 1) % SceneManager.sceneCountInBuildSettings;
            if (a == 0 || a == 1) a = 2;
            sceneNum = a;
        }

        var scene = SceneManager.LoadSceneAsync(sceneNum);
        scene.allowSceneActivation = false;
        

        do{
            CanvasController.Instance.loadingSlider.value = scene.progress;
            await Task.Yield();
           
        } while (scene.progress < 0.9f);

        CanvasController.Instance.loadingSlider.value = 1;
        scene.allowSceneActivation = true;
       
    }
    

  
}
