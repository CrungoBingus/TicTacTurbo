using UnityEngine;

public class MenuActor : MonoBehaviour
{
    public string myName = "MENU_NAME";

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}