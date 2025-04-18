﻿using System;
using System.ComponentModel;

namespace Win32Exception_CS;

static class Class1
{
    static void Main()
    {
        //<snippet1>
        try
        {
            System.Diagnostics.Process myProc = new();
            myProc.StartInfo.FileName = @"c:\nonexist.exe"; // Attempt to start a non-existent executable
            _ = myProc.Start();
        }
        catch (Win32Exception w)
        {
            Console.WriteLine(w.Message);
            Console.WriteLine(w.ErrorCode.ToString());
            Console.WriteLine(w.NativeErrorCode.ToString());
            Console.WriteLine(w.StackTrace);
            Console.WriteLine(w.Source);
            Exception e = w.GetBaseException();
            Console.WriteLine(e.Message);
        }
        //</snippet1>
    }
}
