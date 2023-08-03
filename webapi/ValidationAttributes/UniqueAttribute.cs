using System.ComponentModel.DataAnnotations;
using WebApi.Context;

namespace WebApi.ValidationAttributes;

//[AttributeUsage(AttributeTargets.Property)]
//public class UniqueAttribute : ValidationAttribute
//{
//    readonly string idName;
//    public UniqueAttribute(string idName) : this(idName, "{0} must be unique!") { }
//    public UniqueAttribute(string idName, string errorMessage)
//        => (this.idName, ErrorMessage) = (idName, errorMessage);

//    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
//    {
//        LearnerContext db = validationContext.GetService<LearnerContext>()!;

//        if (value is string name)
//        {
//            long id = (long)validationContext.ObjectType!.GetProperty(idName)!.GetValue(validationContext.ObjectInstance)!;

//            if (!db.Dogs.Any(d => d.Name == name && d.Id != id))
//                return ValidationResult.Success;
//        }

//        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
//    }
//}