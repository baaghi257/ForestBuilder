using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Upgrader : MonoBehaviour
{
    public Image fillImage;
    public float duration;
    public string obj;

    private int upgradeCost;

    public int UpgradeCost { get { return upgradeCost; } set { upgradeCost = value; } }

    private void Start()
    {
        fillImage.fillAmount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(GameManager.instance.CanBuy());
        if(GameManager.instance.CanBuy())
        {
            AnimateUpgrade(1f);
        }
        
    }
    private void AnimateUpgrade(float animTime)
    {
        DOTween.To(() => fillImage.fillAmount, x => fillImage.fillAmount = x, animTime, duration).OnComplete(() => GameManager.instance.UpgradeObject(obj)); 
    }

}
