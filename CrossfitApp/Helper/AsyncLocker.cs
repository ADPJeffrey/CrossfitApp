using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrossfitApp
{
	public class AsyncLocker
	{
		private readonly AsyncSemaphore _semaphore;
		private readonly Task<Releaser> _releaser;

		public AsyncLocker()
		{
			_semaphore = new AsyncSemaphore(1);
			_releaser = Task.FromResult(new Releaser(this));
		}

		public Task<Releaser> LockAsync()
		{
			var wait = _semaphore.WaitAsync();
			return wait.IsCompleted ?
				_releaser :
				wait.ContinueWith((_, state) => new Releaser((AsyncLocker)state),
					this, CancellationToken.None,
					TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		public struct Releaser : IDisposable
		{
			private readonly AsyncLocker _toRelease;

			internal Releaser(AsyncLocker toRelease) { _toRelease = toRelease; }

			public void Dispose()
			{
				if (_toRelease != null)
					_toRelease._semaphore.Release();
			}
		}
	}
}
