using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace InvoiceApp.Helpers
{
    public class OutWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public Action<char> WriteChar
        {
            get => _writeChar ?? (c => Debug.Write(c));
            set => _writeChar = value;
        }
        Action<char>? _writeChar;

        public Action<string?> WriteString
        {
            get => _writeString ?? (s => Debug.Write(s));
            set => _writeString = value;
        }
        Action<string?>? _writeString;

        public override void Write(char value) => WriteChar(value);
        public override void Write(string? value) => WriteString(value);

        public OutWriter(Action<char> writeChar, Action<string?> writeString)
        {
            WriteChar = writeChar;
            WriteString = writeString;
        }
    }
}
