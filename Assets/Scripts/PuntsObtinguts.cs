using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntsObtinguts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Punts Obtinguts: " + DadesGlobals.punts.ToString();
        Invoke("AnarPantallaInici", 5f);

    }


    private void AnarPantallaInici()

    {
        SceneManager.LoadScene("PantallaInici");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
