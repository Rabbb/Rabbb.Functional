// ------------------------------------------------------------------------------
// <copyright file="Poi.cs" company="Rabbb">
// Copyright (c) 2022 Rabbb. All rights reserved.
// Licensed under the MPL-2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable IDE0052, IDE1006, CS0626
#pragma warning disable CA1034, CA1707, CA1716, CA2211,
#pragma warning disable SA1107, SA1124, SA1200, SA1201, SA1202, SA1203, SA1300, SA1303, SA1307, SA1310, SA1311, SA1313, SA1314, SA1401, SA1407, SA1500, SA1503, SA1507, SA1514, SA1516, SA1520, SA1600, SA1601, SA1604, SA1611, SA1615, SA1618, SA1623, SA1624, SA1625, SA1626, SA1627, SA1629

using System;
using static Rabbb.Functional.PoiStatic;

namespace Rabbb.Functional
{
    /// <summary>
    /// An function result that has true result when succeed, or false result when failed. <br/>
    /// 2022-5-9 10:17:17 Ciaran
    /// </summary>
    /// <typeparam name="T">True result type.</typeparam>
    /// <typeparam name="F">False result type.</typeparam>
    public class Poi<T, F>
    {
        private bool bSolve = false;
        private T? resolve;

        public bool IsOk => this.bSolve;

        public bool IsExcept => this.Exception != null;

        public PoiType Type => this.IsOk ? PoiType.True : (this.IsExcept ? PoiType.Except : PoiType.False);

        /// <summary>True result.</summary>
        public T? Resolve
        {
            get => this.resolve;
            set
            {
                this.resolve = value;
                this.bSolve = true;
            }
        }

        /// <summary>False result.</summary>
        public F? Reject { get; set; }

        /// <summary>
        /// Neither True result or False result, it's a program exception. <br/>2022-5-9 10:17:17 Ciaran
        /// </summary>
        public Exception? @Exception { get; set; }

        #region Then

        /// <summary>
        /// Do something, and then return current POI result. <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T, F> Then(POIAction<T, F> action)
        {
            if (this.Exception is null)
                action!(this);
            return this;
        }

        /// <summary>
        /// Handle current POI result, and then return a same POI type result. <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T, F> Then(POIFunc<T, F> func)
        {
            if (this.Exception is null)
                return func!(this) ?? @False<T, F>(default(F?));
            return this;
        }

        /// <summary>
        /// Handle current POI result, and then return a different POI type result. <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T1, F1> Then<T1, F1>(POIFunc<T, F, T1, F1> func)
        {
            if (this.Exception is null)
                return func!(this) ?? @False<T1, F1>(default(F1?));
            return Except<T1, F1>(this.Exception);
        }


        /// <summary>
        /// Do something, and then return current POI result. <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T, F> Then(Action<T?> trueAct, Action<F?> falseAct)
        {
            if (this.Exception is null)
            {
                if (this.bSolve) trueAct!(this.Resolve);
                else falseAct!(this.Reject);
            }

            return this;
        }

        /// <summary>
        /// Handle current POI result, and then return a same POI type result. <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T, F> Then(TrueHandle<T, F> trueFunc, FalseHandle<T, F> falseFunc)
        {
            if (this.Exception is null)
            {
                if (this.bSolve) return trueFunc!(this.Resolve) ?? @False<T, F>(default(F?));
                else return falseFunc!(this.Reject) ?? @False<T, F>(default(F?));
            }

            return this;
        }

        /// <summary>
        /// Handle current POI result, and then return a different POI type result. <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T1, F1> Then<T1, F1>(ConvertHandle<T, T1, F1> trueFunc, ConvertHandle<F, T1, F1> falseFunc)
        {
            if (this.Exception is not null) return Except<T1, F1>(this.Exception) ?? @False<T1, F1>(default(F1?));
            if (this.bSolve) return trueFunc!(this.Resolve) ?? @False<T1, F1>(default(F1?));
            return falseFunc!(this.Reject) ?? @False<T1, F1>(default(F1?));
        }

        /// <summary>
        /// Handle current POI result, and then return a different POI type result. <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T1, F1> Then<T1, F1>(ConvertHandle<T, T1, F1> trueFunc, ConvertHandle<F, T1, F1> falseFunc, ExceptionHandle<T1, F1> exceptionHandle)
        {
            if (this.Exception is not null) return exceptionHandle(this.Exception) ?? Except<T1, F1>(this.Exception);
            if (this.bSolve) return trueFunc!(this.Resolve) ?? @False<T1, F1>(default(F1?));
            return falseFunc!(this.Reject) ?? @False<T1, F1>(default(F1?));
        }

        #endregion

        #region Catch

        /// <summary>
        /// Catch a exception result in the POI chain, then do something. Finally return current POI result.
        /// <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T, F> Catch(Action<Exception> action)
        {
            if (!(this.Exception is null))
            {
                action(this.Exception);
            }

            return this;
        }

        /// <summary>
        /// Catch a exception result in the POI chain, then do something, and then return a new same POI type result.
        /// <br/>Or return current POI result directly.
        /// <br/>2022-5-22 05:39:29 Ciaran
        /// </summary>
        public Poi<T, F> Catch(ExceptionHandle<T, F> func)
        {
            if (!(this.Exception is null))
            {
                return func!(this.Exception) ?? @False<T, F>(default(F?));
            }

            return this;
        }

        #endregion
    }

    #region delegate types

    /// <summary>
    /// A action that do something with last POI result. <br/>2022-5-22 05:39:29 Ciaran
    /// </summary>
    /// <typeparam name="T">true result type.</typeparam>
    /// <typeparam name="F">false result type.</typeparam>
    /// <param name="last_result">Last POI result.</param>
    public delegate void POIAction<T, F>(Poi<T, F> last_result);

    /// <summary>
    /// A function that return a POI result. <br/>2022-5-22 05:39:29 Ciaran
    /// </summary>
    /// <typeparam name="T">true result type.</typeparam>
    /// <typeparam name="F">false result type.</typeparam>
    /// <param name="last_result">Last POI result.</param>
    /// <returns>a POI result.</returns>
    public delegate Poi<T, F> POIFunc<T, F>(Poi<T, F> last_result);

    /// <summary>
    /// A function that return a POI result. <br/>2022-5-22 05:39:29 Ciaran
    /// </summary>
    /// <typeparam name="T">Last true result type.</typeparam>
    /// <typeparam name="F">Last false result type.</typeparam>
    /// <typeparam name="T1">New true result type.</typeparam>
    /// <typeparam name="F1">New false result type.</typeparam>
    /// <param name="last_result">Last POI result.</param>
    /// <returns>a POI result.</returns>
    public delegate Poi<T1, F1> POIFunc<T, F, T1, F1>(Poi<T, F> last_result);

    /// <summary>
    /// Handle true result value, and then provide a same POI type result. <br/>2022-5-22 05:39:29 Ciaran
    /// </summary>
    public delegate Poi<T, F> TrueHandle<T, F>(T? resolve);

    /// <summary>
    /// Handle false result value, and then provide a same POI type result. <br/>2022-5-22 05:39:29 Ciaran
    /// </summary>
    public delegate Poi<T, F> FalseHandle<T, F>(F? reject);

    /// <summary>
    /// Handle exception result, and then return a same POI type result. <br/>2022-5-22 05:39:29 Ciaran
    /// </summary>
    public delegate Poi<T, F> ExceptionHandle<T, F>(Exception ex);

    /// <summary>
    /// Handle true result value, and then provide a different POI type result. <br/>2022-5-22 05:39:29 Ciaran
    /// </summary>
    public delegate Poi<T1, F1> ConvertHandle<in T, T1, F1>(T? resolve);

    #endregion
}