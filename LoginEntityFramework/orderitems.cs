//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoginEntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class orderitems
    {
        public int orderid { get; set; }
        public string item { get; set; }
        public int qty { get; set; }
        public int price { get; set; }
    
        public virtual items items { get; set; }
        public virtual orders orders { get; set; }
    }
}
