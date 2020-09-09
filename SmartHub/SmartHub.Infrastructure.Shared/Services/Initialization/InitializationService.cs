﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Figgle;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Events;
using SmartHub.Application.UseCases.HomeFolder;
using SmartHub.Infrastructure.Database;
using SmartHub.Infrastructure.Shared.Services.Background;

namespace SmartHub.Infrastructure.Shared.Services.Initialization
{
    public class InitializationService : IInitializationService
    {
        private readonly IHomeFolderService _homeFolderService;
        private readonly ILogger _logger = Log.ForContext(typeof(InitializationService));
        private readonly BackgroundServiceStarter<IChannelManager> _channelManagerStarter;
        private readonly BackgroundServiceStarter<IEventDispatcher> _eventDispatcherStarter;

        public InitializationService(IHomeFolderService homeFolderService, BackgroundServiceStarter<IChannelManager> channelManagerStarter,
            BackgroundServiceStarter<IEventDispatcher> eventDispatcherStarter)
        {
            _homeFolderService = homeFolderService;
            _channelManagerStarter = channelManagerStarter;
            _eventDispatcherStarter = eventDispatcherStarter;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _homeFolderService.Create().ConfigureAwait(false);
            WelcomeWithAsciiLogo();
            var (homePath, folderName) = _homeFolderService.GetHomeFolderPath();
            _logger.Information("SmartHub folder is at {@homePath}\\{@folderName}",
                homePath,
                folderName);
            _logger.Information("Start initialization...");
            await _channelManagerStarter.StartAsync(cancellationToken);
            await _eventDispatcherStarter.StartAsync(cancellationToken);
            _logger.Information("Stop initialization.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _channelManagerStarter.StopAsync(cancellationToken);
            await _eventDispatcherStarter.StopAsync(cancellationToken);
            _logger.Information("Stopped all BackgroundServices.");
        }


        private void WelcomeWithAsciiLogo()
        {
            _logger.Information($"{Environment.NewLine}" +
                                FiggleFonts.Standard.Render("SmartHub"));
            _logger.Information($"{Environment.NewLine}" +
                                "Welcome to SmartHub, this is a smarthome written in asp.net core and vue3." + $"{Environment.NewLine}" +
                                "This is a private project of mine and I use this to learn new things and create my own smarthome that " + $"{Environment.NewLine}" +
                                "I am going to use myself." + $"{Environment.NewLine}" +
                                "For more information and if you encounter any issues or have any feedback, please visit: https://github.com/SmartHub-Io/SmartHub." + $"{Environment.NewLine}" +
                                "--------------------------------------------------");
        }


    }
}