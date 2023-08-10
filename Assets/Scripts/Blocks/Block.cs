using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceLenght;

    private int _destroyPrice;
    private int _filling;

    public int LeftToFill => _destroyPrice - _filling;


    public event UnityAction<int> FillingUpdated;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Dictionary<int, Color> _colors = new Dictionary<int, Color>()
    {
        { 0, new Color( 153f/255f,255f/255f,153f/255f, 1f) },
        { 25, new Color( 252f/255f,232f/255f,131f/255f, 1f) },
        { 50, new Color( 252f/255f,232f/255f,131f/255f, 1f)  },
        { 75, new Color( 255f/255f,140f/255f,105f/255f, 1f)  },
        { 100, new Color( 216f/255f,191f/255f,216f/255f, 1f)  },
        { 125, new Color( 161f/255f,133f/255f,148f/255f, 1f)  }
    };
    private int _colorIndex;

    private void Start()
    {
        _destroyPrice = Random.Range(_destroyPriceLenght.x, _destroyPriceLenght.y);

        foreach(int i in _colors.Keys)
            if(_destroyPrice >= i)
            {
                _colorIndex = i;
                _spriteRenderer.color = _colors[i];
            }

        FillingUpdated.Invoke(LeftToFill);
    }

    public void Fill()
    {
        _filling++;
        FillingUpdated.Invoke(LeftToFill);

        if(LeftToFill < _colorIndex)
        {
            if(_colorIndex > 0)
                _colorIndex -= 25;
            _spriteRenderer.color = _colors[_colorIndex];
        }

        if (_filling == _destroyPrice)
        {
            Destroy(this.gameObject);
        }
    }
}
