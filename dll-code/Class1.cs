using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MDCleaner
{
    public class Utils
    {

        // A static, read-only dictionary mapping common MIME types to their primary file extension.
        // The keys (MIME types) are converted to lowercase for case-insensitive lookup.
        private readonly Dictionary<string, string> MimeTypeToExtensionMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // --- Images ---
        { "image/jpeg", ".jpg" },
        { "image/png", ".png" },
        { "image/gif", ".gif" },
        { "image/bmp", ".bmp" },
        { "image/webp", ".webp" },
        { "image/tiff", ".tif" },
        { "image/svg+xml", ".svg" },

        // --- Documents & Text ---
        { "application/pdf", ".pdf" },
        { "text/plain", ".txt" },
        { "text/html", ".html" },
        { "text/css", ".css" },
        { "application/javascript", ".js" },
        { "application/json", ".json" },
        { "application/xml", ".xml" },
        
        // --- Archives ---
        { "application/zip", ".zip" },
        { "application/x-rar-compressed", ".rar" },

        // --- Microsoft Office (OOXML Modern Formats) ---
        { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx" }, // Word
        { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ".xlsx" },      // Excel
        { "application/vnd.openxmlformats-officedocument.presentationml.presentation", ".pptx" }, // PowerPoint

        // --- Microsoft Office (Legacy Binary Formats) ---
        { "application/msword", ".doc" },
        { "application/vnd.ms-excel", ".xls" },
        { "application/vnd.ms-powerpoint", ".ppt" },

        // --- Audio & Video ---
        { "audio/mpeg", ".mp3" },
        { "audio/wav", ".wav" },
        { "video/mp4", ".mp4" },
        { "video/quicktime", ".mov" }
    };

        public string StripMarkdownCodeBlock(string input)
        {
            try
            {
                // Step 1: Remove leading and trailing Markdown code block markers (e.g., ```json, ```csharp, or ```)
                // Handles optional language specifier after the opening ```
                string markdownPattern = @"^```[a-zA-Z]*\s*|```$";
                string result = Regex.Replace(input, markdownPattern, "", RegexOptions.Multiline).Trim();

                // Step 2: If the input didn't contain ``` markers, check if it starts with
                // a bare language identifier like "json", "html", "xml", "csharp", etc.
                // This handles cases where the LLM outputs something like: json {"key": "value"}
                if (!input.Contains("```"))
                {
                    string bareLanguagePattern = @"^(json|html|xml|csharp|css|javascript|js|typescript|ts|python|sql|yaml|yml|bash|sh|plaintext|text|markdown|md)\s+";
                    result = Regex.Replace(result, bareLanguagePattern, "", RegexOptions.IgnoreCase).Trim();
                }
                return result;
            }
            catch (Exception ex)
            {
                //return "Exception: " + ex.GetType() + "\n" + ex.ToString() + "\n" + ex.Message;
                return input;
            }
        }

        /// <summary>
        /// Converts a given MIME type string back to its corresponding file extension.
        /// </summary>
        /// <param name="mimeType">The MIME type (e.g., "image/jpeg").</param>
        /// <returns>The file extension (e.g., ".jpg") including the dot, or null if the MIME type is not recognized.</returns>
        public string GetExtensionFromMimeType(string mimeType)
        {
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                return "unknown";
            }

            // Normalize the input MIME type to ensure a match in the case-insensitive dictionary.
            string normalizedMimeType = mimeType.ToLowerInvariant().Trim();

            if (MimeTypeToExtensionMap.TryGetValue(normalizedMimeType, out string extension))
            {
                return extension;
            }

            // Return null if no matching extension is found
            return "unknown";
        }
    }
}
