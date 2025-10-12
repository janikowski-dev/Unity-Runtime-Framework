using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Internal.Runtime.Utilities
{
    public static class VariablesProcessor
    {
        private static Dictionary<string, string> _variables = new();
        
        public static string GetProcessedText(string text) => ProcessToLower(ProcessVariables(text));

        public static void Override(string key, string value) => OverrideInternal(key, value);

        private static string ProcessVariables(string input)
        {
            return Regex.Replace(input, @"\{(\w+)\}", match =>
            {
                return GetValue(GetInnerValue(match));
            });
        }
        
        private static string ProcessToLower(string input)
        {
            return Regex.Replace(input, @"ToLower\((.*?)\)", match =>
            {
                return GetInnerValue(match).ToLower();
            });
        }

        private static string GetInnerValue(Match match)
        {
            return match.Groups[1].Value;
        }

        private static string GetValue(string key)
        {
            bool hasKey = _variables.ContainsKey(key);
            return hasKey ? _variables[key] : key;
        }

        private static void OverrideInternal(string key, string value)
        {
            _variables[key] = value;
        }
    }
}