using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Mvvm.Calculator.Model
{
    public class Numbers
    {
        public decimal? Number1 { get; set; }
        public decimal? Number2 { get; set; }

        private decimal? _numberResult;

        public decimal? NumberResult
        {
            get => _numberResult;
        }

        public decimal? Sub()
        {
            _numberResult = Number1 - Number2;
            return _numberResult;
        }

        public decimal? Add()
        {
            _numberResult = Number1 + Number2;
            return _numberResult;
        }

    }
}
