﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Entities
{
    public class PricedItem
    {
        public string ClientId { get; set; }
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string TypeLine { get; set; }
        public int FrameType { get; set; }
        public decimal Calculated { get; set; }
        public bool Elder { get; set; }
        public bool Shaper { get; set; }
        public string Icon { get; set; }
        public int Ilvl { get; set; }
        public int Tier { get; set; }
        public bool Corrupted { get; set; }
        public int Links { get; set; }
        public int Sockets { get; set; }
        public int Quality { get; set; }
        public int Level { get; set; }
        public int Stacksize { get; set; }
        public int TotalStacksize { get; set; }
        public string Variant { get; set; }
        public decimal Total { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Mean { get; set; }
        public decimal Mode { get; set; }
        public decimal Median { get; set; }
        public string BaseType { get; set; }
        public int Count { get; set; }
        public virtual Stashtab Stashtab { get; set; }
        public virtual int StashtabId { get; set; }
    }
}
