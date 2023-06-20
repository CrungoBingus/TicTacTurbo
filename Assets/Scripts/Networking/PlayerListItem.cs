using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _text;
    Player _player;

    public void SetUp(Player m_player)
    {
        _player = m_player;
        _text.text = m_player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (_player == otherPlayer)
            Destroy(gameObject);
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}