﻿using System.Runtime.Serialization;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Configuration;
[Serializable]
internal class ConfigurationException : Exception
{
    public ConfigurationException()
    {
    }

    public ConfigurationException(string? message) 
        : base(message)
    {
    }

    public ConfigurationException(string? message, Exception? innerException) 
        : base(message, innerException)
    {
    }

    protected ConfigurationException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}