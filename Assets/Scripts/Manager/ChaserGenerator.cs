using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserGenerator : MonoSingleton<ChaserGenerator>
{
    [SerializeField]
    private GameObject _chaserPrefab = null;

    [SerializeField]
    private Vector3 _chaserSpawnPosition = Vector3.zero;

    private Chaser _chaser = null;

    public Chaser Chaser => _chaser;

    private bool _isGenerating = false;

    public bool GenerateChaser()
    {
        if (_isGenerating || PlayerController.Instance.Height < 100f)
            return false;

        _isGenerating = true;

        GameObject chaser = Instantiate(_chaserPrefab, _chaserSpawnPosition, Quaternion.identity);
        _chaser = chaser.GetComponent<Chaser>();

        return true;
    }
}
