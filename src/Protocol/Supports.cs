using System;
using Newtonsoft.Json;

namespace OmniSharp.Extensions.LanguageServer.Protocol
{
    public struct Supports<T> : ISupports
    {
        private readonly bool? _isSupported;
        public Supports(bool? isSupported, T value)
        {
            _isSupported = isSupported;
            Value = value;
        }

        public Supports(bool? isSupported)
        {
            _isSupported = isSupported;
            Value = default(T);
        }

        public T Value { get; set; }
        public bool IsSupported => _isSupported ?? false;
        public Type ValueType => typeof(T);
        object ISupports.Value => Value;

        public static implicit operator T(Supports<T> value)
        {
            return value.Value;
        }

        public static implicit operator Supports<T>(T value)
        {
            return new Supports<T>(true, value);
        }
    }

    public static class Supports
    {
        public static Supports<T> OfValue<T>(T value)
            where T : class
        {
            return new Supports<T>(value != null, value);
        }

        public static Supports<T> OfBoolean<T>(bool? isSupported)
        {
            return new Supports<T>(isSupported);
        }
    }
}
