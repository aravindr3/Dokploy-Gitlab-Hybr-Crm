using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HyBrForex.Infrastructure.Persistence.Settings;

public class UlidToStringConverter : ValueConverter<Ulid, string>
{
    public UlidToStringConverter()
        : base(
            ulid => Ulid.NewUlid().ToString(), // Convert Ulid to string
            str => Ulid.Parse(str)) // Convert string back to Ulid
    {
    }
}