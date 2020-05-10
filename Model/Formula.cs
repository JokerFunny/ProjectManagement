﻿using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Model for Formula
    /// </summary>
    public class Formula
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<Guid, int> MaterialsWithPercentQuantity { get; set; }

        public decimal WeightInGramms { get; set; }

        public Guid CreatedBy { get; set; }

        public List<Guid> SharedWith { get; set; }
    }
}
