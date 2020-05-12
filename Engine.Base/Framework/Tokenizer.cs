// <copyright file="Tokenizer.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The tokenizer class.
    /// </summary>
    /// <remarks>
    /// <para>Based on TokenizerHelper from: http://referencesource.microsoft.com</para>
    /// </remarks>
    public class Tokenizer
    {
        #region Fields
        /// <summary>
        /// The quote char.
        /// </summary>
        private readonly char quoteChar;

        /// <summary>
        /// The arg separator.
        /// </summary>
        private readonly char argSeparator;

        /// <summary>
        /// The str.
        /// </summary>
        private readonly char[] str;

        /// <summary>
        /// The str len.
        /// </summary>
        private readonly int strLen;

        /// <summary>
        /// The char index.
        /// </summary>
        private int charIndex;

        /// <summary>
        /// The current token index.
        /// </summary>
        internal int currentTokenIndex;

        /// <summary>
        /// The current token length.
        /// </summary>
        internal int currentTokenLength;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Constructor for TokenizerHelper which accepts an IFormatProvider.
        /// If the IFormatProvider is null, we use the thread's IFormatProvider info.
        /// We will use ',' as the list separator, unless it's the same as the
        /// decimal separator.  If it *is*, then we can't determine if, say, "23,5" is one
        /// number or two.  In this case, we will use ";" as the separator.
        /// </summary>
        /// <param name="str">The string which will be tokenized.</param>
        /// <param name="formatProvider">The IFormatProvider which controls this tokenization.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tokenizer(string str, IFormatProvider formatProvider)
            : this(str, '\'', GetNumericListSeparator(formatProvider))
        { }

        /// <summary>
        /// Initialize the TokenizerHelper with the string to tokenize,
        /// the char which represents quotes and the list separator.
        /// </summary>
        /// <param name="str">The string to tokenize.</param>
        /// <param name="quoteChar">The quote char.</param>
        /// <param name="separator">The list separator.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tokenizer(string str, char quoteChar, char separator)
        {
            this.str = str?.ToCharArray();
            strLen = this.str.Length;
            currentTokenIndex = -1;
            this.quoteChar = quoteChar;
            argSeparator = separator;

            // Skip past any whitespace so NextToken() logic always starts on the first character of the next token.
            while (charIndex < strLen)
            {
                if (!char.IsWhiteSpace(this.str[charIndex]))
                {
                    break;
                }

                ++charIndex;
            }
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        public bool FoundSeparator { get; private set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Get the current token.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetCurrentToken() => currentTokenIndex < 0 ? string.Empty : new Span<char>(str).Slice(currentTokenIndex, currentTokenLength).ToString();

        /// <summary>
        /// Throws an exception if there is any non-whitespace left unparsed.
        /// </summary>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void LastTokenRequired()
        {
            if (charIndex != strLen)
            {
                throw new InvalidOperationException("Extra data encountered. There are non-whitespace characters remaining unparsed.");
            }
        }

        /// <summary>
        /// Advances to the NextToken
        /// </summary>
        /// <returns>
        /// true if next token was found, false if at end of string
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NextToken() => NextToken(false);

        /// <summary>
        /// Advances to the NextToken
        /// </summary>
        /// <param name="allowQuotedToken">if set to <see langword="true" /> [allow quoted token].</param>
        /// <returns>
        /// true if next token was found, false if at end of string
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NextToken(bool allowQuotedToken) => NextToken(allowQuotedToken, argSeparator);

        /// <summary>
        /// Advances to the NextToken.  A separator character can be specified
        /// which overrides the one previously set.
        /// </summary>
        /// <param name="allowQuotedToken">if set to <see langword="true" /> [allow quoted token].</param>
        /// <param name="separator">The separator.</param>
        /// <returns>
        /// true if next token was found, false if at end of string
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool NextToken(bool allowQuotedToken, char separator)
        {
            // Set the side effects.
            currentTokenIndex = -1; // reset the currentTokenIndex.
            FoundSeparator = false; // reset.

            // If we're at end of the string, just return false.
            if (charIndex >= strLen)
            {
                return false;
            }

            var currentChar = str[charIndex];
            Debug.Assert(!char.IsWhiteSpace(currentChar), "Token started on Whitespace");

            // Setup the quoteCount.
            var quoteCount = 0;

            // If we are allowing a quoted token and this token begins with a quote, set up the quote count and skip the initial quote.
            if (allowQuotedToken && currentChar == quoteChar)
            {
                quoteCount++; // Increment quote count.
                ++charIndex; // Move to next character.
            }

            var newTokenIndex = charIndex;
            var newTokenLength = 0;

            // Loop until hit end of string or hit a , or whitespace if at end of string just return false.
            while (charIndex < strLen)
            {
                currentChar = str[charIndex];

                // If have a QuoteCount and this is a quote decrement the quoteCount.
                if (quoteCount > 0)
                {
                    // If anything but a quoteChar we move on
                    if (currentChar == quoteChar)
                    {
                        --quoteCount;

                        // If at zero which it always should for now break out of the loop
                        if (0 == quoteCount)
                        {
                            ++charIndex; // Move past the quote.
                            break;
                        }
                    }
                }
                else if (char.IsWhiteSpace(currentChar) || (currentChar == separator))
                {
                    FoundSeparator |= currentChar == separator;
                    break;
                }

                ++charIndex;
                ++newTokenLength;
            }

            // If quoteCount isn't zero we hit the end of the string before the ending quote.
            if (quoteCount > 0)
            {
                throw new InvalidOperationException("Missing end quote while parsing.");
            }

            ScanToNextToken(separator); // Move so at the start of the nextToken for next call.

            // Finally made it, update the currentToken values.
            currentTokenIndex = newTokenIndex;
            currentTokenLength = newTokenLength;

            if (currentTokenLength < 1)
            {
                throw new InvalidOperationException("Encountered an empty token while parsing.");
            }

            return true;
        }

        /// <summary>
        /// Advances to the NextToken, throwing an exception if not present
        /// </summary>
        /// <param name="allowQuotedToken">if set to <see langword="true" /> [allow quoted token].</param>
        /// <returns>
        /// The next token found
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string NextTokenRequired(bool allowQuotedToken = false) => NextToken(allowQuotedToken) ? GetCurrentToken() : throw new InvalidOperationException("Premature string termination encountered while advancing to next token.");

        /// <summary>
        /// helper to move the _charIndex to the next token or to the end of the string
        /// </summary>
        /// <param name="separator">The separator.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ScanToNextToken(char separator)
        {
            // If already at end of the string don't bother
            if (charIndex >= strLen)
            {
                return;
            }

            var currentChar = str[charIndex];

            // Check whether the current Char is a space or a separator. If not either, we have an error. This can happen if the char after the quotes string isn't a char.
            if (!(currentChar == separator) && !char.IsWhiteSpace(currentChar))
            {
                throw new InvalidOperationException("Extra data encountered while parsing. Expected a separator or white space.");
            }

            // Loop until hit a character that isn't an argument separator or whitespace.
            // !ToDo: if more than one argSet; throw an exception
            var argSepCount = 0;
            while (charIndex < strLen)
            {
                currentChar = str[charIndex];

                if (currentChar == separator)
                {
                    FoundSeparator = true;
                    ++argSepCount;
                    charIndex++;

                    if (argSepCount > 1)
                    {
                        throw new InvalidOperationException("An empty token was encountered while parsing.");
                    }
                }
                else if (char.IsWhiteSpace(currentChar))
                {
                    ++charIndex;
                }
                else
                {
                    break;
                }
            }

            // If there was a separatorChar then we shouldn't be at the end of string, or it means there was a separator but there isn't an argument.
            if (argSepCount > 0 && charIndex >= strLen)
            {
                throw new InvalidOperationException("An empty token was encountered while parsing.");
            }
        }

        /// <summary>
        /// Helper to get the numeric list separator for a given IFormatProvider.
        /// Separator is a comma [,] if the decimal separator is not a comma, or a semicolon [;] otherwise.
        /// </summary>
        /// <param name="formatProvider">The provider.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char GetNumericListSeparator(IFormatProvider formatProvider)
        {
            var numericListSeparator = ',';

            // Get the NumberFormatInfo out of the provider, if possible.
            // If the IFormatProvider doesn't contain a NumberFormatInfo, then this method returns the current culture's NumberFormatInfo.
            var numberDecimalSeparator = NumberFormatInfo.GetInstance(formatProvider)?.NumberDecimalSeparator;

            // If the decimal separator is the same as the list separator, we use the ";".
            if ((numberDecimalSeparator?.Length > 0) && (numericListSeparator == numberDecimalSeparator?[0]))
            {
                numericListSeparator = ';';
            }

            return numericListSeparator;
        }
        #endregion Methods
    }
}
