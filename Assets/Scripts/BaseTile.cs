using UnityEngine;

public class BaseTile : MonoBehaviour
{
    [SerializeField] Transform _tileBody;
    float desiredYPositionTileBody = 0f;

    public Material defualtMaterial;
    MeshRenderer _myRenderer;

    public int x=0, y=0;

    private void Awake()
    {
        _myRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void IsHovered(Material m_material)
    {
        desiredYPositionTileBody = 0.5f;
        _myRenderer.material = m_material;
    }

    private void FixedUpdate()
    {
        _tileBody.localPosition = new Vector3(0,
            Mathf.Lerp(_tileBody.localPosition.y, desiredYPositionTileBody, Time.deltaTime * 3f),
            0f);
        desiredYPositionTileBody = 0f;

        _myRenderer.material = defualtMaterial;
    }
}