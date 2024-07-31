        public class ContaTipoConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Conta);
            }
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JObject jsonObject = JObject.Load(reader);
                Conta conta = new Conta();
                if (jsonObject.TryGetValue("tipo", out JToken tipoToken))
                {
                    if (tipoToken.Type == JTokenType.Integer)
                    {
                        conta.Tipo = tipoToken.Value<int>();
                    }
                    else if (tipoToken.Type == JTokenType.Object)
                    {
                        conta.Tipo = tipoToken.ToObject<TipoDescricao>();
                    }
                }
                // Preencha outras propriedades da pessoa, se houver
                return conta;
            }
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException(); // Não é necessário para a desserialização
            }
        }


    
        public void FormatDataTipo()
        {
            TipoDescricao tipoDescricao = Tipo as TipoDescricao;
            
            if (tipoDescricao != null && Tipo is TipoDescricao)
            {
                switch (tipoDescricao.Id)
                {
                    case "Poupanca":
                        TipoId = 2;
            }
        }

            // Registrar a fábrica personalizada de JsonSerializerSettings para a classe de conversão da Conta
            services.AddSingleton<JsonSerializerSettings>(serviceProvider =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new ContaTipoConverter());
                settings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                return settings;
            });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);