using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class GameStateText : MonoBehaviour
{
    TMP_Text _myText;

    private void Awake()
    {
        _myText = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        _myText.text =
            "My Color: " + 
            GameManager.Instance.ReturnPlayerColorString(PlayerMotor.Instance.GetColor()) +
            "\nCurent Turn: " +
            GameManager.Instance.ReturnPlayerColorString(GameManager.Instance.playersTurn);
    }
}