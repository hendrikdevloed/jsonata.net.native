﻿using Jsonata.Net.Native.Eval;
using Jsonata.Net.Native.Json;
using Jsonata.Net.Native.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonata.Net.Native
{
    public sealed class JsonataQuery
    {
        private readonly Node m_node;

        public JsonataQuery(string queryText)
        {
            this.m_node = Parser.Parse(queryText);
        }

        public string Eval(string dataJson)
        {
            JToken data = JToken.Parse(dataJson, ParseSettings.DefaultSettings);
            JToken result = this.Eval(data);
            return result.ToIndentedString();
        }

        public async Task<string> EvalAsync(string dataJson)
        {
            JToken data = JToken.Parse(dataJson, ParseSettings.DefaultSettings);
            JToken result = await this.EvalAsync(data);
            return result.ToIndentedString();
        }

        public JToken Eval(JToken data, JObject? bindings = null)
        {
            EvaluationEnvironment env;
            if (bindings != null)
            {
                env = new EvaluationEnvironment(bindings);
            }
            else
            {
                env = EvaluationEnvironment.DefaultEnvironment;
            };
            return EvalProcessor.EvaluateJson(this.m_node, data, env);
        }

        public Task<JToken> EvalAsync(JToken data, JObject? bindings = null)
        {
            EvaluationEnvironment env;
            if (bindings != null)
            {
                env = new EvaluationEnvironment(bindings);
            }
            else
            {
                env = EvaluationEnvironment.DefaultEnvironment;
            };
            return EvalProcessor.EvaluateJsonAsync(this.m_node, data, env);
        }

        public JToken Eval(JToken data, EvaluationEnvironment environment)
        {
            return EvalProcessor.EvaluateJson(this.m_node, data, environment);
        }

        public Task<JToken> EvalAsync(JToken data, EvaluationEnvironment environment)
        {
            return EvalProcessor.EvaluateJsonAsync(this.m_node, data, environment);
        }

        public override string ToString()
        {
            return this.m_node.ToString();
        }
    }
}
