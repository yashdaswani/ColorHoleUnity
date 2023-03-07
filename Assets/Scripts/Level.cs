using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    #region Singleton class: Level

    public static Level Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion
    [SerializeField] ParticleSystem winFx;
    [Space]
    //remaining objects
    [HideInInspector] public int objectsInScene;
    //total objects at the beginning
    [HideInInspector] public int totalObjects;

    //the Objects parent
    [SerializeField] Transform objectsParent;


    [SerializeField] Material objectsMaterial;
    [SerializeField] Material obstacleMaterial;
    [SerializeField] Material groundMaterial;
    [SerializeField] Image filledprogressimg;
    [SerializeField] SpriteRenderer bordersprite;
    [SerializeField] SpriteRenderer sidesprite;

    [SerializeField] Color groundColor;
    [SerializeField] Color objectColor;
    [SerializeField] Color obstacleColor;
    [SerializeField] Color borderColor;
    [SerializeField] Color sideColor;
    [SerializeField] Color filledprogressColor;
    [SerializeField] Color cameraColor;
    [SerializeField] Color fadeColor;






    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelColor();
        CountObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CountObjects()
    {
        //Count collectable white objects
        totalObjects = objectsParent.childCount;
        objectsInScene = totalObjects;
    }
    public void PlayWinFx()
    {
        winFx.Play();
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateLevelColor()
    {
        groundMaterial.color = groundColor;
        objectsMaterial.color = objectColor;
        obstacleMaterial.color = obstacleColor;
        Camera.main.backgroundColor = cameraColor;
        bordersprite.color = borderColor;
        sidesprite.color = sideColor;
        filledprogressimg.color= filledprogressColor;


    }

    private void OnValidate()
    {
        UpdateLevelColor();
    }
}
