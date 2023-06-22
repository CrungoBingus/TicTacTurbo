using UnityEngine;

using System.Collections;

public class eHostileTakeover : Effect_BASE
{
    public override void RunEffect(PlayerColors m_pc, int m_x, int m_y)
    {
        GameManager.Instance._pointOfInterest.position = new Vector3(m_x * 1.25f, 0, m_y * 1.25f);
    }
}