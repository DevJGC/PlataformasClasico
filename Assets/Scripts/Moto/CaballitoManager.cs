using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaballitoManager : MonoBehaviour
{
    [SerializeField] DetectorRueda detectorRueda; // para sacar segundosSinTocar total
    [SerializeField] Moto moto; // para sacar si meta = true

    [SerializeField] private int segundosSinTocarMax = 0; 
    [SerializeField] private int segundosSinTocarParcial = 0;

    // text canvas final
    [SerializeField] private TextMeshProUGUI textTitulo;
    [SerializeField] private TextMeshProUGUI textRecord;
    [SerializeField] private TextMeshProUGUI textParcial;

    bool unaVez;

    private const string RECORD_KEY = "record_segundosSinTocarMax"; // clave para guardar/recuperar el valor de PlayerPrefs

    void Start()
    {
        // Al iniciar, recuperamos el valor máximo guardado previamente
        if (PlayerPrefs.HasKey(RECORD_KEY))
        {
            segundosSinTocarMax = PlayerPrefs.GetInt(RECORD_KEY); //  guarda el record
        }
    }

    void Update()
    {
        // cuando moto.meta == true recoger detectorRueda.segundosSinTocar
        if (moto.meta == true && unaVez == false)
        {
            unaVez = true;
            segundosSinTocarParcial = detectorRueda.segundosSinTocar; // guardamos el valor parcial
            GetCaballito();
        }

        // Verificar si la tecla 'R' es presionada
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteKey(RECORD_KEY); // Eliminamos la clave y por ende el record
            segundosSinTocarMax = 0; // Restablecemos el valor en memoria
            Debug.Log("El record ha sido restablecido.");
        }
    }


    // comparar segundosSinTocarParcial con segundosSinTocarMax
    private void GetCaballito()
    {
        Debug.Log("Chequeo de record");
        if (segundosSinTocarParcial > segundosSinTocarMax)
        {
            segundosSinTocarMax = segundosSinTocarParcial; // es que se ha superado el record
            PlayerPrefs.SetInt(RECORD_KEY, segundosSinTocarMax); // guardamos el nuevo record
            PlayerPrefs.Save(); // asegurarnos de que los cambios se guarden

            // logica si se ha superado el record
            Debug.Log("Se ha superado el record");
            NuevoRecord();
        }
        else
        {
            // logica si no se ha superado el record
            Debug.Log("No se ha superado el record");
            NoSuperado();

        }
    }

    // mostrar record en canvas final
    private void NuevoRecord()
    {
        textTitulo.text = "Nuevo Record!";
        textRecord.text = "Record: " + segundosSinTocarMax.ToString();
        textParcial.text = "Actual: " + segundosSinTocarParcial.ToString();
    }

    // mostrar final sin record canvas final
    private void NoSuperado()
    {
        textTitulo.text = "Completado!";
        textRecord.text = "Record: " + segundosSinTocarMax.ToString();
        textParcial.text = "Actual: " + segundosSinTocarParcial.ToString();
    }


}
