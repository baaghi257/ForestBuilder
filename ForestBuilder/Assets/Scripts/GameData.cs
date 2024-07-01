using UnityEngine;


[System.Serializable]
public class GameData
{
    public float remainingCoins;
    public int cartNum;
    public int houseNum;
    public float[] position;

    public GameData(GameManager manager)
    {
        remainingCoins = manager.remainingCoins;
        cartNum = manager.cartNum;
        houseNum = manager.houseNum;
        Vector3 cartPos = manager.cartSpawner.transform.position;
        position = new float[]
        {
            cartPos.x, cartPos.y,cartPos.z
        };
    }
}
