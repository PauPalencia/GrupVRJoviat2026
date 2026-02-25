using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public static class TokenSequenceUtils
{
    private static readonly Regex NumberRegex = new Regex(@"-?\d+", RegexOptions.Compiled);

    public static bool TryGetTokenNumber(GameObject source, out int number)
    {
        number = 0;
        if (source == null)
        {
            return false;
        }

        CrearSecuencia crearSecuencia = source.GetComponentInParent<CrearSecuencia>();
        if (crearSecuencia != null)
        {
            number = crearSecuencia.NumeroToken;
            return true;
        }

        if (TryParseNumber(source.name, out number))
        {
            return true;
        }

        TMP_Text tmp = source.GetComponentInChildren<TMP_Text>(true);
        if (tmp != null && TryParseNumber(tmp.text, out number))
        {
            return true;
        }

        TextMesh textMesh = source.GetComponentInChildren<TextMesh>(true);
        if (textMesh != null && TryParseNumber(textMesh.text, out number))
        {
            return true;
        }

        return false;
    }

    public static int PaintAllTokens(Color color)
    {
        HashSet<Renderer> renderersToPaint = new HashSet<Renderer>();

        // Ruta principal: objetos cuyo nombre empiece por "token".
        Transform[] allTransforms = Object.FindObjectsOfType<Transform>(true);
        foreach (Transform transform in allTransforms)
        {
            if (!IsTokenName(transform.name))
            {
                continue;
            }

            foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>(true))
            {
                renderersToPaint.Add(renderer);
            }
        }

        // Fallback: cualquier objeto con CrearSecuencia, por si el nombre no sigue el patrón "token".
        if (renderersToPaint.Count == 0)
        {
            CrearSecuencia[] tokens = Object.FindObjectsOfType<CrearSecuencia>(true);
            foreach (CrearSecuencia token in tokens)
            {
                foreach (Renderer renderer in token.GetComponentsInChildren<Renderer>(true))
                {
                    renderersToPaint.Add(renderer);
                }
            }
        }

        foreach (Renderer renderer in renderersToPaint)
        {
            foreach (Material material in renderer.materials)
            {
                ApplyColorToMaterial(material, color);
            }
        }

        return renderersToPaint.Count;
    }

    private static void ApplyColorToMaterial(Material material, Color color)
    {
        if (material == null)
        {
            return;
        }

        if (material.HasProperty("_Color"))
        {
            material.SetColor("_Color", color);
        }

        if (material.HasProperty("_BaseColor"))
        {
            material.SetColor("_BaseColor", color);
        }

        material.color = color;
    }

    private static bool IsTokenName(string name)
    {
        return name.StartsWith("token", System.StringComparison.OrdinalIgnoreCase);
    }

    private static bool TryParseNumber(string value, out int number)
    {
        number = 0;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        Match match = NumberRegex.Match(value);
        return match.Success && int.TryParse(match.Value, out number);
    }
}
