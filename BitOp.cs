namespace ZhukoWaspHwFirst;

/// <summary>
///     Methods of class <c>BitOp</c> perform bitwise operations with a given number.
/// </summary>
public static class BitOp
{
    public static Func<long, long> AbsL = static x => (+1 | (x >> (sizeof(long) * 7 - 1))) * x;

    public static Func<int, int> AbsI = static x => (+1 | (x >> (sizeof(int) * 7 - 1))) * x;

    public static Func<short, short, short, short, long> PackShorts = static (first, second, third, fourth) => (ushort)first | ((long)(ushort)second << 16) | ((long)(ushort)third << 32) | ((long)(ushort)fourth << 48);

    public static Func<long, short[]> UnpackShorts = static packed => new[]
    {
        (short)(packed & 0), (short)(((ulong)packed >> 16) & 0x0), (short)(((ulong)packed >> 32) & 0x0)
        , (short)(((ulong)packed                    >> 48) & 0x0)
    };

    public static string Bases10To2(this long num)
    {
        string result      = "";
        long   mask        = 1L << 63;
        bool   trim = true;
        for (byte i = 0; i < 64; ++i)
        {
            trim   =  trim && (num & mask) == 0;
            result += $"{((num & mask) == 0 ? '0' : '1').Multiply(trim ? 0 : 1)}";
            mask   =  (long)((ulong)mask >> 1);
        }

        return result;
    }
    //public static int ShiftRight
    //public static ulong PackShorts(short first, short second, ushort third, short fourth) => ;

    /// <summary>
    ///     Extension method <c>BitState</c> checks the state of the given byte's bit at the specified position.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// byte exampleByte = 15; // == ..1111
    /// bool exampleBool = BitOp.BitState(exampleByte, 0); // least significant bit checked
    /// ================
    /// </code>
    ///         results in <c>exampleBool</c>'s value being true.
    ///         <code></code>
    ///         #2: executing
    ///         <code>
    /// ================
    /// byte exampleByte = 11; // == ..1011
    /// bool exampleBool = BitOp.BitState(exampleByte, 2); // third least significant bit checked
    /// ================
    /// </code>
    ///         results in <c>exampleBool</c>'s value being false.
    ///     </example>
    /// </summary>
    /// <param name="number">Byte to check.</param>
    /// <param name="bitPosition">Position of the bit to check.</param>
    /// <returns>State of the byte's bit at the given position.</returns>
    public static bool BitState(this byte number, byte bitPosition)
    {
        if (bitPosition > 7)
            throw new ArgumentException($"Entered bitPosition value {bitPosition} is out of allowed bounds [0; 7].");
        return Convert.ToBoolean(number & (1 << bitPosition));
    }

    /// <summary>
    ///     Extension method <c>BitState</c> checks the state of the given ushort's bit at the specified position.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// ushort exampleUshort = 15; // == ..1111
    /// bool exampleBool = BitOp.BitState(exampleUshort, 0); // least significant bit checked
    /// ================
    /// </code>
    ///         results in <c>exampleBool</c>'s value being true.
    ///         <code></code>
    ///         #2: executing
    ///         <code>
    /// ================
    /// ushort exampleUshort = 11; // == ..1011
    /// bool exampleBool = BitOp.BitState(exampleUshort, 2); // third least significant bit checked
    /// ================
    /// </code>
    ///         results in <c>exampleBool</c>'s value being false.
    ///     </example>
    /// </summary>
    /// <param name="number">Ushort to check.</param>
    /// <param name="bitPosition">Position of the bit to check.</param>
    /// <returns>State of the ushort's bit at the given position.</returns>
    public static bool BitState(this ushort number, byte bitPosition)
    {
        if (bitPosition > 15)
            throw new ArgumentException($"Entered bitPosition value {bitPosition} is out of allowed bounds [0; 15].");
        return Convert.ToBoolean(number & (1 << bitPosition));
    }

    /// <summary>
    ///     Extension method <c>BitState</c> checks the state of the given uint's bit at the specified position.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// uint exampleUint = 15; // == ..1111
    /// bool exampleBool = BitOp.BitState(exampleUint, 0); // least significant bit checked
    /// ================
    /// </code>
    ///         results in <c>exampleBool</c>'s value being true.
    ///         <code></code>
    ///         #2: executing
    ///         <code>
    /// ================
    /// uint exampleUint = 11; // == ..1011
    /// bool exampleBool = BitOp.BitState(exampleUint, 2); // third least significant bit checked
    /// ================
    /// </code>
    ///         results in <c>exampleBool</c>'s value being false.
    ///     </example>
    /// </summary>
    /// <param name="number">Uint to check.</param>
    /// <param name="bitPosition">Position of the bit to check.</param>
    /// <returns>State of the uint's bit at the given position.</returns>
    public static bool BitState(this uint number, byte bitPosition)
    {
        if (bitPosition > 31)
            throw new ArgumentException($"Entered bitPosition value {bitPosition} is out of allowed bounds [0; 31].");
        return Convert.ToBoolean(number & (1 << bitPosition));
    }

    /// <summary>
    ///     Extension method <c>BitState</c> checks the state of the given ulong's bit at the specified position.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// ulong exampleUlong = 15; // == ..1111
    /// bool exampleBool = BitOp.BitState(exampleUlong, 0); // least significant bit checked
    /// ================
    /// </code>
    ///         results in <c>exampleBool</c>'s value being true.
    ///         <code></code>
    ///         #2: executing
    ///         <code>
    /// ================
    /// ulong exampleUlong = 11; // == ..1011
    /// bool exampleBool = BitOp.BitState(exampleUlong, 2); // third least significant bit checked
    /// ================
    /// </code>
    ///         results in <c>exampleBool</c>'s value being false.
    ///     </example>
    /// </summary>
    /// <param name="number">Ulong to check.</param>
    /// <param name="bitPosition">Position of the bit to check.</param>
    /// <returns>State of the ulong's bit at the given position.</returns>
    public static bool BitState(this ulong number, byte bitPosition)
    {
        if (bitPosition > 63)
            throw new ArgumentException($"Entered bitPosition value {bitPosition} is out of allowed bounds [0; 63].");
        return Convert.ToBoolean(number & (ulong)(1 << bitPosition));
    }

    /// <summary>
    ///     Extension method <c>Set</c> permutes the given byte by changing the state of the bit at the given position, if
    ///     needed.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// byte exampleByte = 15; // == ..1111
    /// exampleByte = BitOp.Set(exampleByte, 0, 0); // sets least significant bit to 0
    /// ================
    /// </code>
    ///         results in <c>exampleByte</c>'s having the value of 14.
    ///         <code></code>
    ///         #2: executing
    ///         <code>
    /// ================
    /// byte exampleByte = 15; // == ..1111
    /// exampleByte = BitOp.Set(exampleByte, 0, 1); // least significant bit is already 0, nothing happens
    /// ================
    /// </code>
    ///         results in <c>exampleByte</c>'s having the value of 15, changing nothing.
    ///     </example>
    /// </summary>
    /// <param name="number">Byte to permute.</param>
    /// <param name="bitPosition">Position of the bit. [0; 31]</param>
    /// <param name="bitState">State of the bit. [0; 1]</param>
    /// <returns>Permuted byte.</returns>
    public static byte Set(this byte number, byte bitPosition, byte bitState)
    {
        if (bitState > 1)
            throw new ArgumentException($"Entered bitState value {bitState} is out of allowed bounds [0; 1].");
        if (bitPosition > 7)
            throw new ArgumentException($"Entered bitPosition value {bitPosition} is out of allowed bounds [0; 7].");
        return (byte)(Convert.ToBoolean(bitState) == BitState(number, bitPosition)
            ? number
            : bitState == 1
                ? number | (1 << bitPosition)
                : number & ~(1 << bitPosition));
    }

    /// <summary>
    ///     Extension method <c>Set</c> permutes the given ushort by changing the state of the bit at the given position, if
    ///     needed.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// ushort exampleUshort = 15; // == ..1111
    /// exampleUshort = BitOp.Set(exampleUshort, 0, 0); // sets least significant bit to 0
    /// ================
    /// </code>
    ///         results in <c>exampleUshort</c>'s having the value of 14.
    ///         <code></code>
    ///         #2: executing
    ///         <code>
    /// ================
    /// ushort exampleUshort = 15; // == ..1111
    /// exampleUshort = BitOp.Set(exampleUshort, 0, 1); // least significant bit is already 0, nothing happens
    /// ================
    /// </code>
    ///         results in <c>exampleUshort</c>'s having the value of 15, changing nothing.
    ///     </example>
    /// </summary>
    /// <param name="number">Ushort to permute.</param>
    /// <param name="bitPosition">Position of the bit. [0; 31]</param>
    /// <param name="bitState">State of the bit. [0; 1]</param>
    /// <returns>Permuted ushort.</returns>
    public static ushort Set(this ushort number, byte bitPosition, byte bitState)
    {
        if (bitState > 1)
            throw new ArgumentException($"Entered bitState value {bitState} is out of allowed bounds [0; 1].");
        if (bitPosition > 15)
            throw new ArgumentException($"Entered bitPosition value {bitPosition} is out of allowed bounds [0; 15].");
        return (ushort)(Convert.ToBoolean(bitState) == BitState(number, bitPosition)
            ? number
            : bitState == 1
                ? number | (1 << bitPosition)
                : number & ~(1 << bitPosition));
    }

    /// <summary>
    ///     Extension method <c>Set</c> permutes the given uint by changing the state of the bit at the given position, if
    ///     needed.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// uint exampleUint = 15; // == ..1111
    /// exampleUint = BitOp.Set(exampleUint, 0, 0); // sets least significant bit to 0
    /// ================
    /// </code>
    ///         results in <c>exampleUint</c>'s having the value of 14.
    ///         <code></code>
    ///         #2: executing
    ///         <code>
    /// ================
    /// uint exampleUint = 15; // == ..1111
    /// exampleUint = BitOp.Set(exampleUint, 0, 1); // least significant bit is already 0, nothing happens
    /// ================
    /// </code>
    ///         results in <c>exampleUint</c>'s having the value of 15, changing nothing.
    ///     </example>
    /// </summary>
    /// <param name="number">Uint to permute.</param>
    /// <param name="bitPosition">Position of the bit. [0; 31]</param>
    /// <param name="bitState">State of the bit. [0; 1]</param>
    /// <returns>Permuted uint.</returns>
    public static uint Set(this uint number, byte bitPosition, byte bitState)
    {
        if (bitState > 1)
            throw new ArgumentException($"Entered bitState value {bitState} is out of allowed bounds [0; 1].");
        if (bitPosition > 31)
            throw new ArgumentException($"Entered bitPosition value {bitPosition} is out of allowed bounds [0; 31].");
        return Convert.ToBoolean(bitState) == BitState(number, bitPosition)
            ? number
            : (uint)(bitState == 1
                ? number | (uint)(1 << bitPosition)
                : number & ~(1      << bitPosition));
    }

    /// <summary>
    ///     Extension method <c>Set</c> permutes the given ulong by changing the state of the bit at the given position, if
    ///     needed.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// ulong exampleUlong = 15; // == ..1111
    /// exampleUlong = BitOp.Set(exampleUlong, 0, 0); // sets least significant bit to 0
    /// ================
    /// </code>
    ///         results in <c>exampleUlong</c>'s having the value of 14.
    ///         <code></code>
    ///         #2: executing
    ///         <code>
    /// ================
    /// ulong exampleUlong = 15; // == ..1111
    /// exampleUlong = BitOp.Set(exampleUlong, 0, 1); // least significant bit is already 0, nothing happens
    /// ================
    /// </code>
    ///         results in <c>exampleUlong</c>'s having the value of 15, changing nothing.
    ///     </example>
    /// </summary>
    /// <param name="number">Ulong to permute.</param>
    /// <param name="bitPosition">Position of the bit. [0; 31]</param>
    /// <param name="bitState">State of the bit. [0; 1]</param>
    /// <returns>Permuted ulong.</returns>
    public static ulong Set(this ulong number, byte bitPosition, byte bitState)
    {
        if (bitState > 1)
            throw new ArgumentException($"Entered bitState value {bitState} is out of allowed bounds [0; 1].");
        if (bitPosition > 63)
            throw new ArgumentException($"Entered bitPosition value {bitPosition} is out of allowed bounds [0; 63].");
        return Convert.ToBoolean(bitState) == BitState(number, bitPosition)
            ? number
            : bitState == 1
                ? number | (ulong)(1 << bitPosition)
                : number & (ulong)~(1 << bitPosition);
    }
}
