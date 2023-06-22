using UnityEngine;

// Stop inputs
// Pass effect from player side
// Run effect logics (reading state, getting randoms)
// Pass effect logics to server
// Run on all clients
// Resume inputs
// Move next turn

public class Effect_BASE : MonoBehaviour
{
    public string effectName;
    public Sprite effectIcon;
    [TextArea] public string effectDescription;

    public bool isPreliminary = false;

    public TileStates targetTileState = TileStates.Empty;

    public virtual void RunEffect(PlayerColors m_pc)
    {
        // BITCH!
    }
    public virtual void RunEffect(PlayerColors m_pc, int x, int y)
    {

    }
}