﻿using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DSharpPlus.Entities;
using Application.Handlers;

namespace Application
{
    public class Client
    {
        public Handlers.MessageHandler msgHandler;
        public Handlers.EventHandler eventHandler;
        public DiscordClient client = new DiscordClient(new DiscordConfiguration {
            Token = Config.DiscordToken,
            TokenType = TokenType.Bot
        });

        public async Task Init(string[] args)
        {
            try
            {
                await client.ConnectAsync();
                Console.WriteLine("Connected to Discord");
                this.msgHandler = new Handlers.MessageHandler(this);
            }catch(Exception e)
            {
                Console.WriteLine("Could not connect to discord");
                Console.WriteLine(e.Message.ToString());
                return;
            }

            this.client.MessageCreated += msgHandler.OnMessage;
            this.client.GuildMemberAdded += eventHandler.OnGuildMemberJoin;

            await Task.Delay(-1);
        }
    }
}
