using UnityEngine;

public class eBonusBuild : Effect_BASE
{
    public override void RunEffect(PlayerColors m_pc)
    {
        TileStates[,] m_gameTileState = GameManager.Instance.GameBoard;

        int m_x = 0, m_y = 0;
        do 
        {
            m_x = Random.Range(0, 12);
            m_y = Random.Range(0, 12);
        } while (m_gameTileState[m_x, m_y] == TileStates.Built);

        GameManager.Instance.BuildOnTile(m_pc, m_x, m_y);
    }
}