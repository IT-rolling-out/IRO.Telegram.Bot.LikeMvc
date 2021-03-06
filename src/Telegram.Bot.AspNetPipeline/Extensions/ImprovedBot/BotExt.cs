﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.AspNetPipeline.Core;
using Telegram.Bot.Types;

namespace Telegram.Bot.AspNetPipeline.Extensions.ImprovedBot
{
    /// <summary>
    /// Just proxing to singleton and pass context to methods.
    /// </summary>
    public class BotExt
    {
        readonly IBotExtSingleton _botExtSingleton;

        readonly UpdateContext _updateContext;

        public BotExt(IBotExtSingleton botExtSingleton, UpdateContext updateContext)
        {
            _botExtSingleton = botExtSingleton;
            _updateContext = updateContext;
        }

        /// <summary>
        /// </summary>
        /// <param name="fromType">Used to set which members messages must be processed.</param>
        public Task<Message> ReadMessageAsync(
            ReadCallbackFromType fromType = ReadCallbackFromType.CurrentUser,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            return _botExtSingleton.ReadMessageAsync(_updateContext, fromType, cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="updateValidator">User delegate to check if Update from current context is fits.
        /// If true - current Update passed to callback result, else - will be processed by other controller actions with lower priority.</param>
        public Task<Message> ReadMessageAsync(
            UpdateValidatorDelegate updateValidator,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            return _botExtSingleton.ReadMessageAsync(_updateContext, updateValidator, cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="updateValidator">User delegate to check if Update from current context is fits.
        /// If true - current Update passed to callback result, else - will be processed by other controller actions with lower priority.</param>
        public Task<Update> ReadUpdateAsync(
            UpdateValidatorDelegate updateValidator,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            return _botExtSingleton.ReadUpdateAsync(_updateContext, updateValidator, cancellationToken);
        }

        public Task<Update> ReadUpdateAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _botExtSingleton.ReadUpdateAsync(
                _updateContext,
                async (newUpdateContext, originalUpdateContext) => UpdateValidatorResult.Valid,
                cancellationToken
                );
        }

    }
}
