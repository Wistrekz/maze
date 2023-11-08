using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Exit : MonoBehaviour
{
    [SerializeField] private Player _player;
    public bool IsNeedKey;
    [Space]
    [SerializeField] private Material _closedMaterial;
    [SerializeField] private Material _openMaterial;

    [HideInInspector] public bool IsPlayerOnExit;

    public bool IsOpen { get; private set; }

    private MeshRenderer _renderer;

    private void Awake()
    {
        if(!IsNeedKey)
        {
            Open();
        }
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name + 'и' +_player.name);
        Debug.Log(collision.gameObject.name == _player.name);
        if (collision.gameObject.name == _player.name)
        {
            IsPlayerOnExit = true;
        }
    }


    public void Open()
    {
        IsOpen = true;
        //_renderer.material.color = _openMaterial.color;
    }

    public void Close()
    {
        IsOpen = false;
        //_renderer.material.color = _closedMaterial.color;
    }
}
