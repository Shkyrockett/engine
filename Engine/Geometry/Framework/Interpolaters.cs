using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// Interpolaters
    /// </summary>
    public class Interpolaters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Linear(
            double v1, double v2,
            double t)
            => (1 - t) * v1 + t * v2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> Linear(
            double x1, double y1,
            double x2, double y2,
            double t)
            => new Tuple<double, double>(
                (1 - t) * x1 + t * x2,
                (1 - t) * y1 + t * y2
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Linear(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => new Tuple<double, double, double>(
                (1 - t) * x1 + t * x2,
                (1 - t) * y1 + t * y2,
                (1 - t) * z1 + t * z2
                );
    }
}
