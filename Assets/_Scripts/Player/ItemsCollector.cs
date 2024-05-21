using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCollector : MonoBehaviour
{
    private int counts;
    [SerializeField] private Text apples_text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Items"))
        {
            Destroy(collision.gameObject);
            counts++;
            apples_text.text = "apples:" + counts;
        }
    }
}
