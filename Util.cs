using System.Linq;

namespace LISPClosureValidator
{
        public static class Util {
        private const char _leftParen = '(';
        private const char _rightParen = ')';
        private const int _closingParenFail = -1;

        public static char[] ConvertCodeStringToCharArray(string codeString) {
            var codeCharArray = codeString.ToCharArray();
            var parenArray = codeCharArray.Where(c => c == _leftParen || c == _rightParen).ToArray();
            return parenArray;
        }

        public static bool ClosureValid(char[] parens) {
            bool innerCloserValid = true;
            bool postclosureValid = true;
            //failure test
            if (parens == null || parens.Length < 2) {
                return false;
            }
            if (parens != null && parens.Length > 0 && parens[0] != _leftParen) {
                return false;
            }
            //successful test
            if (parens != null && parens.Length == 2 && parens[0] == _leftParen && parens[1] == _rightParen) {
                return true;
            }

            //test for equal number of parenthesis as a shortcut before longer calculations
            var openParens = parens.Where(p => p == _leftParen).Count();
            var closingParens = parens.Where(p => p == _rightParen).Count();
            if (openParens != closingParens) {
                return false;
            }

            //find closing paren index for starting paren
            int closingIndex = ClosingParenIndex(parens);

            //return fail if closing index is not found
            if (closingIndex == _closingParenFail) {
                return false;
            }

            //if there are parens inside this closure, return their closure validity
            if (closingIndex > 1) {
                char[] innerParenClosure = parens.Skip(1).Take(closingIndex - 1).ToArray();                
                innerCloserValid = ClosureValid(innerParenClosure);
            }

            //if there are parens after this closure, return their closure validity
            if (closingIndex < parens.Length - 1) {
                char[] postParenClosure = parens.Skip(closingIndex + 1).Take(parens.Length - (closingIndex + 1)).ToArray();                
                postclosureValid = ClosureValid(postParenClosure);
            }

            return innerCloserValid && postclosureValid;
        }

        public static int ClosingParenIndex(char[] parens) {
            //failure test
            if (parens == null || parens.Length < 2) {
                return _closingParenFail;
            }

            int openParenCount = 1;
            int index = 0;
            while(openParenCount > 0 && index < parens.Length - 1) {
                index++;
                if (parens[index] == _leftParen) {
                    openParenCount ++;
                }
                if (parens[index] == _rightParen) {
                    openParenCount --;
                }
            }
            if (openParenCount == 0) {
                return index;
            }
            else {
                return _closingParenFail;
            }
        }
    }
}