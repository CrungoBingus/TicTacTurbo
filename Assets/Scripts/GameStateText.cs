using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class GameStateText : MonoBehaviour
{
    public TMP_Text _turnText, _colorText, _roundNumber;

    private void FixedUpdate()
    {
        _turnText.text = GameManager.Instance.ReturnPlayerColorString(GameManager.Instance.playersTurn) + "'s Turn";
        _colorText.text = "You are " + GameManager.Instance.ReturnPlayerColorString(PlayerMotor.Instance.GetColor()) + ".";
        _roundNumber.text = "Round " + GameManager.Instance.gameRoundCount.ToString();
    }
}