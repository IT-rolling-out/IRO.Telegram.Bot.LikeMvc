﻿using System.Threading.Tasks;
using Telegram.Bot.AspNetPipeline.Extensions;
using Telegram.Bot.AspNetPipeline.Extensions.ReadWithoutContext;
using Telegram.Bot.AspNetPipeline.Mvc.Controllers.Core;
using Telegram.Bot.AspNetPipeline.Mvc.Routing.Metadata;
using Telegram.Bot.Types;

namespace IRO.Samples.AspWebhooks.TelegramControllers
{
    public class TelegramSampleController : BotController
    {
        [BotRoute("/hi")]
        public async Task Hi()
        {
            await UpdateContext.SendTextMessageAsync("Oh, hi mark.");
        }
    }
}
