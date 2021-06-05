using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Common
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PieceType
    {
        /// <summary>
        /// <para>■■■■para>
        /// </summary>
        A = 1,
        /// <summary>
        /// <para>□■□</para>
        /// <para>■■■</para>
        /// </summary>
        B = 2,
        /// <summary>
        /// <para>■■</para>
        /// <para>■■</para>
        /// </summary>
        C = 3,
        /// <summary>
        /// <para>□□■</para>
        /// <para>■■■</para>
        /// </summary>
        D = 4,
        /// <summary>
        /// <para>■□□</para>
        /// <para>■■■</para>
        /// </summary>
        E = 5,
        /// <summary>
        /// <para>□■■</para>
        /// <para>■■□</para>
        /// </summary>
        F = 6,
        /// <summary>
        /// <para>■■□</para>
        /// <para>□■■</para>
        /// </summary>
        G = 7
    }
}
