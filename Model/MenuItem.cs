using System;

namespace IBAS_kantine.Model
{
    public record MenuItemDTO 
    {
        public string? Dag { get; set; }
        public string? KoldRet { get; set; }
        public string? VarmRet { get; set; }
        public int? Uge { get; set; }
        
    }
}