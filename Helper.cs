using System.Text;

namespace ZhukoWaspHwFirst;

public static class Helper
{
    private const          char   EscapeCharacter = (char)0x1B;
    public static readonly string ResetFormatting = $"{EscapeCharacter}[0m";
    public static readonly string EraseLine       = $"{EscapeCharacter}[2K";

    public static string EraseScreenUp(short amount)
    {
        string result = EraseLine;
        while (amount != 0)
        {
            result += $"{EscapeCharacter}[{1}F" + EraseLine;
            --amount;
        }

        return result;
    }

    public static short[] UnpackShorts(this string packed)
    {
        bool negative = packed[0] == '-';
        packed = negative
            ? $"{'0'.Multiply(64 - packed.Substring(1).Length)}{packed.Substring(1)}".ReverseString()
            : $"{'0'.Multiply(64 - packed.Length)}{packed}".ReverseString();
        return new[]
        {
            (short)packed[..16].ReverseString().BasesAnyTo10(2), (short)packed[16..32].ReverseString().BasesAnyTo10(2), (short)packed[32..48].ReverseString().BasesAnyTo10(2), (short)(negative
                ? (short)packed[48..].ReverseString().BasesAnyTo10(2) | unchecked((short)(1 << 15))
                : (short)packed[48..].ReverseString().BasesAnyTo10(2))
        };
    }

    public static string ReverseString(this string original)
    {
        char[] chars = original.ToCharArray();
        Array.Reverse(chars);
        return new String(chars);
    }

    public static Func<int, int, int> Max = static (a, b) => a > b
        ? a
        : b;

    public static Func<int, int, int> Min = static (a, b) => a > b
        ? b
        : a;

    public static long ToPowerOf(this int original, int power)
    {
        if (power == 0) return 1;
        long result                            = original;
        for (int i = 1; i < power; ++i) result *= original;

        return result;
    }

    public static int DigitValue(this char chr) => char.IsDigit(chr)
        ? chr - 48
        : char.IsUpper(chr)
            ? chr - 55
            : char.IsLower(chr)
                ? chr - 87
                : 0;

    public static char ValueToDigit(this byte value) => (char)(value <= 9
        ? 48 + value
        : 55 + value);

    public static char ValueToDigit(this short value) => (char)(value <= 9
        ? 48 + value
        : 55 + value);

    public static char ValueToDigit(this int value) => (char)(value <= 9
        ? 48 + value
        : 55 + value);

    public static char ValueToDigit(this long value) => (char)(value <= 9
        ? 48 + value
        : 55 + value);

    public static long GetBiggestPascalTriangleNumber(int row)
    {
        long divisor = 1, dividend = 1;
        for (int i = 1; i <= row / 2; ++i)
        {
            divisor  *= i;
            dividend *= row - i + 1;
        }

        return dividend / divisor;
    }

    public static long BasesAnyTo10(this string num, int baseFrom, bool upperCase = true)
    {
        int  power    = 0, errorBase = 0;
        long result   = 0;
        bool negative = false;
        if (num[0] == '-')
        {
            num      = num.Substring(1);
            negative = true;
        }

        foreach (char digit in num)
            errorBase = digit.DigitValue() >= baseFrom
                ? Max(digit.DigitValue(), errorBase)
                : errorBase;

        if (errorBase > 0)
            throw new ArgumentException(
                $"'{nameof(baseFrom)}' value entered is too low for the number entered({num}). Should be no less than 2 and at least {errorBase + 1}.");
        foreach (char digit in num.Reverse())
        {
            result += digit.DigitValue() * baseFrom.ToPowerOf(power);
            ++power;
        }

        return negative
            ? result | long.MinValue
            : result;
    }

    public static string Bases10ToAny(this long num, int toBase) //UNUSED
    {
        string result   = "";
        bool   negative = false;
        if (toBase is < 2 or > 36)
            throw new ArgumentException(
                $"{nameof(toBase)}({toBase}) is out of allowed bounds [2; 36].");
        if ($"{num}"[0] == '-') negative = true;
        if (negative)
        {
            ulong unum = Convert.ToUInt64($"{num}".Substring(1));
            while (unum != 0)
            {
                result =  ((byte)(unum % (ulong)toBase)).ValueToDigit() + result;
                unum    /= (ulong)toBase;
            }
        } else
        {
            while (num != 0)
            {
                result =  (num % toBase).ValueToDigit() + result;
                num    /= toBase;
            }
        }

        return negative
            ? '-' + result
            : result;
    }

    public static string Bases10ToAny(this ulong num, int toBase) //UNUSED
    {
        string result = "";
        if (toBase is < 2 or > 36)
            throw new ArgumentException(
                $"{nameof(toBase)}({toBase}) is out of allowed bounds [2; 36].");
        while (num != 0)
        {
            result =  ((byte)(num % (ulong)toBase)).ValueToDigit() + result;
            num    /= (ulong)toBase;
        }

        return result;
    }

    /*
     * TODO: Checking alphanumeric input for specified rules
     */
    /// <summary>
    ///     String extension method <c>Multiply</c> returns passed string, multiplied given amount of times.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// string exampleString = "AB0BA";
    /// exampleString = exampleString.Multiply(2);
    /// ================
    /// </code>
    ///         results in <c>exampleString</c>'s having the value of "AB0BAAB0BA".
    ///     </example>
    /// </summary>
    /// <param name="original">String to multiply</param>
    /// <param name="multiplier">Multiplier</param>
    /// <returns>Multiplied string</returns>
    public static string Multiply(this string original, int multiplier) =>
        multiplier < 0
            ? original
            : new StringBuilder(original.Length * multiplier).Insert(0, original, multiplier).ToString();

    /// <summary>
    ///     Char extension method <c>Multiply</c> returns string containing passed char, multiplied given amount of times.
    ///     <example>
    ///         #1: executing
    ///         <code>
    /// ================
    /// string exampleString = "AB0BA";
    /// exampleString = 'E'.Multiply(10);
    /// ================
    /// </code>
    ///         results in <c>exampleString</c>'s having the value of "EEEEEEEEEE".
    ///     </example>
    /// </summary>
    /// <param name="original">Char to multiply</param>
    /// <param name="multiplier">Multiplier</param>
    /// <returns>Multiplied char in a string</returns>
    public static string Multiply(this char original, int multiplier) => multiplier < 0
        ? $"{original}"
        : new(original, BitOp.AbsI(multiplier));

    public static string GoToLineChar(int line, int chr) => new($"{EscapeCharacter}[{line};{chr}H");

    public static string FormMenu(this string[][] menuItems, int line = 1, int chr = 1)
    {
        //╝╗╔╚╣╩╦╠═║╬
        //menuItems[n].Max(element => element.Length);
        string returnString = "";
        int horizontalIndent = 0,
            maxColumnLength  = menuItems.Max(static item => item.Length);
        for (int i = 0; i < menuItems.Length; ++i)
        {
            int columnWidth    = menuItems[i].Max(static element => element.Length),
                verticalIndent = 0;
            for (int j = 0; j < maxColumnLength; ++j)
            {
                returnString   += $"{GoToLineChar(line + verticalIndent, chr + horizontalIndent)}{(i == 0 ? j == 0 ? "╔" : "╠" : j == 0 ? "╦" : "╬")}{"═".Multiply(columnWidth)}{(j == 0 ? "╗" : "╣").Multiply(i == menuItems.Length - 1 ? 1 : 0)}{GoToLineChar(line + verticalIndent + 1, chr + horizontalIndent)}║{(j >= menuItems[i].Length ? " ".Multiply(columnWidth) : menuItems[i][j] + " ".Multiply(columnWidth - menuItems[i][j].Length))}{"║".Multiply(i == menuItems.Length - 1 ? 1 : 0)}{GoToLineChar(line + verticalIndent + 2, chr + horizontalIndent)}{(i == 0 ? "╚" : "╩")}{"═".Multiply(columnWidth)}{"╝".Multiply(i == menuItems.Length - 1 ? 1 : 0).Multiply(j == maxColumnLength - 1 ? 1 : 0)}";
                verticalIndent += 2;
            }

            horizontalIndent += columnWidth + 1;
        }

        return returnString;
    }
}
