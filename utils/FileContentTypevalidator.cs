using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

public static class SecureFileValidator
{
   //magic bytes
    private static readonly Dictionary<string, byte[]> StandardSignatures = new(StringComparer.OrdinalIgnoreCase)
    {
        
        { "application/pdf", new byte[] { 0x25, 0x50, 0x44, 0x46 } },
        
    };

   
    public static bool IsValidMimeType(Stream stream, string allowedMimeType)
    {
        if (stream == null || stream.Length == 0) return false;

        long originalPosition = stream.Position;
        try
        {
            // Handle standard non-zip signatures
            if (StandardSignatures.TryGetValue(allowedMimeType, out var signature))
            {
                return MatchSignature(stream, signature);
            }


            return false; 
        }
        finally
        {
           
            if (stream.CanSeek) stream.Position = originalPosition;
        }
    }

    private static bool MatchSignature(Stream stream, byte[] signature)
    {
        if (stream.Length < signature.Length) return false;

        byte[] buffer = new byte[signature.Length];
        stream.Position = 0;
        int bytesRead = stream.Read(buffer, 0, buffer.Length);

        if (bytesRead < signature.Length) return false;

        return buffer.SequenceEqual(signature);
    }

   

}