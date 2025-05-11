using TMPro;
using UnityEngine;

public class Plata : MonoBehaviour
{
    [SerializeField]
    private int money=0;
    [SerializeField]
    private TextMeshProUGUI money_text;

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

    private void Update()
    {
        money_text.SetText("Sredstva: " + money);
    }
}
