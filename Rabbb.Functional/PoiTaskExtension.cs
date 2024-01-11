// ------------------------------------------------------------------------------
// <copyright file="PoiTaskExtension.cs" company="Rabbb">
// Copyright (c) 2022 Rabbb. All rights reserved.
// Licensed under the MPL-2.0 license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Rabbb.Functional
{
    public static class PoiTaskExtension
    {
        #region sync

        public static PoiTask<Poi<T1, F1>> Bind<T, F, T1, F1>(this PoiTask<Poi<T, F>> task, ConvertHandle<T, T1, F1> trueHandle, ConvertHandle<F, T1, F1> failHandle, ExceptionHandle<T1, F1> exceptionHandle)
            => new PoiTask<Poi<T1, F1>>(() => task.Result.Then(trueHandle, failHandle, exceptionHandle));

        public static PoiTask<Poi<T1, F1>> Bind<T, F, T1, F1>(this Task<Poi<T, F>> task, ConvertHandle<T, T1, F1> trueHandle, ConvertHandle<F, T1, F1> failHandle, ExceptionHandle<T1, F1> exceptionHandle)
            => new PoiTask<Poi<T1, F1>>(() => task.Result.Then(trueHandle, failHandle, exceptionHandle));

        public static PoiTask<Poi<T1, F1>> Bind<T, F, T1, F1>(this Poi<T, F> task, ConvertHandle<T, T1, F1> trueHandle, ConvertHandle<F, T1, F1> failHandle, ExceptionHandle<T1, F1> exceptionHandle)
            => new PoiTask<Poi<T1, F1>>(() => task.Then(trueHandle, failHandle, exceptionHandle));

        public static PoiTask<Poi<T1, F1>> Bind<T, F, T1, F1>(this PoiTask<Task<Poi<T, F>>> task, ConvertHandle<T, T1, F1> trueHandle, ConvertHandle<F, T1, F1> failHandle, ExceptionHandle<T1, F1> exceptionHandle) => new PoiTask<Poi<T1, F1>>(() =>
        {
            var threadTask1 = task.Result;
            if (threadTask1.Status == TaskStatus.Created)
            {
                threadTask1.RunSynchronously();
            }

            return task.Result.Result.Then(trueHandle, failHandle, exceptionHandle);
        });

        #endregion

        #region async

        public static PoiTask<Task<Poi<T1, F1>>> BindAsync<T, F, T1, F1>(this PoiTask<Poi<T, F>> task, ConvertHandle<T, T1, F1> trueHandle, ConvertHandle<F, T1, F1> failHandle, ExceptionHandle<T1, F1> exceptionHandle)
            => new PoiTask<Task<Poi<T1, F1>>>(() => Task<Poi<T1, F1>>.Factory.StartNew(() => task.Result.Then(trueHandle, failHandle, exceptionHandle)));

        public static PoiTask<Task<Poi<T1, F1>>> BindAsync<T, F, T1, F1>(this Task<Poi<T, F>> task, ConvertHandle<T, T1, F1> trueHandle, ConvertHandle<F, T1, F1> failHandle, ExceptionHandle<T1, F1> exceptionHandle)
            => new PoiTask<Task<Poi<T1, F1>>>(async () => (await task).Then(trueHandle, failHandle, exceptionHandle));

        public static PoiTask<Task<Poi<T1, F1>>> BindAsync<T, F, T1, F1>(this Poi<T, F> task, ConvertHandle<T, T1, F1> trueHandle, ConvertHandle<F, T1, F1> failHandle, ExceptionHandle<T1, F1> exceptionHandle)
            => new PoiTask<Task<Poi<T1, F1>>>(() => Task<Poi<T1, F1>>.Factory.StartNew(() => task.Then(trueHandle, failHandle, exceptionHandle)));

        public static PoiTask<Task<Poi<T1, F1>>> BindAsync<T, F, T1, F1>(this PoiTask<Task<Poi<T, F>>> task, ConvertHandle<T, T1, F1> trueHandle, ConvertHandle<F, T1, F1> failHandle, ExceptionHandle<T1, F1> exceptionHandle)
            => new PoiTask<Task<Poi<T1, F1>>>(async () => (await task.Result).Then(trueHandle, failHandle, exceptionHandle));

        #endregion
    }
}