using UnityEngine;

using System.Collections;

public class eHostileTakeover : Effect_BASE
{
    public override void RunEffect(PlayerColors m_pc, int m_x, int m_y)
    {
        StartCoroutine(RunEffect2(m_pc, m_x, m_y));
    }
    IEnumerator RunEffect2(PlayerColors m_pc, int m_x, int m_y)
    {
        GameManager.Instance._pointOfInterest.position = new Vector3(m_x * 1.25f, 0, m_y * 1.25f);

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.DestroyOnTile(m_pc, m_x, m_y);
        GameManager.Instance.GameBoard[m_x, m_y] = TileStates.Empty;

        yield return new WaitForSeconds(1f);
    }
}