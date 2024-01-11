// ------------------------------------------------------------------------------
// <copyright file="POIStatic.cs" company="Rabbb">
// Copyright (c) 2022 Rabbb. All rights reserved.
// Licensed under the MPL-2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable IDE0052, IDE1006, CS0626
#pragma warning disable CA1034, CA1707, CA1716, CA2211,
#pragma warning disable SA1200, SA1201, SA1202, SA1203, SA1300, SA1303, SA1307, SA1310, SA1311, SA1313, SA1314, SA1401, SA1407, SA1507, SA1514, SA1516, SA1600, SA1601, SA1604, SA1611, SA1615, SA1618, SA1623, SA1624, SA1625, SA1626, SA1627, SA1629

using System;

namespace Rabbb.Functional
{
    /// <summary>
    ///  POI Static Helper.
    /// </summary>
    public static class PoiStatic
    {
        /// <summary>
        /// Return a result that is true. <br/>2022-5-9 10:06:55 Ciaran
        /// </summary>
        public static Poi<T, F> True<T, F>(T? @true) => new Poi<T, F>() { Resolve = @true };

        /// <summary>
        /// Return a result that is false.  <br/>2022-5-9 10:06:55 Ciaran
        /// </summary>
        public static Poi<T, F> False<T, F>(F? @false) => new Poi<T, F>() { Reject = @false };

        /// <summary>
        /// Return a result that neither true or false, which is a program exception.  <br/>2022-5-9 10:06:55 Ciaran
        /// </summary>
        public static Poi<T, F> Except<T, F>(Exception ex) => new Poi<T, F>() { Exception = ex };

    }
}