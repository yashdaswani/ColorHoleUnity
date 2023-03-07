using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UndergorundCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!GameManager.isGameOver)
        {
            string tag = other.tag;

            if (tag.Equals("Object"))
            {
                Level.Instance.objectsInScene--;
                if(other.gameObject.name == "Cylinder 5")
                GameManager.score= GameManager.score + 5;
                if(other.gameObject.name == "Cylinder 10")
                GameManager.score= GameManager.score + 10;
                UIManager.Instance.scoretext.text = "Score : " + GameManager.score.ToString();
                UIManager.Instance.UpdateLevelProgress();
                Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
                Destroy(other.gameObject);

                if(Level.Instance.objectsInScene==0)
                {
                    UIManager.Instance.showLevelcompltetext();
                    Level.Instance.PlayWinFx();
                    Invoke("NextLevel", 2f);
                }

            }
            if (tag.Equals("Obstacle"))
            {
                GameManager.isGameOver = true;
                Camera.main.transform
                    .DOShakePosition(1f, .2f, 20, 90f)
                    .OnComplete(() => {
                        //restart level after shaking complet
                        Level.Instance.RestartLevel();
                    });
            }
        }
       
    }

    void NextLevel()
    {
        Level.Instance.LoadNextLevel();
    }
}
