// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.Extensions.Globalization;

/// <summary>
/// Provides programmatic configuration for localization.
/// </summary>
public class MultipleLocalizationOptions : LocalizationOptions
{
    /// <summary>
    /// Creates a new <see cref="LocalizationOptions" />.
    /// </summary>
    public MultipleLocalizationOptions()
    { }

    public string FullNameAssembly { get; set; }
}
