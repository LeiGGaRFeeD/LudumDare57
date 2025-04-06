using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _depthText;
    [SerializeField] private Slider _oxygenBar;
    [SerializeField] private GameManager _gameManager;

    private PlayerController playerController;

    void Start()
    {
        playerController = _gameManager.player.GetComponent<PlayerController>();

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        UpdateDepth();
        UpdateOxygen();
    }

    private void UpdateDepth()
    {
        float depth = _gameManager.zeroDepth.position.y - _gameManager.player.transform.position.y;

        if (depth < 0)
        {
            _depthText.text = $"Depth: 0m";
        }
        else
            _depthText.text = $"Depth: {depth.ToString("F2")}m";
    }

    private void UpdateOxygen()
    {
        if (_oxygenBar != null)
        {
            _oxygenBar.value = playerController.GetCurrentOxygen();
        }
    }
}
