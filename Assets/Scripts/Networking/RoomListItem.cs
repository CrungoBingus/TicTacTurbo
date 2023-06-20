using UnityEngine;

using Photon.Realtime;

using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public RoomInfo info;

    public void SetUp(RoomInfo m_info)
    {
        info = m_info;
        text.text = m_info.Name;
    }
    public void OnClick()
    {
        Launcher._instance.JoinRoom(info);
    }
}