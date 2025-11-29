using System.ComponentModel.DataAnnotations;
using CoreDriven.Dto;
using CoreDriven.Dto.Users;
using CoreDriven.UseCases.Validators;
using Validot;
using Validot.Results;

namespace CoreDriven.UseCases.Validator;

public static class ValidotExtension
{
    extension(IValidationResult result)
    {
        private string? Resolve()
        {
            var replace = result.ToString()?
                .Replace("\r\n", " | ")
                .Replace("\n", " | ");
            return replace;
        }
        
    }
    public static void Validate<T>(this T dto) where T : class, IValidot
    {
        var validator = dto switch
        {
            UserCreateDto => UserCreateDto.GetCreateValidator() as IValidator<T>,
            _ => null
        };
        if (validator == null)
            throw new InvalidOperationException($"No validator found for type {typeof(T).Name}");
    
        if (validator.IsValid(dto)) return;
        var result = validator.Validate(dto);
        throw new ValidationException(result.Resolve());
    }

}