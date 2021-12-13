﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpikePrism.Domain
{
    public class Calculator : INotifyPropertyChanged
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _token;

        public event PropertyChangedEventHandler PropertyChanged;

        private int _result;
        public int Result
        {
            get => _result;
            set
            {
                _result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }

        public async void Run()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _token = _cancellationTokenSource.Token;

            var random = new Random();
            while (!_token.IsCancellationRequested)
            {
                Result = random.Next(100);
                await Task.Delay(100);
            }
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

    }
}
