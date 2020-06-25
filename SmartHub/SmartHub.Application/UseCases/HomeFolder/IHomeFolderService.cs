using System;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Application.UseCases.HomeFolder
{
    public interface IHomeFolderService : IInitialize
    {
        Tuple<string, string> GetHomeFolderPath();
    }
}