using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    [Tooltip("The name of the scene to be loaded")] [SerializeField]
    private string scene;

    private void Start()
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        Application.targetFrameRate = Mathf.FloorToInt((float)Screen.currentResolution.refreshRateRatio.value);
#endif
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
}