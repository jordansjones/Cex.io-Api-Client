using System;
using System.Linq;

namespace Nextmethod.Cex
{
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
    public class Balance
    {

        public Balance()
        {
            BF1 = SymbolBalance.Zero;
            BTC = SymbolBalance.Zero;
            DVC = SymbolBalance.Zero;
            GHS = SymbolBalance.Zero;
            IXC = SymbolBalance.Zero;
            LTC = SymbolBalance.Zero;
            NMC = SymbolBalance.Zero;
        }

        public SymbolBalance BF1 { get; internal set; }
        
        public SymbolBalance BTC { get; internal set; }

        public SymbolBalance DVC { get; internal set; }

        public SymbolBalance GHS { get; internal set; }

        public SymbolBalance IXC { get; internal set; }

        public SymbolBalance LTC { get; internal set; }

        public SymbolBalance NMC { get; internal set; }

    }
// ReSharper restore InconsistentNaming

    public class SymbolBalance
    {

        public static readonly SymbolBalance Zero = new SymbolBalance(0.0m, 0.0m, 0.0m);

        public SymbolBalance(decimal available, decimal bonus, decimal orders)
        {
            Available = available;
            Bonus = bonus;
            Orders = orders;
        }

        public decimal Available { get; private set; }

        public decimal Bonus { get; private set; }

        public decimal Orders { get; private set; }

    }
// ReSharper restore MemberCanBePrivate.Global
}