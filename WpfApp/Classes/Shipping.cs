//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp.Classes
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shipping
    {
        public int ShipCode { get; set; }
        public string Berth { get; set; }
        public string VisitPurpose { get; set; }
        public Nullable<int> Shipid { get; set; }
    
        public virtual Ship Ship { get; set; }
    }
}