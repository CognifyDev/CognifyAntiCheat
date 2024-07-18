using System;
using System.Collections.Generic;
using System.Linq;
using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Utils;
using BindingFlags = System.Reflection.BindingFlags;

namespace CognifyAntiCheat.Check;

public class CheckManager
{
    private readonly PlayerControl _target;

    private readonly Dictionary<Type, Check> _checks = new();

    private static readonly List<Type> CheckTypes = new();
    
    public CheckManager(PlayerControl target)
    {
        _target = target;
        
        foreach (var checkType in CheckTypes)
        {
            var constructor = checkType.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic,
                new[] { typeof(PlayerControl) });
            if (constructor == null) continue;
            var check = (Check) constructor.Invoke(new object?[] { _target });
            _checks.Add(checkType, check);
        }
    }

    public void Register()
    {
        _checks.Values.Select(check => check.GetListener()).
            ForEach(listener => ListenerManager.GetManager().RegisterListenerIfNotExists(listener));
    }

    public void Clear()
    {
        _checks.Values.Select(check => check.GetListener()).
            ForEach(listener => ListenerManager.GetManager()
                .UnRegisterHandlers(ListenerManager.GetManager().GetHandlers(listener).ToArray()));
    }
    
    public static CheckManager GetManager(PlayerControl target)
    {
        return new CheckManager(target);
    }

    public static void RegisterChecks(Type[] checkTypes)
    {
        CheckTypes.AddRange(checkTypes);
    }
}