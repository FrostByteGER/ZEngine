using System;
using System.Collections.Generic;

namespace ZEngine.Engine.ECS
{
    public struct EGID : IEquatable<EGID>, IEqualityComparer<EGID>, IComparable<EGID>
    {
        private readonly long _gid;

        public int EntityId => (int) (_gid & 0xFFFFFFFF);

        public static bool operator ==(EGID obj1, EGID obj2)
        {
            return obj1._gid == obj2._gid;
        }

        public static bool operator !=(EGID obj1, EGID obj2)
        {
            return obj1._gid != obj2._gid;
        }

        public static explicit operator int(EGID id)
        {
            return id.EntityId;
        }

        public static explicit operator long(EGID id)
        {
            return id._gid;
        }

        public EGID(int entityId, int groupId) : this()
        {
            _gid = MakeGlobalId(entityId, groupId);
        }

        internal EGID(int entityId, int groupId) : this()
        {
            _gid = MakeGlobalId(entityId, groupId);
        }

        private static long MakeGlobalId(int entityId, int groupId)
        {
            return (long) groupId << 32 | ((long) (uint) entityId & 0xFFFFFFFF);
        }

        public bool Equals(EGID other)
        {
            return _gid == other._gid;
        }

        public bool Equals(EGID x, EGID y)
        {
            return x == y;
        }

        public int GetHashCode(EGID obj)
        {
            return _gid.GetHashCode();
        }

        public int CompareTo(EGID other)
        {
            return _gid.CompareTo(other._gid);
        }
    }
}