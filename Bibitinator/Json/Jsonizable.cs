using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibitinator.Json
{
    public interface Jsonizable
    {
        // implementing classes should (probably) have this constructor:
        // Jsonizable(string json, int startIndex) { }
        string ToJson();
    }
}
