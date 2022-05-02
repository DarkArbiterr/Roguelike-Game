using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemInfoScreen : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public void Setup(string name, string descr)
    {
        nameText.text = name;
        descriptionText.text = descr;
        gameObject.SetActive(true);
        StartCoroutine(SetActive());
    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
