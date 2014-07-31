﻿#region Copyright Notice
// ------------------------------------------------------------------------------ //
//                                                                                //
//               All contents copyright � Edward Nutting 2014                     //
//                                                                                //
//        You may not share, reuse, redistribute or otherwise use the             //
//        contents this file outside of the Fling OS project without              //
//        the express permission of Edward Nutting or other copyright             //
//        holder. Any changes (including but not limited to additions,            //
//        edits or subtractions) made to or from this document are not            //
//        your copyright. They are the copyright of the main copyright            //
//        holder for all Fling OS files. At the time of writing, this             //
//        owner was Edward Nutting. To be clear, owner(s) do not include          //
//        developers, contributors or other project members.                      //
//                                                                                //
// ------------------------------------------------------------------------------ //
#endregion
    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Kernel.Compiler.ILOps
{
    /// <summary>
    /// Base class for IL op classes that are used to convert from IL code to ASM.
    /// </summary>
    /// <remarks>
    /// <para> 
    /// Some IL OP implementations may throw specific exceptions so make sure they are 
    /// handled cleanly and outputted to the user.
    /// </para>
    /// </remarks>
    public abstract class ILOp
    {
        /// <summary>
        /// The full list of supported Il ops. Please see remarks for important info.
        /// </summary>
        /// <remarks>
        /// Enum values here = System.Reflection.OpCode.Value - they can be used interchangeably.
        /// </remarks>
        public enum OpCodes : ushort
        {
            #region Values
            /// <summary> - </summary>
            /// <remarks></remarks>
            Nop = 0x0000,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Break = 0x0001,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldarg_0 = 0x0002,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldarg_1 = 0x0003,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldarg_2 = 0x0004,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldarg_3 = 0x0005,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldloc_0 = 0x0006,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldloc_1 = 0x0007,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldloc_2 = 0x0008,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldloc_3 = 0x0009,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stloc_0 = 0x000A,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stloc_1 = 0x000B,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stloc_2 = 0x000C,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stloc_3 = 0x000D,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldarg_S = 0x000E,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldarga_S = 0x000F,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Starg_S = 0x0010,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldloc_S = 0x0011,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldloca_S = 0x0012,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stloc_S = 0x0013,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldnull = 0x0014,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_M1 = 0x0015,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_0 = 0x0016,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_1 = 0x0017,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_2 = 0x0018,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_3 = 0x0019,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_4 = 0x001A,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_5 = 0x001B,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_6 = 0x001C,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_7 = 0x001D,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_8 = 0x001E,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4_S = 0x001F,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I4 = 0x0020,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_I8 = 0x0021,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_R4 = 0x0022,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldc_R8 = 0x0023,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Dup = 0x0025,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Pop = 0x0026,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Jmp = 0x0027,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Call = 0x0028,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Calli = 0x0029,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ret = 0x002A,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Br_S = 0x002B,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Brfalse_S = 0x002C,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Brtrue_S = 0x002D,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Beq_S = 0x002E,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bge_S = 0x002F,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bgt_S = 0x0030,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ble_S = 0x0031,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Blt_S = 0x0032,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bne_Un_S = 0x0033,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bge_Un_S = 0x0034,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bgt_Un_S = 0x0035,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ble_Un_S = 0x0036,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Blt_Un_S = 0x0037,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Br = 0x0038,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Brfalse = 0x0039,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Brtrue = 0x003A,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Beq = 0x003B,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bge = 0x003C,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bgt = 0x003D,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ble = 0x003E,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Blt = 0x003F,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bne_Un = 0x0040,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bge_Un = 0x0041,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Bgt_Un = 0x0042,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ble_Un = 0x0043,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Blt_Un = 0x0044,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Switch = 0x0045,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_I1 = 0x0046,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_U1 = 0x0047,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_I2 = 0x0048,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_U2 = 0x0049,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_I4 = 0x004A,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_U4 = 0x004B,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_I8 = 0x004C,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_I = 0x004D,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_R4 = 0x004E,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_R8 = 0x004F,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldind_Ref = 0x0050,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stind_Ref = 0x0051,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stind_I1 = 0x0052,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stind_I2 = 0x0053,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stind_I4 = 0x0054,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stind_I8 = 0x0055,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stind_R4 = 0x0056,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stind_R8 = 0x0057,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Add = 0x0058,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Sub = 0x0059,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Mul = 0x005A,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Div = 0x005B,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Div_Un = 0x005C,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Rem = 0x005D,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Rem_Un = 0x005E,
            /// <summary> - </summary>
            /// <remarks></remarks>
            And = 0x005F,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Or = 0x0060,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Xor = 0x0061,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Shl = 0x0062,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Shr = 0x0063,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Shr_Un = 0x0064,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Neg = 0x0065,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Not = 0x0066,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_I1 = 0x0067,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_I2 = 0x0068,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_I4 = 0x0069,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_I8 = 0x006A,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_R4 = 0x006B,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_R8 = 0x006C,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_U4 = 0x006D,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_U8 = 0x006E,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Callvirt = 0x006F,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Cpobj = 0x0070,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldobj = 0x0071,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldstr = 0x0072,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Newobj = 0x0073,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Castclass = 0x0074,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Isinst = 0x0075,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_R_Un = 0x0076,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Unbox = 0x0079,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Throw = 0x007A,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldfld = 0x007B,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldflda = 0x007C,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stfld = 0x007D,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldsfld = 0x007E,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Ldsflda = 0x007F,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stsfld = 0x0080,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Stobj = 0x0081,
            /// <summary> - </summary>
            /// <remarks></remarks>
            Conv_Ovf_I1_Un = 0x0082,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I2_Un = 0x0083,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I4_Un = 0x0084,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I8_Un = 0x0085,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U1_Un = 0x0086,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U2_Un = 0x0087,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U4_Un = 0x0088,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U8_Un = 0x0089,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I_Un = 0x008A,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U_Un = 0x008B,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Box = 0x008C,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Newarr = 0x008D,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldlen = 0x008E,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelema = 0x008F,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_I1 = 0x0090,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_U1 = 0x0091,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_I2 = 0x0092,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_U2 = 0x0093,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_I4 = 0x0094,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_U4 = 0x0095,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_I8 = 0x0096,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_I = 0x0097,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_R4 = 0x0098,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_R8 = 0x0099,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem_Ref = 0x009A,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem_I = 0x009B,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem_I1 = 0x009C,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem_I2 = 0x009D,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem_I4 = 0x009E,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem_I8 = 0x009F,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem_R4 = 0x00A0,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem_R8 = 0x00A1,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem_Ref = 0x00A2,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldelem = 0x00A3,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stelem = 0x00A4,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Unbox_Any = 0x00A5,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I1 = 0x00B3,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U1 = 0x00B4,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I2 = 0x00B5,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U2 = 0x00B6,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I4 = 0x00B7,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U4 = 0x00B8,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I8 = 0x00B9,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U8 = 0x00BA,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Refanyval = 0x00C2,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ckfinite = 0x00C3,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Mkrefany = 0x00C6,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldtoken = 0x00D0,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_U2 = 0x00D1,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_U1 = 0x00D2,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_I = 0x00D3,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_I = 0x00D4,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_Ovf_U = 0x00D5,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Add_Ovf = 0x00D6,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Add_Ovf_Un = 0x00D7,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Mul_Ovf = 0x00D8,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Mul_Ovf_Un = 0x00D9,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Sub_Ovf = 0x00DA,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Sub_Ovf_Un = 0x00DB,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Endfinally = 0x00DC,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Leave = 0x00DD,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Leave_S = 0x00DE,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stind_I = 0x00DF,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Conv_U = 0x00E0,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Prefix7 = 0x00F8,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Prefix6 = 0x00F9,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Prefix5 = 0x00FA,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Prefix4 = 0x00FB,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Prefix3 = 0x00FC,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Prefix2 = 0x00FD,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Prefix1 = 0x00FE,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Prefixref = 0x00FF,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Arglist = 0xFE00,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ceq = 0xFE01,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Cgt = 0xFE02,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Cgt_Un = 0xFE03,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Clt = 0xFE04,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Clt_Un = 0xFE05,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldftn = 0xFE06,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldvirtftn = 0xFE07,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldarg = 0xFE09,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldarga = 0xFE0A,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Starg = 0xFE0B,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldloc = 0xFE0C,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Ldloca = 0xFE0D,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Stloc = 0xFE0E,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Localloc = 0xFE0F,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Endfilter = 0xFE11,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Unaligned = 0xFE12,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Volatile = 0xFE13,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Tailcall = 0xFE14,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Initobj = 0xFE15,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Constrained = 0xFE16,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Cpblk = 0xFE17,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Initblk = 0xFE18,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Rethrow = 0xFE1A,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Sizeof = 0xFE1C,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Refanytype = 0xFE1D,
            /// <summary> - </summary>
            /// <remarks></remarks> 
            Readonly = 0xFE1E
            #endregion
        }

        /// <summary>
        /// Converts the IL op into assembly code.
        /// </summary>
        /// <param name="anILOpInfo">The ILOpInfo for the specific conversion.</param>
        /// <param name="aScannerState">The current scanner state.</param>
        /// <returns>ASM code in a string or null if the conversion failed.</returns>
        public abstract string Convert(ILOpInfo anILOpInfo, ILScannerState aScannerState);
    }
}
