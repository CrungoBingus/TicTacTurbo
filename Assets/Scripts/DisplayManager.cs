using UnityEngine;
using UnityEngine.UI;

using System.Collections;

using TMPro;

public class DisplayManager : MonoBehaviour
{
    [Header("Display Effects Items")]
    [SerializeField] GameObject effectParent;
    [SerializeField] TMP_Text effectName;
    [SerializeField] TMP_Text effectDescription;

    public static DisplayManager Instance;
    private void Awake()
    {
        Instance = this;
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

        //
        // play Animation?
        //

        yield return new WaitForSeconds(5f);

        effectParent.SetActive(false);
        GameManager.Instance.isBusy = false;
    }
}