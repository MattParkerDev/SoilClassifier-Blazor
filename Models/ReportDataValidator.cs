using FluentValidation;
using SoilClassifier_Blazor.Shared;

namespace SoilClassifier_Blazor.Models
{
    public class ReportDataValidator : AbstractValidator<ListState>
    {
        public ReportDataValidator()
        {
            RuleFor(listState => listState.ProjectName)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.ProjectNumber)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.ReportNumber)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.ClientName)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.Limits)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.ChainageMeasuredFrom)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.SampledBy)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.SampledDate)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.PreparedBy)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.PreparedDate)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.GroupedSamplesString)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.SignatoryName)
                .NotEmpty().WithMessage("This field is required.");
            RuleFor(listState => listState.NataNumber)
                .NotEmpty().WithMessage("This field is required.");

        }
    }
}
