using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UndergroundCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.isGameover)
        {
            string tag = other.tag;

            if (tag.Equals("Object"))
            {
                Level.instance.objectInScene--;
                UIManager.instance.UpdateLevelProgress();

                Magnet.instance.RemoveToMagnetField(other.attachedRigidbody);

                Destroy(other.gameObject);

                //check if win
                if(Level.instance.objectInScene == 0)
                {
                    UIManager.instance.ShowLevelCompletedUI();
                    Level.instance.PlayWinFx();
                    // no more object to collect
                    Invoke("NextLevel", 2f);
                }
            }

            if (tag.Equals("Obstacle"))
            {
                GameManager.isGameover = true;
                Camera.main.transform.DOShakePosition(1f, .2f, 20, 90f).OnComplete(() =>
                {
                    Level.instance.ReloadLevel();
                });
                
            }
        }
    }

    void NextLevel()
    {
        Level.instance.LoadNextLevel();
    }
}
