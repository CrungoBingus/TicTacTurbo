using UnityEngine;

using System.Collections;

public class PlayerMotor : MonoBehaviour
{
    PlayerColors myColor;

    [SerializeField] Transform _Base;


    //
    // Later make private & allocate with GameManager
    //
    Camera _myCamera;

    Vector2 _mousePosition;
    Ray _rayOrigin;
    RaycastHit _hitInfo;


    public static PlayerMotor Instance;
    private void Awake()
    {
        Instance = this;

        _myCamera = GetComponentInChildren<Camera>();
    }

    public void StartPlayerMotor()
    {
        _Base.Rotate(0, GameManager.Instance.ReturnRotationForColor(myColor), 0);
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _rayOrigin = _myCamera.ScreenPointToRay(_mousePosition);

        Hover();
        Click();
    }

    void Click()
    {
        if (!Input.GetMouseButtonDown(0) || myColor != GameManager.Instance.playersTurn)
            return;

        if (Physics.Raycast(_rayOrigin, out _hitInfo))
        {
            var mm_objectHit = _hitInfo.collider;

            if (mm_objectHit.GetComponentInParent<BaseTile>())
            {
                BaseTile mm_ClickedTile = mm_objectHit.GetComponentInParent<BaseTile>();

                if (GameManager.Instance.GameBoard[mm_ClickedTile.x, mm_ClickedTile.y] == TileStates.Empty &&
                    !GameManager.Instance.isBusy)
                {
                    GameManager.Instance.BuildOnTile(myColor, mm_ClickedTile.x, mm_ClickedTile.y);

                    //
                    // If ! preliminary Effect
                    //
                    EffectManager.Instance.gameEffects[GameManager.Instance.currentEffect].RunEffect(myColor);

                    GameManager.Instance.MoveToNextTurn();
                }
            }
        }
    }
    void Hover()
    {
        if (GameManager.Instance.isBusy)
            return;

        if(Physics.Raycast(_rayOrigin, out _hitInfo))
        {
            var mm_objectHit = _hitInfo.collider;

            if (mm_objectHit.GetComponentInParent<BaseTile>())
            {
                mm_objectHit.GetComponentInParent<BaseTile>().IsHovered(GameManager.Instance.baseHoverMaterial);
            }
        }
    }

    public void SetColor(PlayerColors m_pc)
    {
        myColor = m_pc;
    }
    public PlayerColors GetColor()
    {
        return myColor;
    }
}