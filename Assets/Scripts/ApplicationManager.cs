using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class ApplicationManager : MonoBehaviour
{
    /*
     * when switching scenes
     * we also need to move the origin,
     * in order to access the AR camera
     */
    [SerializeField]
    private GameObject arOrigin;
    [SerializeField]
    private GameObject menuManager, calibrationManager, gameManager;
    [SerializeField]
    private GameObject menuWrapper, calibrationWrapper, gameWrapper;
    

    /*
     * the Content should include the Object to place
     * (plus the simulated scene, in our case)
     */
    [SerializeField]
    private GameObject contentWrapper;

    private static bool _shouldLoadMenu, _shouldLoadCalibration, _shouldLoadGame;

    void Start()
    {
        _shouldLoadMenu = true;
        _shouldLoadCalibration = false;
        _shouldLoadGame = false;
        
        HideManagersAndWrappers();
    }



    void Update()
    {
        if (_shouldLoadMenu)
        {
            _shouldLoadMenu = false;
            LoadMenu();
        }
        else if (_shouldLoadCalibration)
        {
            _shouldLoadCalibration = false;
            LoadCalibration();
            UnloadMenu();
        }
        else if (_shouldLoadGame)
        {
            _shouldLoadGame = false;
            LoadGame();
            UnloadCalibration();
        }
    }

    public static void DemandLoadMenu()
    {
        _shouldLoadMenu = true;
    }

    public static void DemandLoadCalibration()
    {
        _shouldLoadCalibration = true;
    }

    public static void DemandLoadGame()
    {
        _shouldLoadGame = true;
    }
    
    private void HideManagersAndWrappers()
    {
        menuManager.SetActive(false);
        calibrationManager.SetActive(false);
        gameManager.SetActive(false);
        
        menuWrapper.SetActive(false);
        calibrationWrapper.SetActive(false);
        gameWrapper.SetActive(false);
        
        /* also disables camera */
        arOrigin.SetActive(false);
    }

    private void LoadMenu()
    {
        StartCoroutine(LoadAsyncScene("MenuScene", menuManager, menuWrapper, false));
    }

    private void LoadCalibration()
    {
        StartCoroutine(LoadAsyncScene("CalibrationScene", calibrationManager, calibrationWrapper, true));
    }

    private void LoadGame()
    {
        StartCoroutine(LoadAsyncScene("GameScene", gameManager, gameWrapper, true));
    }

    private void UnloadMenu()
    {
        UnloadAsyncScene("MenuScene", menuManager, menuWrapper);
    }
    
    private void UnloadCalibration()
    {
        UnloadAsyncScene("CalibrationScene", calibrationManager, calibrationWrapper);
    }

    private void UnloadGame()
    {
        UnloadAsyncScene("GameScene", gameManager, gameWrapper);
    }
    
    
    
    
    private IEnumerator LoadAsyncScene(string sceneName, GameObject sceneManager, GameObject sceneWrapper, bool showContent)
    {
        /*
         * see:https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.MoveGameObjectToScene.html 
         */
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
       
        /* loading done! */
        
        /* moving the appropriate objects to the Calibration Scene */
        MoveUsedObjectsToScene(sceneName, sceneWrapper);
        /* activating the corresponding Manager and Wrapper*/
        sceneManager.SetActive(true);
        sceneWrapper.SetActive(true);
        if (showContent)
        {
            /* showing content */
            contentWrapper.SetActive(true);
            /* should also activate the main Camera */
            arOrigin.SetActive(true);
        }
    }

    private void UnloadAsyncScene(string sceneName, GameObject sceneManager, GameObject sceneWrapper)
    {
        /* moving the objects back */
        MoveUsedObjectsToScene("Root", sceneWrapper);
        /* de-activating the Manager and Wrapper */
        sceneManager.SetActive(false);
        sceneWrapper.SetActive(false);
        contentWrapper.SetActive(false);
        arOrigin.SetActive(false);
        

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(sceneName));
    }

    private void MoveUsedObjectsToScene(string sceneName, GameObject sceneWrapper)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        /* moving the objects back */
        SceneManager.MoveGameObjectToScene(sceneWrapper, scene);
        SceneManager.MoveGameObjectToScene(arOrigin, scene);
        SceneManager.MoveGameObjectToScene(contentWrapper, scene);
    }

}
