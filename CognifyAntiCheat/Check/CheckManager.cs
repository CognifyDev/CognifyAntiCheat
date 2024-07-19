using System;
using System.Collections.Generic;
using System.Linq;
using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Check;

public class CheckManager
{
    private static readonly List<CheckManager> CheckManagers = new();
    
    private readonly List<Check> _checks = new();

    private static readonly List<Type> CheckTypes = new();

    public PlayerControl Player { get; }
    
    public CheckManager(PlayerControl target)
    {
        Player = target;
        foreach (var checkType in CheckTypes)
        {
            var constructor = checkType.GetConstructors()[0];
            var check = (Check) constructor.Invoke(new object?[] { target });
            _checks.Add(check);
        }
        CheckManagers.Add(this);
    }

    public void Register()
    {
        foreach (var check in _checks.Where(check => check.Enabled))
        {
            ListenerManager.GetManager().RegisterListener(check.GetListener());
        }
    }

    public void Clear()
    {
        _checks.Where(check => check.Enabled).Select(check => check.GetListener()).
            ForEach(listener => ListenerManager.GetManager()
                .UnRegisterHandlers(ListenerManager.GetManager().GetHandlers(listener).ToArray()));
        CheckManagers.Remove(this);
    }
    
    public static CheckManager GetManager(PlayerControl target)
    {
        foreach (var checkManager in CheckManagers.Where(checkManager => checkManager.Player.IsSamePlayer(target)))
        {
            return checkManager;
        }

        return new CheckManager(target);
    }

    public static void RegisterChecks(IEnumerable<Type> checkTypes)
    {
        CheckTypes.AddRange(checkTypes);
    }
}