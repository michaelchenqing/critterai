﻿using System.Runtime.InteropServices;

namespace org.critterai.nav
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NavmeshTileHeader
    {
        public int magic;
        public int version;

        /// <summary>
        /// The x-position of the tile within the tile grid. (x, z)
        /// </summary>
        public int x;

        /// <summary>
        /// The z-position of the tile within the tile grid. (x, z)
        /// </summary>
        public int z;

        /// <summary>
        /// The layer of the tile.
        /// </summary>
        /// <remarks>
        /// Layering occurs on the y-axis. (Height)
        /// </remarks>
        public int layer;

        /// <summary>
        /// The user assigned id of the tile.
        /// </summary>
        public uint userId;

        /// <summary>
        /// The number of polygons in the tile.
        /// </summary>
        public int polyCount;

        /// <summary>
        /// The number of polygon vertices in the tile. 
        /// </summary>
        public int vertCount;

        /// <summary>
        /// The number of links allocated. 
        /// </summary>
        public int maxLinkCount;

        /// <summary>
        /// The number of detail meshes.
        /// </summary>
        public int detailMeshCount;

        /// <summary>
        /// The number of unique detail vertices.  (In addition to the
        /// polygon vertices.)
        /// </summary>
        public int detailVertCount;

        /// <summary>
        /// The number of detail triangles.
        /// </summary>
        public int detailTriCount;

        /// <summary>
        /// The number of bounding volume nodes.
        /// </summary>
        public int bvNodeCount;

        /// <summary>
        /// The number of off-mesh connections.
        /// </summary>
        public int connCount;

        /// <summary>
        /// The index of the first polygon which is an off-mesh connection.
        /// </summary>
        public int connBase;

        /// <summary>
        /// The designed minimum floor to 'ceiling' height that will still 
        /// allow the floor area to be considered traversable.
        /// </summary>
        public float walkableHeight;

        /// <summary>
        /// Represents the closest any part of a mesh gets to an
        /// obstruction in the source geometry. (Usually the client radius.)
        /// </summary>
        public float walkableRadius;

        /// <summary>
        /// The designed maximum ledge height that is considered to still be
        /// traversable. 
        /// </summary>
        public float walkableStep;

        /// <summary>
        /// The minimum bounds of the tile's AABB in the form (x, y, z).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] boundsMin;

        /// <summary>
        /// The maximum bounds of the tile's AABB in the form (x, y, z).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] boundsMax;

        /// <summary>
        /// The bounding volumn quantization factor. (For converting from world
        /// to bounding volumn coordinates.)
        /// </summary>
        public float bvQuantFactor;
    }
}
