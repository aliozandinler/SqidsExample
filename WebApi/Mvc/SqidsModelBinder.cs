using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sqids;

namespace WebApi.Mvc;

public class SqidsModelBinder(SqidsEncoder<int> sqidsEncoder) : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        string modelName = bindingContext.ModelName;
        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult != ValueProviderResult.None && !string.IsNullOrWhiteSpace(valueProviderResult.FirstValue))
        {
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            var ids = sqidsEncoder.Decode(valueProviderResult.FirstValue);
            if (ids is not [])
                bindingContext.Result = ModelBindingResult.Success(ids[0]);
        }

        return Task.CompletedTask;
    }
}