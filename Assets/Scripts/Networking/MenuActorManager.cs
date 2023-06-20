using UnityEngine;

public class MenuActorManager : MonoBehaviour
{
    MenuActor[] _menus;

    public static MenuActorManager instance;
    private void Awake()
    {
        instance = this;

        foreach (Transform m_menuTransform in transform)
            m_menuTransform.gameObject.SetActive(true);

        _menus = GetComponentsInChildren<MenuActor>();
        foreach (MenuActor m_menu in _menus)
        {
            m_menu.Close();
        }
    }

    public void OpenMenu(string m_name)
    {
        foreach (MenuActor m_menu in _menus)
        {
            m_menu.Close();
            if (m_menu.myName == m_name)
                m_menu.Open();
        }
    }
}