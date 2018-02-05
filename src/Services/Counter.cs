using System;
using System.Collections.Generic;
using System.Text;

namespace Aiursoft.Pylon.Services
{
    public class Counter
    {
        private readonly object obj = new object();
        public int _Counted = 0;
        public int GetUniqueNo
        {
            get
            {
                lock (obj)
                {
                    _Counted++;
                }
                return _Counted;
            }
        }
    }
}
