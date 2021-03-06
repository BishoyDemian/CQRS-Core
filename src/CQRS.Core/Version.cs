﻿using System;
using CQRS.Core.Internal;

namespace CQRS.Core
{
    public struct Version: IEquatable<Version>, IComparable<Version>, IComparable
    {
        public Version(int major, int minor, int revision)
        {
            Ensure.Range(() => major, 0, int.MaxValue);
            Ensure.Range(() => minor, 0, int.MaxValue);
            Ensure.Range(() => revision, 0, int.MaxValue);

            Major = major;
            Minor = minor;
            Revision = revision;
        }

        public Version(int major, int minor) : this(major, minor, default(int))
        {
        }

        public int Major { get; }

        public int Minor { get; }

        public int Revision { get; }


        public override string ToString()
        {
            return $"{Major}.{Minor}.{Revision}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Major;
                hashCode = (hashCode * 397) ^ Minor;
                hashCode = (hashCode * 397) ^ Revision;
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is Version && Equals((Version)obj);
        }

        public bool Equals(Version other)
        {
            return other.Major == Major && other.Minor == Minor && other.Revision == Revision;
        }

        public int CompareTo(Version other)
        {
            var result = Major.CompareTo(other.Major);

            if (result != 0)
                return result;

            result = Minor.CompareTo(other.Minor);

            if (result != 0)
                return result;

            return Revision.CompareTo(other.Revision);
        }

        public int CompareTo(object obj)
        {
            var version = obj as Version?;
            return version != null ? CompareTo(version.Value) : -1;
        }



        // Equality operator. Returns null if either operand is null, 
        // otherwise returns dbTrue or dbFalse:
        public static bool operator ==(Version a, Version b)
        {
            return a.Equals(b);
        }

        // Inequality operator. Returns dbNull if either operand is
        // dbNull, otherwise returns dbTrue or dbFalse:
        public static bool operator !=(Version a, Version b)
        {
            return !a.Equals(b);
        }

        // Overload the conversion to string:
        public static implicit operator string(Version v)
        {
            return v.ToString();
        }
    }
}
