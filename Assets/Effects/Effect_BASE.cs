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
    [TextArea] public string effectDescription;

    public virtual void RunEffect(PlayerColors m_pc)
    {
        // BITCH!
    }
}