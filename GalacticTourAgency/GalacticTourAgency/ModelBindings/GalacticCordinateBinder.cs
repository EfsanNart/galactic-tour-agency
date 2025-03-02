using GalacticTourAgency.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GalacticTourAgency.ModelBindings
{
    public class GalacticCordinateBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            if (string.IsNullOrWhiteSpace(value))
            {
                return Task.CompletedTask;
            }
            var cordinateParts = value.Split(',');
            if (cordinateParts.Length != 2 || !double.TryParse(cordinateParts[0], out double x) || !double.TryParse(cordinateParts[1], out double y))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Geçersiz koordinat değeri");
                return Task.CompletedTask;
            }
            var result = new GalacticCordinate
            {
                X = x,
                Y = y
            };
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
