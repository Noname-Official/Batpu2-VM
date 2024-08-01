using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Godot;

class Assembler
{
    public static string Assemble(string path)
    {
        string pythonPath = "python";
        string scriptPath = @"assembler/main.py";

        string baseName = path.GetBaseName();
        string assemblyPath = path;
        string objectPath = baseName + ".o";
        string byteCodePath = baseName + ".bin";

        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = pythonPath,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            ArgumentList = { scriptPath, "-o", objectPath, assemblyPath },
        };

        using (Process process = Process.Start(start))
        {
            using (System.IO.StreamReader reader = process.StandardError)
            {
                string result = reader.ReadToEnd();
                if (result != "")
                    return result;
            }
        }

        start = new ProcessStartInfo
        {
            FileName = pythonPath,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            ArgumentList = { scriptPath, "-l", "-o", byteCodePath, objectPath },
        };

        using (Process process = Process.Start(start))
        {
            using (System.IO.StreamReader reader = process.StandardError)
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}
