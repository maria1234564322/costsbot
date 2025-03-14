﻿using TelegramBot.ChatEngine.Commands.Interfaces;
using TelegramBot.ChatEngine.Commands.PipelineBaseKit;

namespace TelegramBot.ChatEngine.Commands.Routing;

/// <summary>
/// Representatin for list of routes and commands under these routes
/// </summary>
public class RoutingTable
{
    private readonly IReadOnlyDictionary<string, CommandMetadata> _routes;
    private readonly IReadOnlyDictionary<string, Type> _stageTypes;
    public RoutingTable(Dictionary<string, CommandMetadata> routes, IReadOnlyDictionary<string, Type> stageTypes)
    {
        _routes = routes;
        _stageTypes = stageTypes;
    }

    public Type GetStageType(string typeFullname)
    {
        if (typeFullname == null) return null;
        _ = _stageTypes.TryGetValue(typeFullname, out var result);
        return result;
    }

    /// <summary>
    /// Returns the command metadata found by route or null if no matches were found
    /// </summary>
    /// <param name="route"></param>
    /// <returns></returns>
    public CommandMetadata GetCommand(string route)
    {
        if (route == null) return null;
        _routes.TryGetValue(route, out var command);
        return command;
    }

    public string AlternativeRoute<TCommand>() where TCommand : ICommand
    {
        var commandType = typeof(TCommand);
        return _routes
            .FirstOrDefault(x => x.Value.Type == commandType)
            .Value
            .Route
            .AlternativeRoute;
    }
    public string Route<TCommand>() where TCommand : ICommand
    {
        var commandType = typeof(TCommand);
        return _routes
            .FirstOrDefault(x => x.Value.Type == commandType)
            .Value
            .Route
            .Route;
    }
}
