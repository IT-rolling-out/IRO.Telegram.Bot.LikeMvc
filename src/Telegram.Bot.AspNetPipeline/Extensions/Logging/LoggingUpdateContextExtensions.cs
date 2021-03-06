﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot.AspNetPipeline.Core;

//ReSharper disable CheckNamespace
namespace Telegram.Bot.AspNetPipeline.Extensions
{
    public static class LoggingUpdateContextExtensions
    {
        const string LoggerPropertyName = "_Logger";
       
        /// <summary>
        /// Fast way to log message in UpdateContext scope.
        /// <para></para>
        /// Better to use ILoggerFactory injection, but you can use current service for fast loggin.
        /// </summary>
        public static ILogger Logger(this UpdateContext @this)
        {
            if (@this.Properties.TryGetValue(LoggerPropertyName, out var loggerNotCasted))
            {
                return (ILogger)loggerNotCasted;
            }
            else
            {
                var loggerFactory = @this.Services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger(@this.GetType());
                @this.Properties[LoggerPropertyName] = logger;
                return logger;
            }
        }
    }
}
