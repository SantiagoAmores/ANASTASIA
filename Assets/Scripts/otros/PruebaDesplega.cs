using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// NOTA: SelectorDePrefabs se reemplaza por el nombre del script en el que haya que poner los desplegables
/*[CustomEditor(typeof(SelectorDePrefabs))]
public class PruebaDesplega : Editor
{
    public override void OnInspectorGUI()
    {
        SelectorDePrefabs selector = (SelectorDePrefabs)target;

        // NOTA: Los GUID son identificadores unicos que tiene cada asset del proyecto, aqui se crea una lista con assets del tipo Prefab dentro del proyecto
        string[] guids = AssetDatabase.FindAssets("t:Prefab");

        // Crea una lista con 
        string[] prefabPaths = new string[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            prefabPaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
        }

        string[] prefabNames = new string[guids.Length];

        // NOTA: Dependiendo de cantidad de desplegables necesarios se quitan o se ponen aqui!!
        int selectedIndex1 = -1, selectedIndex2 = -1, selectedIndex3 = -1;
        int counter = 0;

        foreach(var path in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            // NOTA: Aqui es donde se escribe el tag. Reemplazar por el tag de los objetos a incluir en los desplegables
            if (prefab.CompareTag("Projectile"))
            {
                prefabNames[counter] = prefab.name;
                // NOTA: Dependiendo de la cantidad de desplegables se ponen o se quitan linea aqui tambien
                if (prefab == selector.enemigo1)    {   selectedIndex1 = counter;   }
                if (prefab == selector.enemigo2)    {   selectedIndex2 = counter;   }
                if (prefab == selector.enemigoJefe) {   selectedIndex3 = counter;   }
                counter++;
            }
        }

        // Si se han encontrado prefabs con el tag, procede
        if (counter > 0) {
            
            string[] filteredPrefabNames = new string[counter];
            System.Array.Copy(prefabNames, filteredPrefabNames, counter);
            
            // NOTA: Cada uno de los siguientes selectedIndex* y sus correspondientes if son para los desplegables creados previamente
            // Es decir, que que se quitan o se añaden mas o menos dependiendo la cantidad de objetos que hagan falta
            // Añade el desplegable a la interfaz, que incluye el texto en si del selector, el int del prefab elegido y la lista en si, que sale al clicar
            // Dentro del if convierte el GameObject elegido al objeto de la clase en el que se necesite
            selectedIndex1 = EditorGUILayout.Popup("Selector 1", selectedIndex1, filteredPrefabNames);
            if (selectedIndex1 >= 0) {
                GameObject selected = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPaths[selectedIndex1]);
                selector.enemigo1 = selected;
            }

            selectedIndex2 = EditorGUILayout.Popup("Selector 2", selectedIndex2, filteredPrefabNames);
            if (selectedIndex2 >= 0) {
                GameObject selected = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPaths[selectedIndex2]);
                selector.enemigo2 = selected;
            }

            selectedIndex3 = EditorGUILayout.Popup("Selector 3", selectedIndex3, filteredPrefabNames);
            if (selectedIndex3 >= 0) {
                GameObject selected = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPaths[selectedIndex3]);
                selector.enemigoJefe = selected;
            }
        }

        // NOTA: Si se escribe mal el tag o no hay prefabs con ese tag se muestra esta advertencia en la interfaz de Unity
        else
        {
            EditorGUILayout.HelpBox("¡No se encontraron prefabs con el tag escrito en el código!", MessageType.Warning);
        }

        // La siguiente linea sirve para añadir los desplegables al script
        DrawDefaultInspector();
    }
}
*/