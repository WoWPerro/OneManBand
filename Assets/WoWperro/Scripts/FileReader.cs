using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

// La clase ya es estática, lo cual es la práctica correcta.
public static class FileReader
{
    // El método lee un archivo CSV y devuelve una lista de objetos Note.
    public static List<Note> ReadNotesFromCSV(string fileName)
    {
        List<Note> notes = new List<Note>();
        TextAsset txtAsset = Resources.Load<TextAsset>(fileName); // Forma moderna de cargar

        if (txtAsset == null)
        {
            Debug.LogError($"[FileReader] Error: No se pudo encontrar el archivo de notas '{fileName}' en la carpeta 'Resources'.");
            return notes;
        }

        string[] lines = txtAsset.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            string[] lineData = lines[i].Split(',');
            if (lineData.Length < 3) continue; // Ignorar líneas mal formateadas

            // Se asume el formato: tiempo,tipo,tiempoParaTocar
            float.TryParse(lineData[0], NumberStyles.Any, CultureInfo.InvariantCulture, out float time);
            int.TryParse(lineData[1], out int type);
            float.TryParse(lineData[2], NumberStyles.Any, CultureInfo.InvariantCulture, out float timeToTap);

            notes.Add(new Note(type, time, timeToTap));
        }
        return notes;
    }
}