﻿#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public class Label : IEquatable<Label>
    {
        public int Id { get; private set; }

        /// <summary>
        /// The name of this award, certification, seal or regulatory label.
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; internal set; }

        /// <summary>
        /// The products tagged with this award, certification, seal or regulatory label.
        /// </summary>
        public ICollection<Product> Products { get; internal set; } = new HashSet<Product>();

        public Label(string name)
        {
            Name = name;
        }

        public override bool Equals(object? other) => other is Label && Equals(other);

        public bool Equals(Label? other) => Name == other?.Name;

        public static bool operator ==(Label lhs, Label rhs) => lhs.Equals(rhs);
        public static bool operator !=(Label lhs, Label rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Name.GetHashCode();
    }
}
