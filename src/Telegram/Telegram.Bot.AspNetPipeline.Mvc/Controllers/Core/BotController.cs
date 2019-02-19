﻿using System.Threading;
using Microsoft.Extensions.Logging;
using Telegram.Bot.AspNetPipeline.Core;
using Telegram.Bot.AspNetPipeline.Extensions.ImprovedBot;
using Telegram.Bot.AspNetPipeline.Extensions.Logging;
using Telegram.Bot.AspNetPipeline.Mvc.Core;
using Telegram.Bot.AspNetPipeline.Mvc.Extensions.Main;
using Telegram.Bot.AspNetPipeline.Mvc.Extensions.MvcFeatures;
using Telegram.Bot.Types;

namespace Telegram.Bot.AspNetPipeline.Mvc.Controllers.Core
{
    public abstract class BotController
    {
        public ControllerActionContext ControllerContext { get; private set; }

        #region Proxy properties.
        public UpdateContext UpdateContext => ControllerContext.UpdateContext;

        public Update Update => ControllerContext.UpdateContext.Update;

        public Message Message => Update.Message;

        public Chat Chat => Update.Message.Chat;

        public BotClientContext BotContext => UpdateContext.BotContext;

        public ITelegramBotClient Bot => UpdateContext.BotContext.Bot;

        /// <summary>
        /// Just proxy to UpdateContext.BotExt().
        /// </summary>
        public BotExt BotExt => UpdateContext.BotExt;

        public CancellationToken UpdateProcessingAborted => UpdateContext.UpdateProcessingAborted;

        /// <summary>
        /// Just proxy to ControllerContext.Features().
        /// </summary>
        public ContextMvcFeatures Features => ControllerContext.Features();

        /// <summary>
        /// Just proxy to UpdateContext.Logger().
        /// Useful for fast easy logging, but better to create logger by <see cref="ILoggerFactory"/>.
        /// </summary>
        public ILogger Logger => UpdateContext.Logger();

        public bool IsModelStateValid => ControllerContext.IsModelStateValid;
        #endregion

        bool _isInit;

        #region Init region
        /// <summary>
        /// Called after controller created to set context.
        /// </summary>
        private void Initialize(ControllerActionContext controllerActionContext)
        {
            if (_isInit)
            {
                throw new System.Exception("You can`t init controller twice.");
            }
            ControllerContext = controllerActionContext;
            _isInit = true;
            AfterInitialized();
        }

        /// <summary>
        /// Invoked after context initialized.
        /// </summary>
        protected virtual void AfterInitialized()
        {
        }

        /// <summary>
        /// Used to invoke initializer after controller constructed but before controller routing method invoked.
        /// </summary>
        public static void InvokeInitializer(BotController controller, ControllerActionContext controllerActionContext)
        {
            controller.Initialize(controllerActionContext);
        }
        #endregion
    }
}
