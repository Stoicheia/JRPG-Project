using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float lifetime = 1.5f;
    [SerializeField] private Vector3 floatDirection = Vector3.up;
    public TextMeshProUGUI textMesh;

    private Color _startColor;
    private float _timer;

    private void Awake()
    {
        _startColor = textMesh.color;
        transform.SetAsLastSibling();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        transform.position += floatDirection * floatSpeed * Time.deltaTime;

        float alpha = Mathf.Lerp(_startColor.a, 0f, _timer / lifetime);
        textMesh.color = new Color(_startColor.r, _startColor.g, _startColor.b, alpha);

        if (_timer >= lifetime)
            Destroy(gameObject);
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
