﻿using Jsonata.Net.Native.Parsing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Jsonata.Net.Native.Eval
{
    internal static class EvalProcessor_Lambda
	{
		internal static JToken CallLambdaFunction(FunctionTokenLambda lambdaFunction, List<JToken> args, JToken? inputAsContext)
		{
			if (lambdaFunction.signature != null)
            {
				args = ValidateSignature(lambdaFunction.signature, args, inputAsContext);
            };
			List<(string, JToken)> alignedArgs = AlignArgs(lambdaFunction.paramNames, args);

			Environment executionEnv = Environment.CreateNestedEnvironment(lambdaFunction.environment);
			foreach ((string name, JToken value) in alignedArgs)
            {
				executionEnv.Bind(name, value);
            };

			JToken result = EvalProcessor.Eval(lambdaFunction.body, lambdaFunction.context, executionEnv);
			return result;
		}

        private static List<(string, JToken)> AlignArgs(List<string> paramNames, List<JToken> args)
        {
			List<(string, JToken)> result = new List<(string, JToken)>(paramNames.Count);
			//for some reson jsonata does not care if function invocation args does not match expected number of args 
			// - in case when there's no signature specified
			// see for example lambdas.case010 test

			for (int i = 0; i < paramNames.Count; ++i)
            {
				JToken value;
				if (i >= args.Count)
                {
					value = EvalProcessor.UNDEFINED;
                }
                else
                {
					value = args[i];
                };
				result.Add((paramNames[i], value));
            }
			return result;
        }

        private static List<JToken> ValidateSignature(LambdaNode.Signature signature, List<JToken> args, JToken? inputAsContext)
        {
            throw new NotImplementedException();
        }

        /*
		internal static JToken CallLambdaFunction(string functionName, MethodInfo methodInfo, List<JToken> args, JToken? inputAsContext, Environment env)
		{
			ParameterInfo[] parameterList = methodInfo.GetParameters();

			if (args.Count > parameterList.Length)
			{
				throw new JsonataException("T0410", $"Function '{functionName}' requires {parameterList.Length} arguments. Passed {args.Count} arguments");
			};

			object?[] parameters;
			try
			{
				parameters = BindFunctionArguments(functionName, parameterList, args, env, out bool returnUndefined);
				if (returnUndefined)
                {
					return EvalProcessor.UNDEFINED;
                }
			}
			catch (JsonataException)
            {
				//try binding with context if possible
				if (inputAsContext != null 
					&& args.Count < parameterList.Length 
					&& parameterList[0].IsDefined(typeof(AllowContextAsValueAttribute))
				)
				{
					List<JToken> newArgs = new List<JToken>(args.Count + 1);
					newArgs.Add(inputAsContext);
					newArgs.AddRange(args);
					parameters = BindFunctionArguments(functionName, parameterList, newArgs, env, out bool returnUndefined);
					if (returnUndefined)
					{
						return EvalProcessor.UNDEFINED;
					}
				}
				else
                {
					throw;
                }
			}
			object? resultObj;
			try
			{
				resultObj = methodInfo.Invoke(null, parameters);
			}
			catch (TargetInvocationException ti)
			{
				if (ti.InnerException is JsonataException)
				{
					ExceptionDispatchInfo.Capture(ti.InnerException).Throw();
				}
				else
				{
					throw new Exception($"Error evaluating function '{functionName}': {(ti.InnerException?.Message ?? "?")}", ti);
				}
				throw;
			}
			JToken result = ConvertFunctionResult(functionName, resultObj);
			return result;
		}

		private static object?[] BindFunctionArguments(string functionName, ParameterInfo[] parameterList, List<JToken> args, Environment env, out bool returnUndefined)
        {
			returnUndefined = false;
			object?[] parameters = new object[parameterList.Length];
			for (int i = 0; i < parameterList.Length; ++i)
			{
				ParameterInfo parameterInfo = parameterList[i];
				if (i >= args.Count)
				{
					OptionalArgumentAttribute? optional = parameterInfo.GetCustomAttribute<OptionalArgumentAttribute>();
					if (optional != null)
					{
						//use default value
						parameters[i] = optional.DefaultValue;
						continue;
					};
					EvalEnvironmentArgumentAttribute? evalEnv = parameterInfo.GetCustomAttribute<EvalEnvironmentArgumentAttribute>();
					if (evalEnv != null)
					{
						if (parameterInfo.ParameterType != typeof(EvaluationEnvironment))
						{
							throw new Exception($"Declaration error for function '{functionName}': attribute [{nameof(EvalEnvironmentArgumentAttribute)}] can only be specified for arguments of type {nameof(EvaluationEnvironment)}");
						};
						parameters[i] = env.GetEvaluationEnvironment();
					};
					throw new JsonataException("T0410", $"Function '{functionName}' requires {parameterList.Length} arguments. Passed {args.Count} arguments");
				}
				else
				{
					parameters[i] = ConvertFunctionArg(functionName, i, args[i], parameterInfo, out bool needReturnUndefined);
					if (needReturnUndefined)
					{
						returnUndefined = true;
					}
				}
			};
			return parameters;
		}

		private static JToken ConvertFunctionResult(string functionName, object? resultObj)
		{
			if (resultObj is JToken token)
			{
				return token;
			}
			else if (resultObj == null)
			{
				return JValue.CreateNull();
			}
			else if (resultObj is double resultDouble)
			{
				return ReturnDoubleResult(resultDouble);
			}
			else if (resultObj is float resultFloat)
			{
				return ReturnDoubleResult(resultFloat);
			}
			else if (resultObj is decimal resultDecimal)
			{
				return ReturnDecimalResult(resultDecimal);
			}
			else if (resultObj is int resultInt)
			{
				return new JValue(resultInt);
			}
			else if (resultObj is long resultLong)
			{
				return new JValue(resultLong);
			}
			else if (resultObj is string resultString)
			{
				return new JValue(resultString);
			}
			else if (resultObj is bool resultBool)
			{
				return new JValue(resultBool);
			}
			else
			{
				return JToken.FromObject(resultObj);
			}
		}

		private static JToken ReturnDoubleResult(double resultDouble)
		{
			if (Double.IsNaN(resultDouble) || Double.IsInfinity(resultDouble))
			{
				throw new JsonataException("D3030", "Jsonata does not support NaNs or Infinity values");
			};

			long resultLong = (long)resultDouble;
			if (resultLong == resultDouble)
			{
				return new JValue(resultLong);
			}
			else
			{
				return new JValue(resultDouble);
			}
		}

		private static JToken ReturnDecimalResult(decimal resultDecimal)
		{
			long resultLong = (long)resultDecimal;
			if (resultLong == resultDecimal)
			{
				return new JValue(resultLong);
			}
			else
			{
				return new JValue(resultDecimal);
			}
		}

		private static object ConvertFunctionArg(string functionName, int parameterIndex, JToken argToken, ParameterInfo parameterInfo, out bool returnUndefined)
		{
			if (argToken.Type == JTokenType.Undefined
				&& parameterInfo.GetCustomAttribute<PropagateUndefinedAttribute>() != null
			)
			{
				returnUndefined = true;
				return EvalProcessor.UNDEFINED;
			}
			else
			{
				returnUndefined = false;
			};

			//TODO: add support for broadcasting Undefined
			if (parameterInfo.ParameterType.IsAssignableFrom(argToken.GetType()))
			{
				return argToken;
			}
			else if (parameterInfo.ParameterType == typeof(double))
			{
				switch (argToken.Type)
				{
				case JTokenType.Integer:
					return (double)(long)argToken;
				case JTokenType.Float:
					return (double)argToken;
				}
			}
			else if (parameterInfo.ParameterType == typeof(float))
			{
				switch (argToken.Type)
				{
				case JTokenType.Integer:
					return (float)(long)argToken;
				case JTokenType.Float:
					return (float)(double)argToken;
				}
			}
			else if (parameterInfo.ParameterType == typeof(int))
			{
				switch (argToken.Type)
				{
				case JTokenType.Integer:
					return (int)(long)argToken;
				}
			}
			else if (parameterInfo.ParameterType == typeof(long))
			{
				switch (argToken.Type)
				{
				case JTokenType.Integer:
					return (long)argToken;
				}
			}
			else if (parameterInfo.ParameterType == typeof(decimal))
			{
				switch (argToken.Type)
				{
				case JTokenType.Integer:
					return (decimal)(long)argToken;
				case JTokenType.Float:
					return (decimal)(double)argToken;
				}
			}
			else if (parameterInfo.ParameterType == typeof(string))
            {
				switch (argToken.Type)
				{
				case JTokenType.String:
					return (string)argToken!;
				}
			}
			else if (parameterInfo.ParameterType == typeof(bool))
			{
				switch (argToken.Type)
				{
				case JTokenType.Boolean:
					return (bool)argToken;
				}
			}
			throw new JsonataException("T0410", $"Argument {parameterIndex} ('{parameterInfo.Name}') of function {functionName} should be {parameterInfo.ParameterType.Name} bun incompatible value of type {argToken.Type} was specified");
		}

		*/
    }
}