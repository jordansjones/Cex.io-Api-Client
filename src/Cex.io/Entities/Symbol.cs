using System;
using System.Collections.Generic;
using System.Linq;

namespace Nextmethod.Cex
{
// ReSharper disable InconsistentNaming
    public enum Symbol
    {
        BF1,
        BTC,    
        DVC,
        GHS,
        IXC,
        LTC,
        NMC,
        USD,
        EUR,
		ETH
    }

    
    public sealed class SymbolPair
    {
        
        public static readonly SymbolPair GHS_NMC = new SymbolPair(Symbol.GHS, Symbol.NMC);
        public static readonly SymbolPair BTC_EUR = new SymbolPair(Symbol.BTC, Symbol.EUR);

		#region USD markets

		public static readonly SymbolPair ETH_USD = new SymbolPair(Symbol.ETH, Symbol.USD);
		public static readonly SymbolPair BTC_USD = new SymbolPair(Symbol.BTC, Symbol.USD);

		#endregion

		#region BTC markets

		public static readonly SymbolPair ETH_BTC = new SymbolPair(Symbol.ETH, Symbol.BTC);
		public static readonly SymbolPair BF1_BTC = new SymbolPair(Symbol.BF1, Symbol.BTC);
		public static readonly SymbolPair LTC_BTC = new SymbolPair(Symbol.LTC, Symbol.BTC);
		public static readonly SymbolPair NMC_BTC = new SymbolPair(Symbol.NMC, Symbol.BTC);
		public static readonly SymbolPair GHS_BTC = new SymbolPair(Symbol.GHS, Symbol.BTC);

		#endregion

		private SymbolPair(KeyValuePair<Symbol, Symbol> pair)
            : this(pair.Key, pair.Value)
        {}

        private SymbolPair(Symbol @from, Symbol to)
        {
            From = @from;
            To = to;
        }

        public Symbol From { get; private set; }

        public Symbol To { get; private set; }

        public bool Equals(SymbolPair other)
        {
            return From == other.From && To == other.To;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != this.GetType()) { return false; }
            return Equals((SymbolPair) obj);
        }

        public override int GetHashCode()
        {
            unchecked { return ((int) From * 397) ^ (int) To; }
        }

        public static bool operator ==(SymbolPair left, SymbolPair right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SymbolPair left, SymbolPair right)
        {
            return !Equals(left, right);
        }

    }

// ReSharper restore InconsistentNaming


    internal static class SymbolExtensions
    {

        public static string Name(this Symbol sym)
        {
            return sym.ToString();
        }

    }

}
