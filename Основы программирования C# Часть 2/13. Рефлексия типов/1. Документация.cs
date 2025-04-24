using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Documentation;

public class Specifier<T> : ISpecifier
{
    private Type _type = typeof(T);

    public string GetApiDescription()
    {
        return _type.GetCustomAttribute<ApiDescriptionAttribute>()?
            .Description;
    }

    public string[] GetApiMethodNames()
    {
        return _type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<ApiMethodAttribute>() != null)
            .Select(m => m.Name)
            .ToArray();
    }

    public string GetApiMethodDescription(string methodName)
    {
        return _type.GetMethod(methodName)?
            .GetCustomAttribute<ApiDescriptionAttribute>()?
            .Description;
    }

    public string[] GetApiMethodParamNames(string methodName)
    {
        return _type.GetMethod(methodName)?
            .GetParameters()?
            .Select(p => p.Name)
            .ToArray();
    }

    public string GetApiMethodParamDescription(string methodName, string paramName)
    {
        return _type.GetMethod(methodName)?
            .GetParameters()
            .Where(p => p.Name == paramName)
            .FirstOrDefault()?
            .GetCustomAttribute<ApiDescriptionAttribute>()?
            .Description;
    }

    public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
    {
        var paramDescription = new ApiParamDescription();
        var param = _type.GetMethod(methodName)?
            .GetParameters()?
            .Where(p => p.Name == paramName)
            .FirstOrDefault();
        if (param != null)
            FillApiParamDescription(paramDescription, param, paramName);
        else
            paramDescription.ParamDescription = new CommonDescription(paramName);
        return paramDescription;
    }

    private static void FillApiParamDescription(ApiParamDescription paramDescription,
        ParameterInfo param, string paramName = null)
    {
        var requiredAttribute = param.GetCustomAttribute<ApiRequiredAttribute>();
        paramDescription.Required = requiredAttribute != null ? requiredAttribute.Required : false;
        paramDescription.MinValue = param.GetCustomAttribute<ApiIntValidationAttribute>()?.MinValue;
        paramDescription.MaxValue = param.GetCustomAttribute<ApiIntValidationAttribute>()?.MaxValue;
        if (paramName != null)
            paramDescription.ParamDescription = new CommonDescription(paramName,
                param.GetCustomAttribute<ApiDescriptionAttribute>()?.Description);
        else
            paramDescription.ParamDescription = new CommonDescription();
    }

    public ApiMethodDescription GetApiMethodFullDescription(string methodName)
    {
        var result = new ApiMethodDescription();
        var method = _type.GetMethod(methodName);
        if (method.GetCustomAttribute<ApiMethodAttribute>() == null)
            return null;
        result.MethodDescription = new CommonDescription(methodName,
            GetApiMethodDescription(methodName));
        var paramDescriptions = new List<ApiParamDescription>();
        foreach (var param in method?.GetParameters())
            paramDescriptions.Add(GetApiMethodParamFullDescription(methodName, param.Name));
        result.ParamDescriptions = paramDescriptions.ToArray();
        if (method.ReturnParameter.Name != null)
        {
            result.ReturnDescription = new ApiParamDescription();
            FillApiParamDescription(result.ReturnDescription, method.ReturnParameter);
        }
        return result;
    }
}