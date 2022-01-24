using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Timers;

namespace RapidPay.Domain.Adapters
{
    public class UniversalFeesExchange
    {
        public static UniversalFeesExchange _singleton;
        private object syncStack = new object();

        public UniversalFeesExchange Instance
        {
            get
            {
                if (_singleton == null)
                    _singleton = new UniversalFeesExchange();
                return _singleton;
            }
        }

        public UniversalFeesExchange()
        {
            var aTimer = new System.Timers.Timer(30000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            Fees.Push(1);
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            lock (syncStack)
            {
                var fee = CalculateHourlyRate();
                Fees.TryPeek(out decimal lastFee);
                var newFee = lastFee * fee;
                Fees.Push(newFee);
            }
        }

        private ConcurrentStack<decimal> Fees = new();
        public decimal CurrentFee()
        {
            decimal currentFee;
            lock (syncStack)
            {
                Fees.TryPeek(out currentFee);
            }
            return currentFee;
        }
        
        public decimal CalculateHourlyRate()
        {
            return (decimal)RandomNumberBetween(0, 2);
        }
        private  double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = new Random().NextDouble();

            return minValue + (next * (maxValue - minValue));
        }
    }
}