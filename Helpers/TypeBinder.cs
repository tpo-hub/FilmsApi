using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var NameProp = bindingContext.ModelName;
            var ValuesProvider = bindingContext.ValueProvider.GetValue(NameProp);

            if(ValuesProvider == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try 
            {
                var DeserializedValue = JsonConvert.DeserializeObject<T>(ValuesProvider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(DeserializedValue);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(NameProp, $"Invalid value for this field");
            }

            return Task.CompletedTask;
        
        }
    }
}
