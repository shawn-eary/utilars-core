﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace efModels
{
    public partial class License
    {
        public License()
        {
            ArtPiece = new HashSet<ArtPiece>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<ArtPiece> ArtPiece { get; set; }
    }
}