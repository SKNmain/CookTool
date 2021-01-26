using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookTool.Client.Shared.Models
{
    public class BarcodeScanResult
    {
        public BarcodeScanResult() { Error = "No Barcode Scanned"; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
