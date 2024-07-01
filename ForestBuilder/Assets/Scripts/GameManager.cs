using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject cartSpawner, houseSpawner;
    public GameObject[] cartPrefab, housePrefab;

    private GameObject cart, house;

    Upgrader upgrader = new Upgrader();

    [HideInInspector] public int houseNum, cartNum;
    public float remainingCoins;
    private float targetValue;
    public float countDuration;
    public TextMeshProUGUI coinText;
    public GameObject coinImage;
    public GameObject coinSprite;
    public GameObject startCanvas, endCanvas;

    Coroutine C2T;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        SaveData.obj.Add(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        cart = Instantiate(cartPrefab[cartNum], cartSpawner.transform.position, Quaternion.LookRotation(cartSpawner.transform.forward));
        house = Instantiate(housePrefab[houseNum], houseSpawner.transform.position, Quaternion.LookRotation(houseSpawner.transform.forward));
        upgrader.UpgradeCost = 30;
        targetValue = remainingCoins;
        coinText.text = " : " + (int)remainingCoins;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            startCanvas.SetActive(true);
        }

        if(cartNum == cartPrefab.Length - 1 && houseNum == housePrefab.Length - 1)
        {
            endCanvas.SetActive(true);
        }
    }

    IEnumerator CountUp(float value)
    {
        var rate = Mathf.Abs(value - remainingCoins) / countDuration;
        while(remainingCoins != value)
        {
            remainingCoins = Mathf.MoveTowards(remainingCoins, value, rate * Time.deltaTime);
            coinText.text = " : " + ((int)remainingCoins).ToString();
            yield return null;
        }
    }
    IEnumerator CountDown(float value)
    {
        var rate = Mathf.Abs(value - remainingCoins) / countDuration;
        while (remainingCoins != value)
        {
            remainingCoins = Mathf.MoveTowards(remainingCoins, value, rate * Time.deltaTime);
            coinText.text = " :" + ((int)remainingCoins).ToString();
            yield return null;
        }
    }

    public void AddCoins(float value)
    {
        targetValue += value;
        if(C2T != null)
        {
            StopCoroutine(C2T);
        }
        C2T = StartCoroutine(CountUp(targetValue));
    }

    public void DeductCoins(float value)
    {
        targetValue -= value;
        if (C2T != null)
        {
            StopCoroutine(C2T);
        }
        C2T = StartCoroutine(CountDown(targetValue));
    }

    public bool CanBuy()
    {
        return remainingCoins - upgrader.UpgradeCost >= 0;
    }
    public void UpgradeObject(string obj)
    {
        switch (obj)
        {
            case "Cart":

                Destroy(cart.gameObject);

                cartNum++;
                
                int i = cartNum;
                if(i == cartNum)
                {
                    if(remainingCoins - upgrader.UpgradeCost >= 0)
                    {
                        cart = Instantiate(cartPrefab[i], cartSpawner.transform.position, Quaternion.LookRotation(cartSpawner.transform.forward));
                    }

                }
                break;

            case "House":
                Destroy(house.gameObject);
                houseNum++;
                int j = houseNum;

                if (j == houseNum)
                {
                    if(remainingCoins - upgrader.UpgradeCost >= 0)
                    {
                        house = Instantiate(housePrefab[j], houseSpawner.transform.position, Quaternion.LookRotation(houseSpawner.transform.forward));
                    }
                }

                break;

            default:
                break;
        }
        DeductCoins(upgrader.UpgradeCost);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

}
