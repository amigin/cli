﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DotNet.Cli.Utils.IA
{
    // Stupid-simple console manager
    internal class Reporter
    {
        private static readonly Reporter NullReporter = new Reporter(console: null);
        private static object _lock = new object();

        private readonly AnsiConsole _console;

        static Reporter()
        {
            Reset();
        }

        private Reporter(AnsiConsole console)
        {
            _console = console;
        }

        public static Reporter Output { get; private set; }
        public static Reporter Error { get; private set; }
        public static Reporter Verbose { get; private set; }

        /// <summary>
        /// Resets the Reporters to write to the current Console Out/Error.
        /// </summary>
        public static void Reset()
        {
            lock (_lock)
            {
                Output = new Reporter(AnsiConsole.GetOutput());
                Error = new Reporter(AnsiConsole.GetError());
                Verbose = IsVerbose ?
                    new Reporter(AnsiConsole.GetOutput()) :
                    NullReporter;
            }
        }

        public static bool IsVerbose => true;

        public void WriteLine(string message)
        {
            lock (_lock)
            {       _console?.WriteLine(message);
            }
        }

        public void WriteLine()
        {
            lock (_lock)
            {
                _console?.Writer?.WriteLine();
            }
        }

        public void Write(string message)
        {
            lock (_lock)
            {
                    _console?.Write(message);
            }
        }
    }
}
