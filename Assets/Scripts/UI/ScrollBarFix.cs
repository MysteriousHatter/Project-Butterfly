using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollBarFix : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(FixScrollbar());
    }

    IEnumerator FixScrollbar()
    {
        yield return new WaitForEndOfFrame();
        this.GetComponent<Scrollbar>().value = 1;
    }
}
