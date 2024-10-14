using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnarPantallaJoc : MonoBehaviour
{
   public void AnarPantallaJugant()
    {
        DadesGlobals.ReiniciarPunts();
        SceneManager.LoadScene("PantallaJugant");
    }
}
