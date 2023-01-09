// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.Globalization;

/// <summary>
/// Provides strings for <typeparamref name="TResourceSource"/>.
/// </summary>
/// <typeparam name="TResourceSource">The <see cref="Type"/> to provide strings for.</typeparam>
public class MultipleStringLocalizer<TResourceSource> : IStringLocalizer<TResourceSource>
{
    private readonly List<IStringLocalizer> _localizers = new();

    /// <summary>
    /// Creates a new <see cref="StringLocalizer{TResourceSource}"/>.
    /// </summary>
    /// <param name="factories">The <see cref="IStringLocalizerFactory"/> to use.</param>
    public MultipleStringLocalizer(IEnumerable<IStringLocalizerFactory> factories)
    {
        if (factories == null)
        {
            throw new ArgumentNullException(nameof(factories));
        }
        foreach (var factory in factories)
        {
            _localizers.Add(factory.Create(typeof(TResourceSource)));
        }
    }

    /// <inheritdoc />
    public virtual LocalizedString this[string name]
    {
        get
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            foreach (var localizer in _localizers)
            {
                var value = localizer[name];
                if (!value.ResourceNotFound)
                    return value;
            }
            return new LocalizedString(name, name);
        }
    }

    /// <inheritdoc />
    public virtual LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            foreach (var localizer in _localizers)
            {
                var value = localizer[name, arguments];
                if (!value.ResourceNotFound)
                    return value;
            }
            return new LocalizedString(name, name);
        }
    }

    /// <inheritdoc />
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        foreach (var localizer in _localizers)
            foreach (var value in localizer.GetAllStrings(includeParentCultures))
                yield return value;
    }
}
