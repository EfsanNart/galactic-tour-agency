using System.ComponentModel.DataAnnotations;

namespace GalacticTourAgency.Attributes
{
    public class GalacticElementCompositionAttribute : ValidationAttribute
    {
        private readonly string[] _validElement = new[]
        {
            "Hydrogen","Oxygen","Nitrogen","Iron","Gold","Silver","Platinum"
        };
        public int MinElements { get; }
        public int MaxElements { get; }
        public GalacticElementCompositionAttribute(int minElements = 1, int maxElements = 5)
        {
            MinElements = minElements;
            MaxElements = maxElements;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string composition)
            {
                var elements=composition.Split(',').Select(x =>x.Trim()).ToList();
                if(elements.Count < MinElements || elements.Count > MaxElements)
                {
                    return new ValidationResult($"En az {MinElements} en fazla {MaxElements} element içermelidir");
                }
                var invalidElements = elements.Except(_validElement,StringComparer.OrdinalIgnoreCase).ToList();
                if(invalidElements.Any())
                {
                    return new ValidationResult($"Geçersiz elementler: {string.Join(",", invalidElements)}. Geçerli elementler :{string.Join(",",_validElement)}");
                }
            }
            else
            {
                return new ValidationResult("Geçersiz değer, virgüller ile ayrılmış string değer girmelisiniz");
            }
            return ValidationResult.Success;
        }
    }
}
