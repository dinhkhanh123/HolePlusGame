using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton class : UIManager

    public static UIManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    #endregion


    [Header("Level Progress UI")]
    [SerializeField] int sceneOffset;
    [SerializeField] TMP_Text nextLevelText;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] Image progressFillImage;

    [Space]
    [SerializeField] TMP_Text levelCompletedText;

    [Space]
    [SerializeField] Image fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        FadeAtStart();

        progressFillImage.fillAmount = 0f;
        SetLevelProgressText();
    }

    void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }

    // Update is called once per frame
   public void UpdateLevelProgress()
    {
        float val = 1f - ((float)Level.instance.objectInScene / Level.instance.totalObject);
        // progressFillImage.fillAmount = val;
        progressFillImage.DOFillAmount(val, .4f);
    }


    public void ShowLevelCompletedUI()
    {
        levelCompletedText.DOFade(1f, .6f).From(0f);
    }

    public void FadeAtStart()
    {
        fadePanel.DOFade(0f, .8f).From(1f);
    }
}
