using System;
using System.Collections.Generic;
using PostgreSQL_Editor.Global;

namespace PostgreSQL_Editor.Models
{
    public class Module : product
    {
        public decimal holeDiameterFrom { get; set; }
        public decimal holeDiameterTo { get; set; }
        public bool is_filler { get; set; }
        public bool is_oversize_module { get; set; }
        public int max_cable_capacity { get; set; }
        public int GroupCode { get; set; }
        public int PackingQty { get; set; }
        public int Package { get; set; }
        public string PackagingUnit { get; set; }
        public List<Variation> variations { get; set; }
        public string packagingInfo
        {
            get
            {
                if (PackingQty > 0)
                    return $"{PackingQty} pkg {Package} unit(s) with {PackagingUnit} pc(s";
                else
                    return "No packaging info.";
            }
        }
    }

    public class Variation
    {
        public int lfnr { get; set; }
        public Guid id { get; set; }
        public decimal holeDiameterFrom { get; set; }
        public decimal holeDiameterTo { get; set; }
        public decimal color { get; set; }
        public string colorHex
        {
            get
            {
                int intColor = (int)color;
                return String.Format("#{0:X8}", intColor);
            }
        }
        public override string ToString()
        {
            return ("min: " + holeDiameterFrom.ToString() + ", max: " + holeDiameterTo.ToString() + ", Color: " + colorHex);
        }
    }
}
