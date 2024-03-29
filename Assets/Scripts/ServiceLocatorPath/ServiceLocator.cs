﻿using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class ServiceLocator
{
    public static ServiceLocator Instance => _instance ?? (_instance = new ServiceLocator());
    private static ServiceLocator _instance;

    private readonly Dictionary<Type, object> _services;

    private ServiceLocator()
    {
        _services = new Dictionary<Type, object>();
    }

    public void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
        Assert.IsFalse(_services.ContainsKey(type), 
            $"Service {type} already registered");
        
        _services.Add(type, service);
    }

    public T GetService<T>()
    {
        var type = typeof(T);
        if (!_services.TryGetValue(type, out var service))
        {
            throw new Exception($"Service {type} not found");
        }

        return (T) service;
    }
    
    public void RemoveService<T>()
    {
        var type = typeof(T);
        if (!_services.ContainsKey(type))
        {
            throw new Exception($"Service {type} not found");
        }

        _services.Remove(type);
    }
}