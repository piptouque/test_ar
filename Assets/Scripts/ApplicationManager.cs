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

    [SerializeField] private GameObject menuManager, calibrationManager, gameManager, uiManager;
    [SerializeField]
    private GameObject menuWrapper, calibrationWrapper, gameWrapper, uiWrapper;
    

    /*
     * the Content should include the Object to place
     * (plus the simulated scene, in our case)
     */
    [SerializeField]
    private GameObject contentWrapper;

    private static bool _shouldLoadMenu, _shouldLoadCalibration, _shouldLoadGame, _shouldLoadUITest;

    void Start()
    {
        _shouldLoadMenu = false;
        _shouldLoadCalibration = false;
        _shouldLoadGame = false;
        _shouldLoadUITest = false;
        
        HideManagersAndWrappers();
        DemandLoadMenu();
    }

    void ClearApplicationScenes()
    {
        if (IsSceneLoaded("MenuScene"))
        {
            UnloadMenu();
        }

        if (IsSceneLoaded("CalibrationScene"))
        {
            UnloadCalibration();
        }
        if (IsSceneLoaded("GameScene"))
        {
            UnloadGame();
        }

        if (IsSceneLoaded("UITestScene"))
        {
            UnloadUITest();
        }
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
        }
        else if (_shouldLoadGame)
        {
            _shouldLoadGame = false;
            LoadGame();
        }
        else if (_shouldLoadUITest)
        {
            _shouldLoadUITest = false;
            LoadUITest();
        }
        else
        {
            return;
        }
        ClearApplicationScenes();
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

    public static void DemandLoadUITest()
    {
        _shouldLoadUITest = true;
    }

    private static bool IsSceneLoaded(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName).isLoaded;
    }
    
    
    
    private void HideManagersAndWrappers()
    {
        menuManager.SetActive(false);
        calibrationManager.SetActive(false);
        gameManager.SetActive(false);
        uiManager.SetActive(false);
        
        menuWrapper.SetActive(false);
        calibrationWrapper.SetActive(false);
        gameWrapper.SetActive(false);
        uiWrapper.SetActive(false);
        
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

    private void LoadUITest()
    {
        StartCoroutine(LoadAsyncScene("UITestScene", uiManager, uiWrapper, true));
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
    
    private void UnloadUITest()
    {
        UnloadAsyncScene("UITestScene", uiManager, uiWrapper);
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
        
        /* moving the appropriate objects to the scene */
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
