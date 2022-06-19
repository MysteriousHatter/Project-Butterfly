using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMeUI : MonoBehaviour
{
    private Scrollbar scroll;



    private void Awake()
    {
        scroll = GetComponent<Scrollbar>();
        scroll.size = 0.058f;
        scroll.direction = Scrollbar.Direction.BottomToTop;
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
