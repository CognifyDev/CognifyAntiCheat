using System;
using System.Collections.Generic;
using System.Linq;
using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Check;
/*
 * FIXME
 * 无法正常注册Listeners
 */
public class CheckManager
{
    private readonly List<Check> _checks = new();

    private static readonly List<Type> CheckTypes = new();
    
    public CheckManager(PlayerControl target)
    {
        foreach (var checkType in CheckTypes)
        {
            var constructor = checkType.GetConstructors()[0];
            var check = (Check) constructor.Invoke(new object?[] { target });
            _checks.Add(check);
        }
    }

    public void Register()
    {
        foreach (var listener in _checks.Select(check => check.GetListener()))
        {
            ListenerManager.GetManager().RegisterListener(listener);
        }
    }

    public void Clear()
    {
        _checks.Select(check => check.GetListener()).
            ForEach(listener => ListenerManager.GetManager()
                .UnRegisterHandlers(ListenerManager.GetManager().GetHandlers(listener).ToArray()));
    }
    
    public static CheckManager GetManager(PlayerControl target)
    {
        return new CheckManager(target);
    }

    public static void RegisterChecks(IEnumerable<Type> checkTypes)
    {
        CheckTypes.AddRange(checkTypes);
    }
}