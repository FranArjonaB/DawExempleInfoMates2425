using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * Repàs
 * Què hem vist:
 *  - Crear objectes a l'escena(arrosegant)
 *  - Crear EmptyObjects(anarHierarchy y createEmpty) es fa servir per el GeneradorNumeros
 *  - Prefabs, serveix per crear objectes quan el joc esta en execució(l'objecte que ja teniem creat l'arrosegavem a la carpeta prefabs)
 *  - Per crear un prefab a l'escena en execució: metode Instantiate(variable prefab)
 *  - VariablePrefab: variable de tipus GameObject.
 *  
 *  - Trobar posicio objecte actual: es el transform.position
 *  - Trobar marges pantalla: es el Camera.main.ViewportToWorldPoint().
 *  - [SerializedField]: serveix per fer que una variable privada de la clase es mostri a l'editor de Unity.
 *  - Utilitzar una imatge com si fos m'es d'una(contenint subimatges) es fa seleccionat l'sprite, en l'opció sprite Mode canviem de single a Multiple, i finalment anem a Sprite Editor.
 *  
 *  - Destruir objecte actual: Destroy(gameObject).
 *  - Cridar metode al cap de x segons, es fa amb el invoke, fent Invoke("NomMetode", xf (f son els segons)
 *  - Cridar un metode al cap de x segons i cada y segons: InvokeRepeating("Nommetodo", xf, yf).
 *  - Com aturar un InvokeRepeating: CancelInvoke("NomMetode).
 *  - Detectar un objecte toca a un altre: es fa afegint als objectes que volem que es toquin, els components: BoxCollider2D i Rigidbody2D. S'ha d'activar checkbox IsTrigger (boxcollider), GravityScale posar-lo a 0 (Rigidbody2D)
 *  - 
 *
 *  
 */

public class NauJugador : MonoBehaviour
{
    private float _vel;

    private Vector2 minPantalla, maxPantalla;

    [SerializeField] private GameObject prefabProjectil;
    [SerializeField] private GameObject prefabExplosio;

    [SerializeField] private TMPro.TextMeshProUGUI componentTextVides;

    private int videsNau;
    


    // Start is called before the first frame update
    void Start()
    {
        _vel = 8f;
        minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float meitatMidaImatgeX = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x / 2;
        float meitatMidaImatgeY = GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y / 2;

        minPantalla.x = minPantalla.x + 0.75f;
        maxPantalla.x = maxPantalla.x - 0.75F;
        minPantalla.y += 0.75f;
        maxPantalla.y -= 0.75f;

        videsNau = 3;
    }

    // Update is called once per frame
    void Update()
    {
        MoureNau();
        DisparaProjectil();

    }

    private void DisparaProjectil()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject projectil = Instantiate(prefabProjectil);
            projectil.transform.position = transform.position;
        }
    }
    private void MoureNau()
    {

        float direccioIndicadaX = Input.GetAxisRaw("Horizontal");
        float direccioIndicadaY = Input.GetAxisRaw("Vertical");

        Vector2 direccioIndicada = new Vector2(direccioIndicadaX, direccioIndicadaY).normalized;

        Vector2 novaPos = transform.position;
        novaPos = novaPos + direccioIndicada * _vel * Time.deltaTime;

        novaPos.x = Mathf.Clamp(novaPos.x, minPantalla.x, maxPantalla.x);
        novaPos.y = Mathf.Clamp(novaPos.y, minPantalla.y, maxPantalla.y);

        transform.position = novaPos;
    }

    private void OnTriggerEnter2D(Collider2D objecteTocat)
    {
        if (objecteTocat.tag == "Numero")
        {
            videsNau--;
            componentTextVides.text = "Vides:" + videsNau.ToString();

            if (videsNau <= 0)
            {
                GameObject explosio = Instantiate(prefabExplosio);
                explosio.transform.position = transform.position;

                SceneManager.LoadScene("PantallaResultats");



                Destroy(gameObject);
            }
        }
           
    }
}