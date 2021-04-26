using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using OpenCL.Net;
using RGBT;

namespace RGBT
{
    class Program
    {
        static void Main(string[] args)
        {
            SettingUp Setup1 = new SettingUp();
            Setup1.Setup();
            Console.WriteLine("Provide an input with the image to process:\n");

        }
    }
}
