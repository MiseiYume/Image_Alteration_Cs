//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kernel {
    using System.IO;
    using OpenCL.Net;
    using OpenCL.Net.Extensions;
    
    
    public class imagingTest : OpenCL.Net.Extensions.KernelWrapperBase {
        
        public imagingTest(OpenCL.Net.Context context) : 
                base(context) {
        }
        
        protected override string KernelPath {
            get {
                return System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "kernel.cl");
            }
        }
        
        protected override string OriginalKernelPath {
            get {
                return "M:\\Projekty\\Csharp\\RGBT\\kernel.cl";
            }
        }
        
        protected override string KernelSource {
            get {
                return @"__kernel void imagingTest(__read_only  image2d_t srcImg,__write_only image2d_t dstImg)
{
    const sampler_t smp = CLK_NORMALIZED_COORDS_FALSE | 
        CLK_ADDRESS_CLAMP_TO_EDGE | 
        CLK_FILTER_LINEAR;
    int2 coord = (int2)(get_global_id(0), get_global_id(1));
    uint4 bgra = read_imageui(srcImg, smp, coord); 
    float4 bgrafloat = convert_float4(bgra) / 255.0f; 
    
    float luminance = sqrt(0.241f * bgrafloat.z * bgrafloat.z + 0.691f *
        bgrafloat.y * bgrafloat.y + 0.068f * bgrafloat.x * bgrafloat.x);
    bgra.x = bgra.y = bgra.z = (uint) (luminance * 255.0f);
    bgra.w = 255;
    write_imageui(dstImg, coord, bgra);
}";
            }
        }
        
        protected override string KernelName {
            get {
                return "imagingTest";
            }
        }
        
        private OpenCL.Net.Event run(OpenCL.Net.CommandQueue commandQueue, OpenCL.Net.IMem srcImg, OpenCL.Net.IMem dstImg, uint globalWorkSize0, uint globalWorkSize1 = 0, uint globalWorkSize2 = 0, uint localWorkSize0 = 0, uint localWorkSize1 = 0, uint localWorkSize2 = 0, params OpenCL.Net.Event[] waitFor) {
            OpenCL.Net.Cl.SetKernelArg(this.Kernel, 0, srcImg);
            OpenCL.Net.Cl.SetKernelArg(this.Kernel, 1, dstImg);
            OpenCL.Net.Event ev;
            OpenCL.Net.ErrorCode err;
            err = OpenCL.Net.Cl.EnqueueNDRangeKernel(commandQueue, this.Kernel, base.GetWorkDimension(globalWorkSize0, globalWorkSize1, globalWorkSize2), null, base.GetWorkSizes(globalWorkSize0, globalWorkSize1, globalWorkSize2), base.GetWorkSizes(localWorkSize0, localWorkSize1, localWorkSize2), ((uint)(waitFor.Length)), waitFor.Length == 0 ? null : waitFor, out ev);
            OpenCL.Net.Cl.Check(err);
            return ev;
        }
        
        public void Run(OpenCL.Net.CommandQueue commandQueue, OpenCL.Net.IMem srcImg, OpenCL.Net.IMem dstImg, uint globalWorkSize, uint localWorkSize = 0, params OpenCL.Net.Event[] waitFor) {
            OpenCL.Net.Event ev = this.run(commandQueue, srcImg, dstImg, globalWorkSize0: globalWorkSize, localWorkSize0: localWorkSize, waitFor: waitFor);
            ev.Wait();
        }
        
        public OpenCL.Net.Event EnqueueRun(OpenCL.Net.CommandQueue commandQueue, OpenCL.Net.IMem srcImg, OpenCL.Net.IMem dstImg, uint globalWorkSize, uint localWorkSize = 0, params OpenCL.Net.Event[] waitFor) {
            return this.run(commandQueue, srcImg, dstImg, globalWorkSize0: globalWorkSize, localWorkSize0: localWorkSize, waitFor: waitFor);
        }
        
        public void Run(OpenCL.Net.CommandQueue commandQueue, OpenCL.Net.IMem srcImg, OpenCL.Net.IMem dstImg, uint globalWorkSize0, uint globalWorkSize1, uint localWorkSize0 = 0, uint localWorkSize1 = 0, params OpenCL.Net.Event[] waitFor) {
            OpenCL.Net.Event ev = this.run(commandQueue, srcImg, dstImg, globalWorkSize0: globalWorkSize0, globalWorkSize1: globalWorkSize1, localWorkSize0: localWorkSize0, localWorkSize1: localWorkSize1, waitFor: waitFor);
            ev.Wait();
        }
        
        public OpenCL.Net.Event EnqueueRun(OpenCL.Net.CommandQueue commandQueue, OpenCL.Net.IMem srcImg, OpenCL.Net.IMem dstImg, uint globalWorkSize0, uint globalWorkSize1, uint localWorkSize0 = 0, uint localWorkSize1 = 0, params OpenCL.Net.Event[] waitFor) {
            return this.run(commandQueue, srcImg, dstImg, globalWorkSize0: globalWorkSize0, globalWorkSize1: globalWorkSize1, localWorkSize0: localWorkSize0, localWorkSize1: localWorkSize1, waitFor: waitFor);
        }
        
        public void Run(OpenCL.Net.CommandQueue commandQueue, OpenCL.Net.IMem srcImg, OpenCL.Net.IMem dstImg, uint globalWorkSize0, uint globalWorkSize1, uint globalWorkSize2, uint localWorkSize0 = 0, uint localWorkSize1 = 0, uint localWorkSize2 = 0, params OpenCL.Net.Event[] waitFor) {
            OpenCL.Net.Event ev = this.run(commandQueue, srcImg, dstImg, globalWorkSize0: globalWorkSize0, globalWorkSize1: globalWorkSize1, globalWorkSize2: globalWorkSize2, localWorkSize0: localWorkSize0, localWorkSize1: localWorkSize1, localWorkSize2: localWorkSize2, waitFor: waitFor);
            ev.Wait();
        }
        
        public OpenCL.Net.Event EnqueueRun(OpenCL.Net.CommandQueue commandQueue, OpenCL.Net.IMem srcImg, OpenCL.Net.IMem dstImg, uint globalWorkSize0, uint globalWorkSize1, uint globalWorkSize2, uint localWorkSize0 = 0, uint localWorkSize1 = 0, uint localWorkSize2 = 0, params OpenCL.Net.Event[] waitFor) {
            return this.run(commandQueue, srcImg, dstImg, globalWorkSize0: globalWorkSize0, globalWorkSize1: globalWorkSize1, globalWorkSize2: globalWorkSize2, localWorkSize0: localWorkSize0, localWorkSize1: localWorkSize1, localWorkSize2: localWorkSize2, waitFor: waitFor);
        }
    }
}
