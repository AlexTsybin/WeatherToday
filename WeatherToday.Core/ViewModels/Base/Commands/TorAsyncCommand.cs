using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherToday.API.Utils.Extensions;
using WeatherToday.Core.Services.Platform;

namespace WeatherToday.Core.ViewModels.Base.Commands
{
    public class TorAsyncCommand : MvxAsyncCommandBase, IMvxAsyncCommand
    {
        private readonly Func<CancellationToken, Task> _execute;
        private readonly Func<bool> _canExecute;

        public TorAsyncCommand(Func<Task> execute, Func<bool> canExecute = null, bool allowConcurrentExecutions = false)
            : base(allowConcurrentExecutions)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = (cancellationToken) => execute();
            _canExecute = canExecute;
        }

        public TorAsyncCommand(Func<CancellationToken, Task> execute, Func<bool> canExecute = null, bool allowConcurrentExecutions = false)
            : base(allowConcurrentExecutions)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        protected override bool CanExecuteImpl(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        protected override async Task ExecuteAsyncImpl(object parameter)
        {
            try
            {
                await _execute(CancelToken);
            }
            catch (Exception ex)
            {
                ex.HandleEx();
                await Mvx.IoCProvider.Resolve<IUserInteraction>().AlertAsync("Something went wrong", "Oops!");

                Mvx.IoCProvider.Resolve<IUserInteraction>().CloseApp();
            }
        }

        public static TorAsyncCommand<T> CreateCommand<T>(Func<T, Task> execute, Func<T, bool> canExecute = null, bool allowConcurrentExecutions = false)
        {
            return new TorAsyncCommand<T>(execute, canExecute, allowConcurrentExecutions);
        }

        public static TorAsyncCommand<T> CreateCommand<T>(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute = null, bool allowConcurrentExecutions = false)
        {
            return new TorAsyncCommand<T>(execute, canExecute, allowConcurrentExecutions);
        }

        public async Task ExecuteAsync(object parameter = null)
        {
            await base.ExecuteAsync(parameter, false).ConfigureAwait(false);
        }
    }

    public class TorAsyncCommand<T> : MvxAsyncCommandBase, IMvxCommand, IMvxAsyncCommand<T>
    {
        private readonly Func<T, CancellationToken, Task> _execute;
        private readonly Func<T, bool> _canExecute;

        public TorAsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute = null, bool allowConcurrentExecutions = false)
            : base(allowConcurrentExecutions)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = (p, c) => execute(p);
            _canExecute = canExecute;
        }

        public TorAsyncCommand(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute = null, bool allowConcurrentExecutions = false)
            : base(allowConcurrentExecutions)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public async Task ExecuteAsync(T parameter)
        {
            await ExecuteAsync(parameter, false);
        }

        public void Execute(T parameter)
            => base.Execute(parameter);

        public bool CanExecute(T parameter)
            => base.CanExecute(parameter);

        protected override bool CanExecuteImpl(object parameter)
            => _canExecute == null || _canExecute((T)typeof(T).MakeSafeValueCore(parameter));

        protected override async Task ExecuteAsyncImpl(object parameter)
        {
            try
            {
                await _execute((T)typeof(T).MakeSafeValueCore(parameter), CancelToken);
            }
            catch (Exception ex)
            {
                ex.HandleEx();
                await Mvx.IoCProvider.Resolve<IUserInteraction>().AlertAsync("Something went wrong", "Oops!");

                Mvx.IoCProvider.Resolve<IUserInteraction>().CloseApp();
            }
        }
    }
}
