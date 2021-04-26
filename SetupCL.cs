using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Utilities;
using OpenCL.Net;

namespace RGBT
{
    class SettingUp
    {
        private Cl.Context _context;
        private Cl.Device _device;

        private void CheckErr(Cl.ErrorCode err, string name)
        {
            if (err != Cl.ErrorCode.Success)
            {
                Console.WriteLine("ERROR: " + name + " (" + err.ToString() + ")");
            }
        }
        private void ContextNotify(string errInfo, byte[] data, IntPtr cb, IntPtr userData)
        {
            Console.WriteLine("OpenCL Notification: " + errInfo);
        }

        public void Setup()
        {
            Cl.ErrorCode error;
            Cl.Platform[] platforms = Cl.GetPlatformIDs(out error);
            List<Cl.Device> devicesList = new List<Cl.Device>();

            CheckErr(error, "Cl.GetPlatformIDs");

            foreach (Cl.Platform platform in platforms)
            {
                string platformName = Cl.GetPlatformInfo(platform, Cl.PlatformInfo.Name, out error).ToString();
                Console.WriteLine("Platform: " + platformName);
                CheckErr(error, "Cl.GetPlatformInfo");
                //We will be looking only for GPU devices
                foreach (Cl.Device device in Cl.GetDeviceIDs(platform, Cl.DeviceType.Gpu, out error))
                {
                    CheckErr(error, "Cl.GetDeviceIDs");
                    Console.WriteLine("Device: " + device.ToString());
                    devicesList.Add(device);
                }
            }

            if (devicesList.Count <= 0)
            {
                Console.WriteLine("No devices found.");
                return;
            }

            _device = devicesList[0];

            if (Cl.GetDeviceInfo(_device, Cl.DeviceInfo.ImageSupport, out error).CastTo<Cl.Bool>() == Cl.Bool.False)
            {
                Console.WriteLine("No image support.");
                return;
            }
            _context = Cl.CreateContext(null, 1, new[] { _device }, ContextNotify, IntPtr.Zero, out error);    //Second parameter is amount of devices
            CheckErr(error, "Cl.CreateContext");
        }

        static string IsPrime
        {
            get
            {
                return @"
                kernel void GetIfPrime(global int* message)
                {
                    int index = get_global_id(0);

                    int upperl=(int)sqrt((float)message[index]);
                    for(int i=2;i<=upperl;i++)
                    {
                        if(message[index]%i==0)
                        {
                            //printf("" %d / %d\n"",index,i );
                            message[index]=0;
                            return;
                        }
                    }
                    //printf("" % d"",index);
                }";
            }
        }


        static void Run(string[] args)
        {

            int[] Primes = Enumerable.Range(2, 1000000).ToArray();
            EasyCL cl = new EasyCL();
            cl.Accelerator = Accelerator.Gpu;        //You can also set the accelerator after loading the kernel
            cl.LoadKernel(IsPrime);                  //Load kernel string here, (Compiles in the background)
            cl.Invoke("GetIfPrime", Primes.Length, Primes); //Call Function By Name With Parameters
                                                            //Primes now contains all Prime Numbers
        }
    }
}
