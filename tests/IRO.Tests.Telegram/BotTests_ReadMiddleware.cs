﻿using Telegram.Bot.AspNetPipeline.Builder;
using Telegram.Bot.AspNetPipeline.Core;
using Telegram.Bot.AspNetPipeline.Extensions.DevExceptionMessage;
using Telegram.Bot.AspNetPipeline.Extensions.ImprovedBot;
using Telegram.Bot.Types;

namespace IRO.Tests.Telegram
{
    static class BotTests_ReadMiddleware
    {
        public static void Run(BotHandler botHandler)
        {
            botHandler.ConfigureServices((services) =>
            {

            });

            botHandler.ConfigureBuilder((builder) =>
            {
                builder.UseDevEceptionMessage();
                builder.Use(async (ctx, next) =>
                {
                    if (ctx.Message?.Text == null)
                    {
                        await ctx.SendTextMessageAsync("Not text message.");
                        ctx.ForceExit();
                        return;
                    }

                    var ctxTrimmedText = ctx.Message.Text.Trim();
                    Message msg = null;
                    if (ctxTrimmedText.StartsWith("/help"))
                    {
                        await ctx.SendTextMessageAsync("Commands:\n" +
                                                       "/current_user_reply\n" +
                                                       "/current_user\n" +
                                                       "/any_user\n" +
                                                       "/any_user_reply");
                        ctx.Processed();
                    }
                    else if (ctxTrimmedText.StartsWith("/any_user_reply"))
                    {
                        await ctx.SendTextMessageAsync("Reply to bot to process message.");
                        msg = await ctx.BotExt.ReadMessageAsync(ReadCallbackFromType.AnyUserReply);
                    }
                    else if (ctxTrimmedText.StartsWith("/current_user_reply"))
                    {
                        await ctx.SendTextMessageAsync("Reply to bot to process message.");
                        msg = await ctx.BotExt.ReadMessageAsync(ReadCallbackFromType.CurrentUserReply);
                    }
                    else if (ctxTrimmedText.StartsWith("/current_user"))
                    {
                        msg = await ctx.BotExt.ReadMessageAsync();
                    }
                    else if (ctxTrimmedText.StartsWith("/any_user"))
                    {
                        msg = await ctx.BotExt.ReadMessageAsync(ReadCallbackFromType.AnyUser);
                    }

                    if (msg != null)
                    {
                        ctx.Processed();
                        var msgText = msg.Text;
                        await ctx.SendTextMessageAsync($"Command : '{ctxTrimmedText}'.\n" +
                                                       $"Awaited msg: '{msgText}'.");
                    }


                    await next();
                });
                builder.Use(async (ctx, next) =>
                {
                    //if (!ctx.IsProcessed)
                    //    await ctx.SendTextMessageAsync($"Not processed '{ctx.Message.Text}'.");
                    await next();
                });
            });
            botHandler.Start();
        }
    }
}