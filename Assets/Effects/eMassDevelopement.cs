using UnityEngine;

using System.Collections;

public class eMassDevelopement : Effect_BASE
{
    public override void RunEffect(PlayerColors m_pc)
    {
        StartCoroutine(RunEffect2(m_pc));
    }
    IEnumerator RunEffect2(PlayerColors m_pc)
    {
        for (int i = 0; i < 2; i++)
        {
            TileStates[,] m_gameTileState = GameManager.Instance.GameBoard;

            int m_x = 0, m_y = 0;
            do
            {
                m_x = Random.Range(0, 12);
                m_y = Random.Range(0, 12);
            } while (m_gameTileState[m_x, m_y] == TileStates.Built);

            GameManager.Instance.BuildOnTile(m_pc, m_x, m_y);
            GameManager.Instance.GameBoard[m_x, m_y] = TileStates.Built;

            yield return new WaitForSeconds(1f);
        }
    }
}