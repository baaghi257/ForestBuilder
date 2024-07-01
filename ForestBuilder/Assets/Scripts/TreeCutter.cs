using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCutter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement.instance.Attack(true);
            Invoke("DestroyObj", 3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke("DestroyObj");
        PlayerMovement.instance.Attack(false);
    }
    public void DestroyObj()
    {
        //GameManager.instance.AnimateCoins();
        GameManager.instance.AddCoins(50);
        PlayerMovement.instance.Attack(false);
        Destroy(this.gameObject);
    }
}
