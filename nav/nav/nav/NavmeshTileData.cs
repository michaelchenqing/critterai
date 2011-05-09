﻿/*
 * Copyright (c) 2011 Stephen A. Pratt
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using System.Runtime.InteropServices;
using org.critterai.nav.rcn;

namespace org.critterai.nav
{
    /// <summary>
    /// Represents the raw data buffer for a navigation mesh tile.
    /// </summary>
    /// <remarks>
    /// This </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class NavmeshTileData
    {
        /*
         * Design notes:
         * 
         * It is critical that the data always be allocated by the
         * external library.  This is because, in the vast majority of
         * cases, ownership of the data will be transferred to a navmesh.
         * The navmesh will then be responble for freeing the memory, which
         * can't be done if the data was allocated on this side of the interop
         * boundary.
         * 
         */

        private IntPtr mData = IntPtr.Zero;
        private int mDataLength = 0;
        private bool mIsOwned = false;

        public int Length { get { return mDataLength; } }
        public bool IsOwned { get { return mIsOwned; } }

        public NavmeshTileData(NavmeshTileBuildData buildData)
        {
            NavmeshTileEx.BuildTileData(buildData, this);
        }

        public NavmeshTileData(byte[] rawTileData)
        {
            if (rawTileData == null)
                return;
            NavmeshTileEx.BuildTileData(rawTileData
                , rawTileData.Length
                , this);
        }

        ~NavmeshTileData()
        {
            if (!mIsOwned && mData != IntPtr.Zero)
                NavmeshTileEx.FreeEx(this);
            mData = IntPtr.Zero;
        }

        public byte[] GetData()
        {
            if (mData == IntPtr.Zero)
                return null;

            byte[] result = new byte[mDataLength];
            Marshal.Copy(mData, result, 0, mDataLength);
            return result;
        }
    }
}
