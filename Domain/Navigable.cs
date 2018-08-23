﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Navigable : INavigable
    {
        [NotMapped]
        public bool First { get; set; }

        [NotMapped]
        public bool Last { get; set; }

        [NotMapped]
        public bool HasMore { get => !Last; }
    }
}
