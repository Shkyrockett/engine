﻿using System;
using System.Diagnostics;
using System.Globalization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Based on TokenizerHelper from: http://referencesource.microsoft.com
    /// </remarks>
    internal class Tokenizer
    {
        /// <summary>
        /// 
        /// </summary>
        char quoteChar;

        /// <summary>
        /// 
        /// </summary>
        char argSeparator;

        /// <summary>
        /// 
        /// </summary>
        string str;

        /// <summary>
        /// 
        /// </summary>
        int strLen;

        /// <summary>
        /// 
        /// </summary>
        int charIndex;

        /// <summary>
        /// 
        /// </summary>
        internal int currentTokenIndex;

        /// <summary>
        /// 
        /// </summary>
        internal int currentTokenLength;

        /// <summary>
        /// 
        /// </summary>
        bool foundSeparator;

        /// <summary>
        /// Constructor for TokenizerHelper which accepts an IFormatProvider.
        /// If the IFormatProvider is null, we use the thread's IFormatProvider info.
        /// We will use ',' as the list separator, unless it's the same as the
        /// decimal separator.  If it *is*, then we can't determine if, say, "23,5" is one
        /// number or two.  In this case, we will use ";" as the separator.
        /// </summary>
        /// <param name="str"> The string which will be tokenized. </param>
        /// <param name="formatProvider"> The IFormatProvider which controls this tokenization. </param>
        internal Tokenizer(string str, IFormatProvider formatProvider)
        {
            char numberSeparator = GetNumericListSeparator(formatProvider);

            Initialize(str, '\'', numberSeparator);
        }

        /// <summary>
        /// Initialize the TokenizerHelper with the string to tokenize,
        /// the char which represents quotes and the list separator.
        /// </summary>
        /// <param name="str"> The string to tokenize. </param>
        /// <param name="quoteChar"> The quote char. </param>
        /// <param name="separator"> The list separator. </param>
        internal Tokenizer(string str, char quoteChar, char separator)
        {
            Initialize(str, quoteChar, separator);
        }

        /// <summary>
        /// 
        /// </summary>
        internal bool FoundSeparator
        {
            get { return foundSeparator; }
        }

        /// <summary>
        /// Initialize the TokenizerHelper with the string to tokenize,
        /// the char which represents quotes and the list separator.
        /// </summary>
        /// <param name="str"> The string to tokenize. </param>
        /// <param name="quoteChar"> The quote char. </param>
        /// <param name="separator"> The list separator. </param>
        private void Initialize(string str, char quoteChar, char separator)
        {
            this.str = str;
            strLen = str == null ? 0 : str.Length;
            currentTokenIndex = -1;
            this.quoteChar = quoteChar;
            argSeparator = separator;

            // immediately forward past any whitespace so
            // NextToken() logic always starts on the first
            // character of the next token.
            while (charIndex < strLen)
            {
                if (!char.IsWhiteSpace(this.str, charIndex))
                {
                    break;
                }

                ++charIndex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal string GetCurrentToken()
        {
            // if no current token, return null
            if (currentTokenIndex < 0)
            {
                return null;
            }

            return str.Substring(currentTokenIndex, currentTokenLength);
        }

        /// <summary>
        /// Throws an exception if there is any non-whitespace left unparsed.
        /// </summary>
        internal void LastTokenRequired()
        {
            if (charIndex != strLen)
            {
                //throw new System.InvalidOperationException(SR.Get(SRID.TokenizerHelperExtraDataEncountered, _charIndex, _str));
                throw new System.InvalidOperationException();
            }
        }

        /// <summary>
        /// Advances to the NextToken
        /// </summary>
        /// <returns>true if next token was found, false if at end of string</returns>
        internal bool NextToken()
        {
            return NextToken(false);
        }

        /// <summary>
        /// Advances to the NextToken, throwing an exception if not present
        /// </summary>
        /// <returns>The next token found</returns>
        internal string NextTokenRequired()
        {
            if (!NextToken(false))
            {
                //throw new System.InvalidOperationException(SR.Get(SRID.TokenizerHelperPrematureStringTermination, _str));
                throw new System.InvalidOperationException();
            }

            return GetCurrentToken();
        }

        /// <summary>
        /// Advances to the NextToken, throwing an exception if not present
        /// </summary>
        /// <returns>The next token found</returns>
        internal string NextTokenRequired(bool allowQuotedToken)
        {
            if (!NextToken(allowQuotedToken))
            {
                //throw new System.InvalidOperationException(SR.Get(SRID.TokenizerHelperPrematureStringTermination, _str));
                throw new System.InvalidOperationException();
            }

            return GetCurrentToken();
        }

        /// <summary>
        /// Advances to the NextToken
        /// </summary>
        /// <returns>true if next token was found, false if at end of string</returns>
        internal bool NextToken(bool allowQuotedToken)
        {
            // use the currently-set separator character.
            return NextToken(allowQuotedToken, argSeparator);
        }

        /// <summary>
        /// Advances to the NextToken.  A separator character can be specified
        /// which overrides the one previously set.
        /// </summary>
        /// <returns>true if next token was found, false if at end of string</returns>
        internal bool NextToken(bool allowQuotedToken, char separator)
        {
            currentTokenIndex = -1; // reset the currentTokenIndex
            foundSeparator = false; // reset

            // If we're at end of the string, just return false.
            if (charIndex >= strLen)
            {
                return false;
            }

            char currentChar = str[charIndex];

            Debug.Assert(!Char.IsWhiteSpace(currentChar), "Token started on Whitespace");

            // setup the quoteCount
            int quoteCount = 0;

            // If we are allowing a quoted token and this token begins with a quote,
            // set up the quote count and skip the initial quote
            if (allowQuotedToken &&
                currentChar == quoteChar)
            {
                quoteCount++; // increment quote count
                ++charIndex; // move to next character
            }

            int newTokenIndex = charIndex;
            int newTokenLength = 0;

            // loop until hit end of string or hit a , or whitespace
            // if at end of string just return false.
            while (charIndex < strLen)
            {
                currentChar = str[charIndex];

                // if have a QuoteCount and this is a quote
                // decrement the quoteCount
                if (quoteCount > 0)
                {
                    // if anything but a quoteChar we move on
                    if (currentChar == quoteChar)
                    {
                        --quoteCount;

                        // if at zero which it always should for now
                        // break out of the loop
                        if (0 == quoteCount)
                        {
                            ++charIndex; // move past the quote
                            break;
                        }
                    }
                }
                else if ((Char.IsWhiteSpace(currentChar)) || (currentChar == separator))
                {
                    if (currentChar == separator)
                    {
                        foundSeparator = true;
                    }
                    break;
                }

                ++charIndex;
                ++newTokenLength;
            }

            // if quoteCount isn't zero we hit the end of the string
            // before the ending quote
            if (quoteCount > 0)
            {
                //throw new System.InvalidOperationException(SR.Get(SRID.TokenizerHelperMissingEndQuote, _str));
                throw new System.InvalidOperationException();
            }

            ScanToNextToken(separator); // move so at the start of the nextToken for next call

            // finally made it, update the _currentToken values
            currentTokenIndex = newTokenIndex;
            currentTokenLength = newTokenLength;

            if (currentTokenLength < 1)
            {
                //throw new System.InvalidOperationException(SR.Get(SRID.TokenizerHelperEmptyToken, _charIndex, _str));
                throw new System.InvalidOperationException();
            }

            return true;
        }

        /// <summary>
        /// helper to move the _charIndex to the next token or to the end of the string
        /// </summary>
        /// <param name="separator"></param>
        void ScanToNextToken(char separator)
        {
            // if already at end of the string don't bother
            if (charIndex < strLen)
            {
                char currentChar = str[charIndex];

                // check that the currentChar is a space or the separator.  If not
                // we have an error. this can happen in the quote case
                // that the char after the quotes string isn't a char.
                if (!(currentChar == separator) &&
                    !Char.IsWhiteSpace(currentChar))
                {
                    //throw new System.InvalidOperationException(SR.Get(SRID.TokenizerHelperExtraDataEncountered, _charIndex, _str));
                    throw new System.InvalidOperationException();
                }

                // loop until hit a character that isn't
                // an argument separator or whitespace.
                // !!!ToDo: if more than one argSet throw an exception
                int argSepCount = 0;
                while (charIndex < strLen)
                {
                    currentChar = str[charIndex];

                    if (currentChar == separator)
                    {
                        foundSeparator = true;
                        ++argSepCount;
                        charIndex++;

                        if (argSepCount > 1)
                        {
                            //throw new System.InvalidOperationException(SR.Get(SRID.TokenizerHelperEmptyToken, _charIndex, _str));
                            throw new System.InvalidOperationException();
                        }
                    }
                    else if (Char.IsWhiteSpace(currentChar))
                    {
                        ++charIndex;
                    }
                    else
                    {
                        break;
                    }
                }

                // if there was a separatorChar then we shouldn't be
                // at the end of string or means there was a separator
                // but there isn't an argument

                if (argSepCount > 0 && charIndex >= strLen)
                {
                    //throw new System.InvalidOperationException(SR.Get(SRID.TokenizerHelperEmptyToken, _charIndex, _str));
                    throw new System.InvalidOperationException();
                }
            }
        }

        // Helper to get the numeric list separator for a given IFormatProvider.
        // Separator is a comma [,] if the decimal separator is not a comma, or a semicolon [;] otherwise.
        public static char GetNumericListSeparator(IFormatProvider provider)
        {
            char numericSeparator = ',';

            // Get the NumberFormatInfo out of the provider, if possible
            // If the IFormatProvider doesn't not contain a NumberFormatInfo, then
            // this method returns the current culture's NumberFormatInfo.
            NumberFormatInfo numberFormat = NumberFormatInfo.GetInstance(provider);

            Debug.Assert(null != numberFormat);

            // Is the decimal separator the same as the list separator?
            // If so, we use the ";".
            if ((numberFormat.NumberDecimalSeparator.Length > 0) && (numericSeparator == numberFormat.NumberDecimalSeparator[0]))
            {
                numericSeparator = ';';
            }

            return numericSeparator;
        }
    }
}