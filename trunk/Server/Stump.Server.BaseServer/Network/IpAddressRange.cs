﻿using System;
using System.Net;
using System.Text;
using Stump.Core.Extensions;

namespace Stump.Server.BaseServer.Network
{
    public class IPAddressRange
    {
        public IPAddressRange(IPAddressToken[] tokens)
        {
            if (tokens.Length != 4)
                throw new InvalidOperationException("tokens.Length != 4");

            m_tokens = tokens;
        }

        private IPAddressToken[] m_tokens;

        public IPAddressToken[] Tokens
        {
            get { return m_tokens; }
        }

        public bool Match(string ip)
        {
            return Match(IPAddress.Parse(ip));
        }

        public bool Match(IPAddress ip)
        {
            var bytes = ip.GetAddressBytes();

            if (bytes.Length != Tokens.Length)
                return true;

            for (int i = 0; i < Tokens.Length; i++)
            {
                if (!Tokens[i].Match(bytes[i]))
                    return false;
            }

            return true;
        }

        public static IPAddressRange Parse(string str)
        {
            if (str.CountOccurences('.') != 4)
                throw new FormatException(string.Format("{0} must contains 4 dot", str));

            var split = str.Split('.');
            var tokens = new IPAddressToken[4];

            for (int i = 0; i < split.Length; i++)
            {
                tokens[i] = IPAddressToken.Parse(split[i]);
            }

            return new IPAddressRange(tokens);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Tokens.Length; i++)
            {
                sb.Append(Tokens[i].ToString());

                if (i < 3)
                    sb.Append(".");
            }

            return sb.ToString();
        }
    }

    public class IPAddressToken
    {
        public byte Number;
        public bool Star;
        public Tuple<byte, byte> Range;

        public IPAddressToken(bool star)
        {
            Star = star;
        }

        public IPAddressToken(byte low, byte high)
        {
            Range = Tuple.Create(low, high);
        }

        public IPAddressToken(byte number)
        {
            Number = number;
        }

        public bool Match(byte x)
        {
            if (Star)
                return true;

            if (Range != null)
            {
                return Range.Item1 <= x && x <= Range.Item2;
            }

            return x == Number;
        }

        public static IPAddressToken Parse(string str)
        {
            str = str.Trim();

            if (str == "*")
                return new IPAddressToken(true);

            if (str.Contains("-"))
            {
                var low = byte.Parse(str.Substring(0, str.IndexOf("-")).Trim());
                var high = byte.Parse(str.Remove(0, str.IndexOf("-") + 1).Trim());

                return new IPAddressToken(low, high);
            }

            return new IPAddressToken(byte.Parse(str));
        }

        public override string ToString()
        {

            if (Star)
                return "*";

            if (Range != null)
                return Range.Item1 + "-" + Range.Item2;

            return Number.ToString();
        }
    }
}