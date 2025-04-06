using UnityEngine;
using UnityEngine.Events;

public class OxygenSystem : MonoBehaviour
{
    [SerializeField] private float maxOxygen = 100f; 
    [SerializeField] private float oxygenDrainRate = 5f; 
    [SerializeField] private float oxygenRegenRate = 15f; 

    private GameManager _gameManager;
    private float _currentOxygen;

    void Start()
    {
        _currentOxygen = maxOxygen;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        float depth = _gameManager.zeroDepth.position.y - _gameManager.player.transform.position.y;

        // Если под водой
        if (depth > 0)
        {
            _currentOxygen -= oxygenDrainRate * Time.deltaTime; 
            _currentOxygen = Mathf.Clamp(_currentOxygen, 0, maxOxygen);
        }
        else
        {
            _currentOxygen += oxygenRegenRate * Time.deltaTime;
            _currentOxygen = Mathf.Clamp(_currentOxygen, 0, maxOxygen);
        }
    }

    public float GetCurrentOxygen()
    {
        return _currentOxygen;
    }
}
