using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Assignment
{
    internal class Global
    {
        public static readonly string Directory = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), "document");
    }
}
