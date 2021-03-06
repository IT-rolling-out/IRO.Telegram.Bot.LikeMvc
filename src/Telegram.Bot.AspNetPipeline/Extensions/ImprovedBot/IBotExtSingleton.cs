﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.AspNetPipeline.Core;
using Telegram.Bot.Types;

namespace Telegram.Bot.AspNetPipeline.Extensions.ImprovedBot
{
    public interface IBotExtSingleton
    {
        IList<UpdateValidatorDelegate> GlobalValidators { get; }

        /// <summary>
        /// </summary>
        /// <param name="updateContext">Current command context. Needed to find TaskCompletionSource of current command.</param>
        /// <param name="fromType">Used to set which members messages must be processed.</param>
        Task<Message> ReadMessageAsync(
            UpdateContext updateContext, 
            ReadCallbackFromType fromType,
            CancellationToken cancellationToken
        );

        /// <summary>
        /// </summary>
        /// <param name="updateContext">Current command context. Needed to find TaskCompletionSource of current command.</param>
        /// <param name="updateValidator">User delegate to check if Update from current context is fits.
        /// If true - current Update passed to callback result, else - will be processed by other controller actions with lower priority.</param>
        Task<Message> ReadMessageAsync(
            UpdateContext updateContext,
            UpdateValidatorDelegate updateValidator,
            CancellationToken cancellationToken
        );

        Task<Update> ReadUpdateAsync(
            UpdateContext updateContext,
            UpdateValidatorDelegate updateValidator,
            CancellationToken cancellationToken
        );

        Task OnUpdateInvoke(UpdateContext newCtx, Func<Task> next);
    }
}