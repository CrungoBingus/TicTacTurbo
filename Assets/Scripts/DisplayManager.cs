using UnityEngine;
using UnityEngine.UI;

using System.Collections;

using TMPro;

public class DisplayManager : MonoBehaviour
{
    [Header("Round Display Items")]
    [SerializeField] GameObject roundParent;
    [SerializeField] TMP_Text roundColor;
    [SerializeField] TMP_Text roundNumber;
    [SerializeField] Image roundBackgroundImage;

    [Header("Display Effects Items")]
    [SerializeField] GameObject effectParent;
    [SerializeField] TMP_Text effectName;
    [SerializeField] TMP_Text effectDescription;

    public static DisplayManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void DisplayNewRound(int m_effectBase)
    {
        StartCoroutine(nameof(DisplayNewRoundEnum), m_effectBase);
    }

    IEnumerator DisplayNewRoundEnum(int m_effectBase)
    {
        GameManager.Instance.isBusy = true;
        /*roundParent.SetActive(false);
        roundParent.SetActive(true);*/
        roundParent.GetComponent<Animator>().SetBool("playAnim", true);

        //roundBackgroundImage.color = GameManager.Instance.ReturnMaterialForBuilding(GameManager.Instance.playersTurn).color;

        //roundColor.text = GameManager.Instance.playersTurn.ToString() + "'s Turn!";
        //roundNumber.text = "Round: " + GameManager.Instance.gameRoundCount.ToString();

<<<<<<< HEAD
        yield return new WaitForSeconds(1f);

        roundParent.SetActive(false);
=======
        yield return new WaitForSeconds(2f);
        roundParent.GetComponent<Animator>().SetBool("playAnim", false);
        //roundParent.SetActive(false);
>>>>>>> 7b0e21baaec4cea006e5d226e1c3451432b910e5
        GameManager.Instance.isBusy = false;

        DisplayNewEffect(m_effectBase);
    }

    public void DisplayNewEffect(int m_effectBase)
    {
        StartCoroutine(nameof(DisplayNewEffectEnum), m_effectBase);
    }

    IEnumerator DisplayNewEffectEnum(int m_effectBase)
    {
        GameManager.Instance.isBusy = true;
        effectParent.SetActive(true);

        effectName.text = EffectManager.Instance.gameEffects[m_effectBase].effectName;
        effectDescription.text = EffectManager.Instance.gameEffects[m_effectBase].effectDescription;

        yield return new WaitForSeconds(2f);

        effectParent.SetActive(false);
        GameManager.Instance.isBusy = false;
    }
}