using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    #region Singleton class : Level

    public static Level instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    [SerializeField] ParticleSystem winFx;

    [Space]
    public int totalObject;
    public int objectInScene;

    [SerializeField] Transform objectParent;

    
    [Space]
    [Header( " Level Data " )]
    [SerializeField] Material groundMaterial;
    [SerializeField] Material objectMaterial;
    [SerializeField] Material obstacleMaterial;
    [SerializeField] Material borderMaterial;

    //[SerializeField] SpriteRenderer groundSideSprite;
    [SerializeField] Image progressFillImage;

   // [SerializeField] SpriteRenderer bgFadeSprite;

    [Space]
    [Header(" Level Color ")]
    [Header("Ground")]
    [SerializeField] Color groundColor;
    [SerializeField] Color borderColor;
   // [SerializeField] Color sideColor;


    [Header("Object & Obstacle")]
    [SerializeField] Color objectColor;
    [SerializeField] Color obstacleColor;

    [Header("UI (progress)")]
    [SerializeField] Color progressFillColor;

    [Header("Bacground")]
    [SerializeField] Color cameraColor;
    [SerializeField] Color fadeColor;
    void Start()
    {
        CountObjects();
        UpdateLevelColors();
    }

    // Update is called once per frame
    void CountObjects()
    {
        totalObject = objectParent.childCount;
        objectInScene = totalObject;
    }

    public void PlayWinFx()
    {
        winFx.Play();   
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateLevelColors()
    {
        groundMaterial.color = groundColor;

        objectMaterial.color = objectColor;
        obstacleMaterial.color = obstacleColor;
        borderMaterial.color = borderColor; 

        progressFillImage.color = progressFillColor;

        Camera.main.backgroundColor = cameraColor;
       // bgFadeSprite.color = fadeColor;

    }

     void OnValidate()
    {
        UpdateLevelColors();
    }
}
