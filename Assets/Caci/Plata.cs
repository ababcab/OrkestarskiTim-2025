using UnityEngine;

public class Plata : MonoBehaviour
{
    [SerializeField]
    private int money=100;
    public void AddMoney(int am)
    {
        money += am;
    }


    public bool EnoughMoney()
    {
        if(money > 10)
        {
            money -= 10;
            return true;
        }
        return false;
    }
}
