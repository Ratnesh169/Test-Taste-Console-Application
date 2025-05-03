using System.Globalization;

namespace Test_Taste_Console_Application.Utilities
{
    // This is a simple static utility class that provides culture-specific text formatting capabilities,
    // specifically configured for Belgian Dutch (Flemish) culture.
    public static class CultureInfoUtility
    {
        public static readonly TextInfo TextInfo = new CultureInfo("nl-BE").TextInfo;
    }
}
