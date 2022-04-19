using TMPro;
using UnityEngine;

public class FloatScoreText : MonoBehaviour 
{
    [SerializeField] float floatSpeed = 3f;

    public void SetScoreValue(int multiplier)
    {
        GetComponent<TMP_Text>().SetText("+ " + multiplier);

        Destroy(gameObject, 0.5f);

    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * floatSpeed;
    }

}